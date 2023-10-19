using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

namespace LivingMessiah.Web.Pages.Sukkot.ManageNotes.Data;

public interface IRepository
{
		string BaseSqlDump { get; }

		Task<List<NotesQuery>> GetAdminOrUserNotes(Enums.Filter filter);
}


public class Repository : BaseRepositoryAsync, IRepository
{
		public Repository(IConfiguration config, ILogger<Repository> logger)
			: base(config, logger, DataEnumsDatabase.Sukkot.ConnectionStringKey)
		{
		}

		public string BaseSqlDump
		{
				get { return SqlDump!; }
		}

		// Only NotesEnum.All.SqlWhere and NotesEnum.All.SqlOrder are used
		public async Task<List<NotesQuery>> GetAdminOrUserNotes(Enums.Filter filter)
		{
				Sql = $@"
SELECT TOP 500 Id, FirstName, FamilyName, AdminNotes, Notes AS UserNotes, Phone, EMail
FROM Sukkot.vwRegistration
{filter.SqlWhere}
{filter.SqlOrder}
";

				return await WithConnectionAsync(async connection =>
				{
						var rows = await connection.QueryAsync<NotesQuery>(sql: Sql);
						return rows.ToList();
				});
		}

}
