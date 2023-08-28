using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

namespace LivingMessiah.Web.Features.Admin.Database.Sukkot;

public interface IRepository
{
	Task<int> LogErrorTest();
	Task<List<zvwErrorLog>> GetzvwErrorLog();
	Task<int> EmptyErrorLog();
}

public class Repository : BaseRepositoryAsync, IRepository
{

	public Repository(IConfiguration config, ILogger<Repository> logger) 
		: base(config, logger, DataEnumsDatabase.Sukkot.ConnectionStringKey)
	{
	}

	public async Task<int> LogErrorTest()
	{
		base.Sql = "dbo.stpLogErrorTest ";
		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql, commandType: System.Data.CommandType.StoredProcedure);
			return count;
		});
	}

	public async Task<List<zvwErrorLog>> GetzvwErrorLog()
	{
		base.Sql = $@"SELECT TOP 75 * FROM zvwErrorLog ORDER BY ErrorLogID DESC";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<zvwErrorLog>(sql: base.Sql);
			return rows.ToList();
		});
	}

	public async Task<int> EmptyErrorLog()
	{
		base.Sql = "dbo.stpLogErrorEmpty";
		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, commandType: System.Data.CommandType.StoredProcedure);
			return affectedrows;
		});
	}

}

