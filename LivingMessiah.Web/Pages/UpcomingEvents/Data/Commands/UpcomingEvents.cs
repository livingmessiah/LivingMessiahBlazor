using Dapper;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using LivingMessiah.Data;

namespace LivingMessiah.Web.Pages.UpcomingEvents.Data.Commands
{
	public interface IUpcomingEvents
	{
		string BaseSqlDump { get; }
		Task<int> UpdateKeyDate(int Id, DateTime Date); 
		//Task<List<Domain.KeyDates.Commands.DateUnion>> GetDateUnionList(RelativeYearEnum relativeYear);
	}

	public class UpcomingEvents : BaseRepositoryAsync, IUpcomingEvents
	{
		public UpcomingEvents(IConfiguration config, ILogger<UpcomingEvents> logger) : base(config, logger)
		{
		}
		public string BaseSqlDump
		{
			get { return base.SqlDump; }
		}

		public async Task<int> UpdateKeyDate(int id, DateTime date) 
		{
			base.Parms = new DynamicParameters(new { Id = id, Date = date });
			base.Sql = $"UPDATE KeyDate.Date SET Date = @Date WHERE Id=@Id; ";
			return await WithConnectionAsync(async connection =>
			{
				log.LogDebug($"base.Sql: {base.Sql}, base.Parms:{base.Parms}");
				var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
				return count;
			});
		}

	}
}
