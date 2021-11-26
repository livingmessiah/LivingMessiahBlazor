using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using LivingMessiah.Data;                   

using Microsoft.Extensions.Configuration;

using LivingMessiah.Web.Pages.KeyDates.Enums;
using LivingMessiah.Web.Pages.UpcomingEvents.Queries;

namespace LivingMessiah.Web.Pages.SmartEnums
{
	public interface ISmartEnumRepository
	{
		string BaseSqlDump { get; }
		Task<List<DateExplode>> GetDateExplode(RelativeYearEnum relativeYear);
	}

	public class SmartEnumRepository : BaseRepositoryAsync, ISmartEnumRepository
	{
		public SmartEnumRepository(IConfiguration config, ILogger<SmartEnumRepository> logger) : base(config, logger)
		{
		}

		public string BaseSqlDump
		{
			get { return base.SqlDump; }
		}

		public async Task<List<DateExplode>> GetDateExplode(RelativeYearEnum relativeYear)
		{
			base.Sql = $@"
SELECT 
	YearId, Date, GregorianYear, DateTypeId AS DateTypeEnum, DateTypeEnumId
--Id, DateYMD, RowCntByGregorianYear, IsDateTypeContiguous, DateType, DateTypeValue
FROM KeyDate.vwDateExplode
CROSS JOIN KeyDate.Constants c
WHERE YearId = {GetYearId(relativeYear)}
ORDER BY Date
";
			//base.log.LogDebug($"Inside {nameof(GetDateExplode)}, Sql: {Sql}");
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<DateExplode>(sql: base.Sql);
				return rows.ToList();
			});
		}


		private string GetYearId(RelativeYearEnum relativeYear)
		{
			return relativeYear switch
			{
				RelativeYearEnum.Previous => "c.PreviousYear",
				RelativeYearEnum.Current => "c.CurrentYear",
				RelativeYearEnum.Next => "c.NextYear",
				RelativeYearEnum.None => "0",
				_ => "c.CurrentYear",
			};

		}


	}
}
