using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;
using LivingMessiah.Web.Pages.Sukkot.Services;


namespace LivingMessiah.Web.Features.Calendar.ParashaCalendar;

public interface IRepository
{
	string BaseSqlDump { get; }
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
		get { return base.SqlDump ?? ""; }
	}
		
	public async Task<List<Query>> GetQuery(int year)
	{
		log.LogDebug(String.Format("Inside {0}, year={1}", nameof(Repository) + "!" + nameof(GetQuery), year));
		base.Parms = new DynamicParameters(new { Year = year });
		base.Sql = "dbo.stpParashaCalendar";
		return await WithConnectionAsync(async connection =>
		{{
			var rows = await connection.QueryAsync<Query>
				(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			return rows.OrderBy(o => o.ShabbatDate).ToList();
		}});
	}
}

