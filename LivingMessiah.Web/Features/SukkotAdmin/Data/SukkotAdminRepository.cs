using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Features.SukkotAdmin.Attendance.Domain;

using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

namespace LivingMessiah.Web.Features.SukkotAdmin.Data;

public interface ISukkotAdminRepository
{
	Task<List<vwAttendanceAllFeastDays>> GetAttendanceAllFeastDays();
	Task<vwAttendancePeopleSummary> GetAttendancePeopleSummary();
	Task<List<vwAttendanceChart>> GetAttendanceChart();
}

public class SukkotAdminRepository : BaseRepositoryAsync, ISukkotAdminRepository
{
	public SukkotAdminRepository(IConfiguration config, ILogger<SukkotAdminRepository> logger)
		: base(config, logger, DataEnumsDatabase.Sukkot.ConnectionStringKey)
	{
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
