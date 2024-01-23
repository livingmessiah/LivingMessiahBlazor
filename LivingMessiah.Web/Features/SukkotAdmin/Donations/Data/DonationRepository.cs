using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Features.SukkotAdmin.Donations.Domain;
using LivingMessiah.Web.Features.SukkotAdmin.Donations.Enums;
using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

namespace LivingMessiah.Web.Features.SukkotAdmin.Donations.Data;

public interface IDonationRepository
{
	string BaseSqlDump { get; }
	Task<int> InsertRegistrationDonation(Donation donation);
	Task<List<DonationReport>> GetDonationReport(DonationStatusFilter filter, string sortAndOrder);
	Task<List<DonationDetail>> GetDonationDetails(int registrationId);
	Task<List<DonationDetail>> GetDonationDetailsAll();
	Task<DonationDetail> GetDonationDetail(int id);
	Task<DonationDetail> UpdateDonationDetail(DonationDetail donationDetail);
	Task<List<RegistrationLookup>> PopulateRegistrationLookup();
}
public class DonationRepository : BaseRepositoryAsync, IDonationRepository
{
	public string BaseSqlDump
	{
		get { return base.SqlDump ?? ""; }
	}

	public DonationRepository(IConfiguration config, ILogger<DonationRepository> logger)
		: base(config, logger, DataEnumsDatabase.Sukkot.ConnectionStringKey)
	{
	}

	public async Task<List<RegistrationLookup>> PopulateRegistrationLookup()
	{
		base.Sql = $@"
SELECT Id AS ID, Sukkot.udfFormatName(1, FamilyName, FirstName, NULL, NULL) AS Text
FROM Sukkot.Registration
ORDER BY FirstName
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<RegistrationLookup>(base.Sql);
			return rows.ToList();
		});
	}

	public async Task<int> InsertRegistrationDonation(Donation donation)
	{
		base.Sql = "Sukkot.stpDonationInsert ";
		base.Parms = new DynamicParameters(new
		{
			donation.RegistrationId,
			donation.Amount,
			donation.Notes,
			donation.ReferenceId,
			donation.CreatedBy,   
			donation.CreateDate
		});

		/*
			RegistrationId = donation.RegistrationId,
			Amount = donation.Amount,
			Notes = donation.Notes,
			ReferenceId = donation.ReferenceId,
			CreatedBy = donation.CreatedBy,   
			CreateDate = donation.CreateDate
		*/

		base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

		base.log.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(DonationRepository)}!{nameof(InsertRegistrationDonation)}, Sql: {Sql}");

		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: CommandType.StoredProcedure);
			int? x = base.Parms.Get<int?>("NewId");
			if (x == null)
			{
				base.log.LogWarning($"NewId is null; returning as 0; Check dbo.ErrorLog for FK_Donation_Registration conflict Error; donation.RegistrationId: {donation.RegistrationId}");
				return 0;
			}
			else
			{
				int NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				base.log.LogDebug($"Return NewId:{NewId}");
				return NewId;
			}
		});
	}

	public async Task<DonationDetail> UpdateDonationDetail(DonationDetail donationDetail)
	{
		_ = await UpdateDonationProcess(donationDetail);
		DonationDetail NewDonation = new DonationDetail();
		NewDonation = await GetDonationDetail(donationDetail.Id);
		base.log.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(UpdateDonationDetail)}, Sql: {Sql}");
		return NewDonation;
	}

	private async Task<int> UpdateDonationProcess(DonationDetail donationDetail)
	{
		base.Parms = new DynamicParameters(new
		{
			Id = donationDetail.Id,
			RegistrationId = donationDetail.RegistrationId,
			Detail = donationDetail.Detail,
			Amount = donationDetail.Amount,
			Notes = donationDetail.Notes,
			ReferenceId = donationDetail.ReferenceId,
			CreateDate = donationDetail.CreateDate,
			CreatedBy = donationDetail.CreatedBy
		});

		base.Sql = $@"
UPDATE Sukkot.Donation SET 
	RegistrationId = @RegistrationId
, Detail = @Detail
, Amount = @Amount
, Notes = @Notes
, ReferenceId = @ReferenceId
, CreateDate = @CreateDate
, CreatedBy = @CreatedBy
WHERE Id=@Id
";
		base.log.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(UpdateDonationProcess)}, Sql: {Sql}");

		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return count;
		});
	}

	public async Task<List<DonationReport>> GetDonationReport(DonationStatusFilter filter, string sortAndOrder)
	{
		base.Parms = new DynamicParameters(new { DonationStatus = filter.Value });
		//base.Parms = new DynamicParameters(new { SortAndOrder = sortAndOrder });

		base.Sql = $@"
SELECT Id, EMail, FamilyName, FirstName, StatusId, StatusDescr, RegistrationFeeAdjusted
, TotalDonation, AmountDue
FROM Sukkot.tvfDonationReport(@DonationStatus)
ORDER BY {sortAndOrder}
";

		//base.log.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(GetDonationReport)}, filter.Name/filter.Value: {filter.Name}/{filter.Value}");
		//base.log.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(GetDonationReport)}, Sql: {Sql}");

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<DonationReport>(base.Sql, base.Parms);
			return rows.ToList();
		});
	}

	public async Task<List<DonationDetail>> GetDonationDetails(int registrationId)
	{
		base.Parms = new DynamicParameters(new { RegistrationId = registrationId });
		base.Sql = $@"
SELECT 
	Id, RegistrationId, Detail, Amount, Notes, ReferenceId, CreateDate, CreatedBy 
FROM Sukkot.Donation
WHERE RegistrationId = @RegistrationId
ORDER BY Detail
";
		base.log.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(GetDonationDetails)}, Sql: {Sql}, registrationId: {registrationId}");

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<DonationDetail>(base.Sql, base.Parms);
			return rows.ToList();
		});
	}

	public async Task<List<DonationDetail>> GetDonationDetailsAll()
	{
		base.Sql = $@"
SELECT 
	d.Id, RegistrationId, Detail, Amount, d.Notes, ReferenceId, CreateDate, CreatedBy 
,	Sukkot.udfFormatName(1, FamilyName, FirstName, NULL, NULL) AS Name
FROM Sukkot.Donation d
INNER JOIN Sukkot.Registration r ON r.Id = d.RegistrationId
ORDER BY RegistrationId, Detail";

		base.log.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(GetDonationDetailsAll)}, Sql: {Sql}");

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<DonationDetail>(base.Sql);
			return rows.ToList();
		});
	}

	public async Task<DonationDetail> GetDonationDetail(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $@"
SELECT 
	d.Id, RegistrationId, Detail, Amount, d.Notes, ReferenceId, CreateDate, CreatedBy 
,	Sukkot.udfFormatName(1, FamilyName, FirstName, NULL, NULL) AS Name
FROM Sukkot.Donation d
INNER JOIN Sukkot.Registration r ON r.Id = d.RegistrationId
WHERE d.Id = @Id	
";
		base.log.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(GetDonationDetail)}, Sql: {Sql}, id: {id}");

		return await WithConnectionAsync(async connection =>
		{
			var donationDetail = await connection.QueryAsync<DonationDetail>(base.Sql, base.Parms);
			return donationDetail.SingleOrDefault()!;
		});
	}

}
