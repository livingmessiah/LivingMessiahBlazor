﻿using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using EnumsDatabase = LivingMessiah.Web.Features.Admin.Database.Enums.Database;
using LivingMessiah.Web.Data;

namespace LivingMessiah.Web.Pages.Contacts.Data;

public interface IContactRepository
{
	string BaseSqlDump { get; }
	Task<List<ContactVM>> GetAll();
}

public class ContactRepository : BaseRepositoryAsync, IContactRepository
{
	public ContactRepository(IConfiguration config, ILogger<ContactRepository> logger) 
		: base(config, logger, EnumsDatabase.LivingMessiah.ConnectionStringKey)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump ?? ""; }
	}

	public async Task<List<ContactVM>> GetAll()  // bool selectAll
	{
		//const string TOP = "TOP 500 "; //SELECT {TOP} ...
		//base.Parms = new DynamicParameters(new { Top = top });
		base.Sql = $@"
SELECT
Id, FirstName, MiddleName, LastName, OtherNames, Email, Phone, Notes, SukkotAttendanceCSV
--, SendWeeklyNewsletter, NotInvited, SukkotInviteDate
FROM dbo.vwContact
ORDER BY FirstName
";

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<ContactVM>(sql: base.Sql);  //, param: base.Parms
			return rows.ToList();
		});
	}

}
