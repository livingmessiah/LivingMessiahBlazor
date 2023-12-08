using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

namespace LivingMessiah.Web.Pages.Admin.WirecastFolder;

public interface IShabbatWeekRepository
{
	string BaseSqlDump { get; }

	// Wirecast
	Task<int> UpdateWirecastLink(int id, string wireCastLink);
	Task<int> UpdateScratchpad(string scratchPad);
	Task<WirecastVM> GetCurrentWirecast();
	Task<ScratchPad> GetScratchPadWireCast();

	#region ToDo: Move somewhere else

	// ToDo does this go with LivingMessiahAdmin as well
	Task<int> UpdateContactSukkotInviteDate(int id);
	Task<List<Download>> GetDownloads(bool selectAll, bool testEmails);
	#endregion
}


public class ShabbatWeekRepository : BaseRepositoryAsync, IShabbatWeekRepository
{
	public ShabbatWeekRepository(IConfiguration config, ILogger<ShabbatWeekRepository> logger)
		: base(config, logger, DataEnumsDatabase.LivingMessiah.ConnectionStringKey)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump ?? ""; }
	}


	#region ShabbatWeek

	private static (string CompareDate, bool IsDayOfWeekSaturday) CurrentShabbatDate()
	{
		DateTime CompareDate = DateTime.Today;
		string sCompareDate = DateTime.Today.ToString("yyyy-MM-dd") + " 12:00:00 AM";
		bool isDayOfWeekSaturday = CompareDate.DayOfWeek == DayOfWeek.Saturday ? true : false;
		return (sCompareDate, isDayOfWeekSaturday);
	}

	#endregion


	#region Wirecast
	public async Task<WirecastVM> GetCurrentWirecast()
	{
		var datesTuple = CurrentShabbatDate();
		log.LogDebug(String.Format("Inside {0}, CompareDate={1}; IsDayOfWeekSaturday={2}"
			, nameof(ShabbatWeekRepository) + "!" + nameof(GetCurrentWirecast), datesTuple.CompareDate, datesTuple.IsDayOfWeekSaturday));

		base.Parms = new DynamicParameters(new { CompareDate = datesTuple.CompareDate }); 


		if (datesTuple.IsDayOfWeekSaturday)
		{
			base.Sql = $@"
--DECLARE @CompareDate varchar(30) =  '2022-11-12 12:00:00 AM'
SELECT sw.Id, sw.ShabbatDate, sw.WirecastLink
FROM ShabbatWeek sw
WHERE sw.ShabbatDate >= @CompareDate AND sw.ShabbatDate <= @CompareDate
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<WirecastVM>(sql: base.Sql, param: base.Parms);
				return rows.SingleOrDefault()!;
			});
		}
		else
		{
			base.Sql = $@"
--DECLARE @CompareDate varchar(30) =  '2022-11-12 12:00:00 AM'
SELECT sw.Id, sw.ShabbatDate, sw.WirecastLink
FROM ShabbatWeek sw
WHERE sw.ShabbatDate >= @CompareDate
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<WirecastVM>(sql: base.Sql, param: base.Parms);
				return rows.First();
			});
		}

	}

	public async Task<ScratchPad> GetScratchPadWireCast()
	{
		base.Sql = "SELECT WireCast FROM ScratchPad";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<ScratchPad>(sql: base.Sql);
			return rows.SingleOrDefault()!;
		});
	}

	public async Task<int> UpdateWirecastLink(int id, string wireCastLink)
	{
		base.Parms = new DynamicParameters(new { Id = id, WireCastLink = wireCastLink });
		base.Sql = $@"UPDATE dbo.ShabbatWeek SET WirecastLink = @WireCastLink WHERE Id = @Id";

		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return count;
		});
	}

	public async Task<int> UpdateScratchpad(string wireCast)
	{
		base.Parms = new DynamicParameters(new { WireCast = wireCast });
		base.Sql = $@"UPDATE dbo.ScratchPad SET WireCast = @WireCast";

		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return count;
		});
	}
	#endregion



	#region ToDo: Move somewhere else


	public async Task<int> UpdateContactSukkotInviteDate(int id)
	{
		DateTime azdt = DateTime.UtcNow.AddHours(Utc.ArizonaUtcMinus7);
		base.Parms = new DynamicParameters(new { Id = id, SukkotInviteDate = azdt });
		base.Sql = "UPDATE dbo.Contact SET SukkotInviteDate = @SukkotInviteDate WHERE Id=@id";

		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return count;
		});
	}

	public async Task<List<Download>> GetDownloads(bool selectAll, bool testEmails)
	{
		const string TOP = "TOP 250 ";
		string Selected = selectAll ? " 1 AS Selected" : " 0 AS Selected";
		//string Where = testEmails? " WHERE FamilyName LIKE 'Marsing%' OR (FirstName = 'Mark' AND FamilyName = 'Webb') OR (FirstName = 'Ralphie' AND FamilyName = 'Cratty')" : "";
		//string Where = testEmails ? " WHERE FamilyName LIKE 'Marsing%' " : "";
		string Where = "";
		base.Sql = $@"SELECT {TOP}  {Selected}, ROW_NUMBER() OVER(ORDER BY Id ASC)-1 AS ZeroBasedRowCnt, Id, FamilyName, FirstName, SpouseName, EMail FROM Sukkot.Download {Where} ";

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Download>(sql: base.Sql);
			return rows.ToList();
		});
	}

	#endregion
}
