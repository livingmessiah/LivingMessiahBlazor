using Dapper;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.Sukkot.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Data;

namespace LivingMessiah.Web.Pages.Sukkot.Data;

public interface ISukkotRepository
{
	string BaseSqlDump { get; }
	Task<vwRegistration> ById(int id);
	Task<RegistrationPOCO> ById2(int id);
	Task<vwRegistrationStep> GetByEmail(string email);

	Task<int> Create(RegistrationPOCO registration);
	Task<int> Update(RegistrationPOCO registration);
	Task<int> Delete(int id);
	Task<RegistrationSummary> GetRegistrationSummary(int id);

	Task<int> InsertHouseRulesAgreement(string email, string timeZone);
	Task<List<vwHouseRulesAgreement>> NoRegistration();
	Task<int> DeleteHouseRulesAgreementRecord(string email);
}

public class SukkotRepository : BaseRepositoryAsync, ISukkotRepository
{
	public SukkotRepository(IConfiguration config, ILogger<SukkotRepository> logger) : base(config, logger)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump; }
	}

	public async Task<vwRegistration> ById(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $@"SELECT TOP 1 * FROM Sukkot.vwRegistration WHERE Id = @id";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwRegistration>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault();
		});
	}

	public async Task<RegistrationPOCO> ById2(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $@"
--DECLARE @Id int = 32
SELECT TOP 1 
Id, FamilyName, FirstName, SpouseName, OtherNames, EMail, Phone, Adults, ChildBig, ChildSmall
, StatusId
, AttendanceBitwise, LmmDonation, Notes, Avatar
, Sukkot.udfAttendanceDatesConcat(Id) AS AttendanceDatesCSV
FROM Sukkot.Registration 
WHERE Id = @Id
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<RegistrationPOCO>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault();
		});
	}

	public async Task<vwRegistrationStep> GetByEmail(string email)
	{
		base.log.LogDebug(string.Format("Inside: {0} email:{1}"
			, nameof(GetByEmail), email));
		base.Parms = new DynamicParameters(new { EMail = email });
		base.Sql = $@"
--DECLARE @EMail varchar(100) = 'info@test.com'
SELECT Id, EMail
, TimeZone AS HouseRulesAgreementTimeZone, AcceptedDate AS HouseRulesAgreementAcceptedDate
, RegistrationId, FirstName, FamilyName, StatusId
, TotalDonation, RegistrationFeeAdjusted
FROM Sukkot.vwRegistrationStep 
WHERE EMail = @EMail
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwRegistrationStep>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault();
		});
	}

	public async Task<int> Create(RegistrationPOCO registration)
	{
		base.Sql = "Sukkot.stpRegistrationInsert";
		base.Parms = new DynamicParameters(new
		{
			FamilyName = registration.FamilyName,
			FirstName = registration.FirstName,
			SpouseName = registration.SpouseName,
			OtherNames = registration.OtherNames,
			Email = registration.EMail,
			Phone = registration.Phone,
			Adults = registration.Adults,
			ChildBig = registration.ChildBig,
			ChildSmall = registration.ChildSmall,
			StatusId = registration.Status.Value,
			AttendanceBitwise = registration.AttendanceBitwise,
			LmmDonation = 0,
			Avatar = registration.Avatar,
			Notes = registration.Notes,
		});

		base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug($"Inside {nameof(SukkotRepository)}!{nameof(Create)}; About to execute sql:{base.Sql}");
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			int? x = base.Parms.Get<int?>("NewId");
			if (x == null)
			{
				base.log.LogWarning($"NewId is null; returning as 0; Check dbo.ErrorLog for IX_Registration_EMail_Unique duplication Error; registration.EMail: {@registration.EMail}");
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

	public async Task<int> Update(RegistrationPOCO registration)
	{
		//ToDo: used this use base.Parm ?
		base.Sql = $@"
UPDATE Sukkot.Registration SET 
	FamilyName = N'{registration.FamilyName}',
	FirstName = N'{registration.FirstName}',
	SpouseName = N'{registration.SpouseName}',
	OtherNames = N'{registration.OtherNames}',
	EMail = N'{registration.EMail}',
	Phone = N'{registration.Phone}',
	Adults = {registration.Adults},
	ChildBig = {registration.ChildBig},
	ChildSmall = {registration.ChildSmall},
	AttendanceBitwise = {registration.AttendanceBitwise},
	StatusId = {registration.Status.Value},  
	LmmDonation = {registration.LmmDonation},
	Notes = N'{registration.NotesScrubbed}',
	Avatar = N'{registration.Avatar}'
WHERE Id = {registration.Id};
";
		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql);
			return count;
		});
	}

	public async Task<int> Delete(int id)
	{
		base.Sql = "Sukkot.stpRegistrationDelete";
		base.Parms = new DynamicParameters(new { RegistrationId = id });
		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			//if (affectedrows < 0) { throw new Exception($"Registration NOT Deleted"); }
			return affectedrows;
		});
	}

	public async Task<RegistrationSummary> GetRegistrationSummary(int id)
	{
		base.Parms = new DynamicParameters(new { id = id });  // This should be { Id = id })
		base.Sql = $@"
--DECLARE @id int=2
SELECT Id, EMail, FamilyName, Adults, ChildBig, ChildSmall, StatusId
, AttendanceBitwise, RegistrationFeeAdjusted, TotalDonation
FROM Sukkot.tvfRegistrationSummary(@id)
";
		// FROM Sukkot.tvfRegistrationSummary(@id) should be FROM Sukkot.tvfRegistrationSummary(@Id)

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<RegistrationSummary>(base.Sql, base.Parms);
			return rows.SingleOrDefault();
		});
	}

	public async Task<List<vwHouseRulesAgreement>> NoRegistration() 
	{
		base.Sql = $@"
SELECT hra.Id, hra.EMail, hra.AcceptedDate, hra.TimeZone
FROM Sukkot.HouseRulesAgreement hra
	LEFT JOIN Sukkot.Registration r 
		ON hra.Id=r.HouseRulesAgreementId
WHERE r.HouseRulesAgreementId IS NULL
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwHouseRulesAgreement>(base.Sql);
			return rows.ToList();
		});
	}

	public async Task<int> InsertHouseRulesAgreement(string email, string timeZone)
	{
		base.Sql = "Sukkot.stpHouseRulesAgreementInsert";
		base.Parms = new DynamicParameters(new
		{
			EMail = email,
			TimeZone = timeZone
		});
		base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug($"Inside {nameof(SukkotRepository)}!{nameof(InsertHouseRulesAgreement)}; About to execute sql:{base.Sql}");
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			int? x = base.Parms.Get<int?>("NewId");
			if (x == null)
			{
				base.log.LogWarning($"NewId is null; returning as 0; Check dbo.ErrorLog for IX_HouseRulesAgreement_Unique  duplication Error; email: {email}");
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

	public async Task<int> DeleteHouseRulesAgreementRecord(string email)
	{
		base.Sql = "Sukkot.stpHouseRulesAgreementDelete";
		base.Parms = new DynamicParameters(new { EMail = email });
		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			//if (affectedrows < 0) { throw new Exception($"Registration NOT Deleted"); }
			return affectedrows;
		});
	}
}




