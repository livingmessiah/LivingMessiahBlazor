using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

using LivingMessiah.Web.Features.Parasha.ListByBook;

namespace LivingMessiah.Web.Features.Parasha.Data;

public interface IParashaRepository
{
	string BaseSqlDump { get; }
	Task<CurrentParasha?> GetCurrentParasha();
	Task<List<Parashot>> GetParashotByBookId(int bookId);
}

public class ParashaRepository : BaseRepositoryAsync, IParashaRepository
{
	public ParashaRepository(IConfiguration config, ILogger<ParashaRepository> logger)
		: base(config, logger, DataEnumsDatabase.LivingMessiah.ConnectionStringKey)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump ?? ""; }
	}

	public async Task<CurrentParasha?> GetCurrentParasha()
	{
		base.Sql = $@"
SELECT 
Id, TriNum, ShabbatDate
, PrevId, NextId, BookId
, TorahLong, Haftorah, Brit
, ParashaName, AhavtaURL, NameUrl
, BaseParashaUrl
FROM Bible.vwCurrentParasha;
";
		return await WithConnectionAsync(async connection =>
		{
			var row = await connection.QueryAsync<CurrentParasha>(base.Sql);
			return row.SingleOrDefault();
		});
	}

	public async Task<List<Parashot>> GetParashotByBookId(int bookId)
	{
		base.Parms = new DynamicParameters(new { BookId = bookId });
		base.Sql = $@"
--DECLARE @BookId int=1

SELECT
Id
, ROW_NUMBER() OVER(PARTITION BY BookId ORDER BY Id ) AS RowCntByBookId
, BookId, Torah AS TorahLong, Name, TriNum, ParashaName
, NameUrl, AhavtaURL, Meaning, IsNewBook, Haftorah, Brit
, ShabbatDate
, BaseParashaUrl, CurrentParashaUrl
FROM Bible.vwParasha
WHERE BookId = @BookId
ORDER BY Id
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Parashot>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});

	}

}