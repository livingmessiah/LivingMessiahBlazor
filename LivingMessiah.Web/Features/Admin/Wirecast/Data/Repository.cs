
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

namespace LivingMessiah.Web.Features.Admin.Wirecast.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

	Task<WirecastQuery> GetCurrentWirecast();
	Task<ScratchPad> GetScratchPadWireCast();
	Task<int> UpdateWirecastLink(int id, string wireCastLink);
	Task<int> UpdateScratchpad(string scratchPad);
}


public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger)
		: base(config, logger, DataEnumsDatabase.LivingMessiah.ConnectionStringKey)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump ?? ""; }
	}


	public async Task<WirecastQuery> GetCurrentWirecast()
	{
		var datesTuple = CurrentShabbatDate();
		log.LogDebug(String.Format("Inside {0}, CompareDate={1}; IsDayOfWeekSaturday={2}"
			, nameof(Repository) + "!" + nameof(GetCurrentWirecast), datesTuple.CompareDate, datesTuple.IsDayOfWeekSaturday));

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
				var rows = await connection.QueryAsync<WirecastQuery>(sql: base.Sql, param: base.Parms);
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
				var rows = await connection.QueryAsync<WirecastQuery>(sql: base.Sql, param: base.Parms);
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

	private static (string CompareDate, bool IsDayOfWeekSaturday) CurrentShabbatDate()
	{
		DateTime CompareDate = DateTime.Today;
		string sCompareDate = DateTime.Today.ToString("yyyy-MM-dd") + " 12:00:00 AM";
		bool isDayOfWeekSaturday = CompareDate.DayOfWeek == DayOfWeek.Saturday ? true : false;
		return (sCompareDate, isDayOfWeekSaturday);
	}

}
// Ignore Spelling: Scratchpad