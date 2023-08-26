using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SukkotAdmin.Attendance.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes.Domain;
using LivingMessiah.Web.Pages.Sukkot.Domain;

using LivingMessiah.Web.Data;
using EnumsDatabase = LivingMessiah.Web.Features.Admin.Database.Enums.Database;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Data;

public interface ISukkotAdminRepository
{
	Task<List<vwRegistration>> GetAll(EnumsOld.RegistrationSortEnum sort, bool isAscending);
	Task<List<Notes>> GetNotes(EnumsOld.RegistrationSortEnum sort);

	Task<List<vwAttendanceAllFeastDays>> GetAttendanceAllFeastDays();
	Task<vwAttendancePeopleSummary> GetAttendancePeopleSummary();
	Task<List<vwAttendanceChart>> GetAttendanceChart();
}

public class SukkotAdminRepository : BaseRepositoryAsync, ISukkotAdminRepository
{

	public SukkotAdminRepository(IConfiguration config, ILogger<SukkotAdminRepository> logger)
		: base(config, logger, EnumsDatabase.Sukkot.ConnectionStringKey)
	{
	}

	public async Task<List<vwRegistration>> GetAll(EnumsOld.RegistrationSortEnum sort, bool isAscending)
	{
		base.log.LogDebug(string.Format("Inside {0} , sort:{1}, isAscending: {2}"
				, nameof(SukkotAdminRepository) + "!" + nameof(GetAll), sort, isAscending));
		string sortField = sort switch
		{
			EnumsOld.RegistrationSortEnum.Id => "Id",
			EnumsOld.RegistrationSortEnum.LastName => "FamilyName",
			EnumsOld.RegistrationSortEnum.FirstName => "FirstName",
			_ => "Id",
		};

		sortField += isAscending ? "" : " DESC";

		base.Sql = $@"
SELECT TOP 500 Id, FamilyName, FirstName, SpouseName, OtherNames
, EMail, Phone, Adults, ChildBig, ChildSmall
, StatusId
, Notes
, RegistrationFeeAdjusted, LmmDonation
, AttendanceBitwise, AttendanceTotal
FROM Sukkot.vwRegistration
ORDER BY {sortField}
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwRegistration>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});

		//https://stackoverflow.com/questions/17840526/dapper-order-by-parameters
		//http://bobby-tables.com/
		//https://xkcd.com/327/
		//base.Parms = new DynamicParameters(new { SortField = sortField });
		//ORDER BY @SortField

	}

	public async Task<List<Notes>> GetNotes(EnumsOld.RegistrationSortEnum sort)
	{
		string sortField = (sort == EnumsOld.RegistrationSortEnum.LastName) ? "FamilyName" : "Id";

		base.Sql = $@"
SELECT TOP 500 Id, FirstName, FamilyName, Notes AS UserNotes, Phone, EMail
FROM Sukkot.vwRegistration
WHERE Notes IS NOT NULL AND TRIM(Notes) <> ''
ORDER BY {sortField}
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Notes>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}

	public async Task<List<vwAttendanceAllFeastDays>> GetAttendanceAllFeastDays()
	{
		base.Sql = "SELECT FeastDay2, Id,  Adults, ChildBig, ChildSmall, TotalPeeps FROM Sukkot.vwAttendanceAllFeastDays ORDER BY Id";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwAttendanceAllFeastDays>(sql: base.Sql);
			return rows.ToList();
		});
	}

	public async Task<vwAttendancePeopleSummary> GetAttendancePeopleSummary()
	{
		base.Sql = "SELECT * FROM Sukkot.vwAttendancePeopleSummary";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwAttendancePeopleSummary>(sql: base.Sql);
			return rows.SingleOrDefault()!;
		});
	}

	public async Task<List<vwAttendanceChart>> GetAttendanceChart()
	{
		base.Sql = "SELECT FeastDay2, AgeDesc, Days FROM Sukkot.vwAttendanceChart ORDER BY Id, AgeSort";  // Id, AgeSort
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwAttendanceChart>(sql: base.Sql);
			return rows.ToList();
		});
	}

}
