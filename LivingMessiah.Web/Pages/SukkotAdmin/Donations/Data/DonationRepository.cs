using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SukkotApi.Data;
using SukkotApi.Domain.Donations.Enums;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data
{
	public interface IDonationRepository
	{
		Task<int> InsertRegistrationDonation(Donation donation);
		Task<List<DonationReport>> GetDonationReport(BaseDonationStatusFilterSmartEnum filter, string sortAndOrder);
		Task<List<DonationDetail>> GetDonationDetails(int registrationId);
		Task<List<DonationDetail>> GetDonationDetailsAll();
		Task<DonationDetail> GetDonationDetail(int id);
		Task<DonationDetail> UpdateDonationDetail(DonationDetail donationDetail);
		Task<int> DeleteDonationDetail(int id);
	}
	public class DonationRepository : BaseRepositoryAsync, IDonationRepository
	{
		public DonationRepository(IConfiguration config, ILogger<DonationRepository> logger) : base(config, logger)
		{
		}

		public async Task<int> InsertRegistrationDonation(Donation donation)
		{
			base.Sql = "Sukkot.stpDonationInsert ";
			base.Parms = new DynamicParameters(new
			{
				RegistrationId = donation.RegistrationId,
				Amount = donation.Amount,
				Notes = donation.Notes,
				ReferenceId = donation.ReferenceId,
				CreatedBy = donation.CreatedBy,
				CreateDate = donation.CreateDate
			});

			base.log.LogDebug($"Inside {nameof(InsertRegistrationDonation)}, Sql: {Sql}");

			return await WithConnectionAsync(async connection =>
			{
				var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
				return count;
			});
		}

		public async Task<DonationDetail> UpdateDonationDetail(DonationDetail donationDetail)
		{
			_ = await UpdateDonationProcess(donationDetail);
			DonationDetail NewDonation = new DonationDetail();
			NewDonation = await GetDonationDetail(donationDetail.Id);
			base.log.LogDebug($"Inside {nameof(UpdateDonationDetail)}, Sql: {Sql}");
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
			base.log.LogDebug($"Inside {nameof(UpdateDonationProcess)}, Sql: {Sql}");

			return await WithConnectionAsync(async connection =>
			{
				var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
				return count;
			});
		}

		public async Task<List<DonationReport>> GetDonationReport(BaseDonationStatusFilterSmartEnum filter, string sortAndOrder)
		{
			base.Parms = new DynamicParameters(new { DonationStatus = filter.Value });
			//base.Parms = new DynamicParameters(new { SortAndOrder = sortAndOrder });

			base.Sql = $@"
SELECT Id, EMail, FamilyName, FirstName, StatusId, StatusDescr, MealTotalCost, RegistrationFee
, CampCost, TotalDonation, AmountDue
, LocationEnum AS LocationInt
FROM Sukkot.tvfDonationReport(@DonationStatus)
ORDER BY {sortAndOrder}
";

			//base.log.LogDebug($"Inside {nameof(GetDonationReport)}, filter.Name/filter.Value: {filter.Name}/{filter.Value}");
			//base.log.LogDebug($"Inside {nameof(GetDonationReport)}, Sql: {Sql}");

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
WHERE RegistrationId = @RegistrationId
FROM Sukkot.Donation
ORDER BY RegistrationId, Detail
";
			base.log.LogDebug($"Inside {nameof(GetDonationDetails)}, Sql: {Sql}");

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
	Id, RegistrationId, Detail, Amount, Notes, ReferenceId, CreateDate, CreatedBy 
FROM Sukkot.Donation
ORDER BY RegistrationId, Detail";

			base.log.LogDebug($"Inside {nameof(GetDonationDetailsAll)}, Sql: {Sql}");

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
	Id, RegistrationId, Detail, Amount, Notes, ReferenceId, CreateDate, CreatedBy
WHERE Id = @Id	
";
			base.log.LogDebug($"Inside {nameof(GetDonationDetail)}, Sql: {Sql}");

			return await WithConnectionAsync(async connection =>
			{
				var donationDetail = await connection.QueryAsync<DonationDetail>(base.Sql, base.Parms);
				return donationDetail.SingleOrDefault();
			});
		}

		public async Task<int> DeleteDonationDetail(int id)
		{
			base.Parms = new DynamicParameters(new { Id = id });
			base.Sql = "DELETE FROM Sukkot.Donation WHERE Id=@Id";
			
			base.log.LogDebug($"Inside {nameof(DeleteDonationDetail)}, Sql: {Sql}");

			return await WithConnectionAsync(async connection =>
			{
				var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
				return affectedrows;
			});
		}

	}
}
