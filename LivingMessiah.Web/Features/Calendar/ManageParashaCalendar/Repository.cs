﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;


namespace LivingMessiah.Web.Features.Calendar.ManageParashaCalendar;

public interface IRepository
{
		string BaseSqlDump { get; }
		Task<List<YearLookupQuery>> GetYearLookupQuery();
		Task<List<Query>> GetQuery(int year);
}
public class Repository : BaseRepositoryAsync, IRepository
{
		public Repository(IConfiguration config, ILogger<Repository> logger)
			: base(config, logger, DataEnumsDatabase.LivingMessiah.ConnectionStringKey)
		{
		}

		public string BaseSqlDump
		{
				get { return SqlDump ?? ""; }
		}

		public async Task<List<Query>> GetQuery(int year)
		{
				log.LogDebug(string.Format("Inside {0}, year={1}", nameof(Repository) + "!" + nameof(GetQuery), year));
				Parms = new DynamicParameters(new { Year = year });
				Sql = "dbo.stpParashaCalendar";
				return await WithConnectionAsync(async connection =>
				{
						{
								var rows = await connection.QueryAsync<Query>
							(sql: Sql, param: Parms, commandType: System.Data.CommandType.StoredProcedure);
								return rows.OrderBy(o => o.ShabbatDate).ToList();
						}
				});
		}

		public async Task<List<YearLookupQuery>> GetYearLookupQuery()
		{
				log.LogDebug(string.Format("Inside {0}", nameof(Repository) + "!" + nameof(GetYearLookupQuery)));
				Sql = $@"
SELECT Year(ShabbatDate) AS Year, COUNT(*) As RowsPerYear 
FROM ShabbatWeek 
GROUP By Year(ShabbatDate)
	";
				return await WithConnectionAsync(async connection =>
				{
						var rows = await connection.QueryAsync<YearLookupQuery>(sql: Sql);
						log.LogDebug(string.Format("... rows {0}; Sql{1}", rows.Count(), SqlDump));
						return rows.ToList();
				});
		}

}

