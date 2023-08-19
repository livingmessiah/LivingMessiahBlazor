using Dapper;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using LivingMessiah.Web.Data;


namespace LivingMessiah.Web.Pages.ShabbatService.Data;

public interface IRepository
{
	string BaseSqlDump { get; }
	Task<Models.PsalmAndProverbVM> GetCurrentPsalmAndProverb();
}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger) : base(config, logger)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump!; }
	}

	public async Task<Models.PsalmAndProverbVM> GetCurrentPsalmAndProverb()
	{
		var datesTuple = Helper.CurrentShabbatDate();
		bool isDayOfWeekSaturday = datesTuple.Item2;

		string Where = $" WHERE ShabbatDate >= '{datesTuple.Item1}' ";

		base.Sql = $@"
SELECT 
  ShabbatWeekId, ShabbatDate
, PsalmsBCV, PsalmsChapter
, PsalmsKJVHtmlConcat
, PsalmsUrl, PsalmsTitle
, ProverbsBCV, ProverbsChapter
, ProverbsKJVHtmlConcat
, ProverbsUrl
FROM Bible.vwPsalmsAndProverbs v 
WHERE ShabbatDate = dbo.udfGetNextShabbatDate()
";

		log.LogDebug(String.Format("Inside {0}, Sql={1}"
			, nameof(Repository) + "!" + nameof(GetCurrentPsalmAndProverb), base.Sql));


		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Models.PsalmAndProverbVM>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault()!;
		});
	}


}
