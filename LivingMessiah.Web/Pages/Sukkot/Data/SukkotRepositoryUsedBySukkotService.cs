using Dapper;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.Sukkot.Domain;
using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

namespace LivingMessiah.Web.Pages.Sukkot.Data;

public interface ISukkotRepositoryUsedBySukkotService
{
	string BaseSqlDump { get; }

	Task<vwRegistration> ById(int id);
	Task<vwRegistrationStep> GetByEmail(string email);
	Task<int> Delete(int id);
	Task<RegistrationSummary> GetRegistrationSummary(int id);
}

public class SukkotRepositoryUsedBySukkotService : BaseRepositoryAsync, ISukkotRepositoryUsedBySukkotService
{
	public SukkotRepositoryUsedBySukkotService(IConfiguration config, ILogger<SukkotRepositoryUsedBySukkotService> logger)
		: base(config, logger, DataEnumsDatabase.Sukkot.ConnectionStringKey)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump ?? ""; }
	}

	public async Task<vwRegistration> ById(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $@"SELECT TOP 1 * FROM Sukkot.vwRegistration WHERE Id = @id";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwRegistration>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault()!;
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
			return rows.SingleOrDefault()!;
		});
	}

	public async Task<int> Delete(int id)
	{
		base.Sql = "Sukkot.stpRegistrationDelete";
		base.Parms = new DynamicParameters(new { RegistrationId = id });
		return await WithConnectionAsync(async connection =>
		{
			var affectedRows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			//if (affectedRows < 0) { throw new Exception($"Registration NOT Deleted"); }
			return affectedRows;
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
			return rows.SingleOrDefault()!;
		});
	}

}




