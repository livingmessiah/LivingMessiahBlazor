using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;
using LivingMessiah.Web.Data;
using System;

namespace LivingMessiah.Web.Features.Contacts.Data;

public interface IRepository
{
	string BaseSqlDump { get; }
	Task<List<ContactQuery>> GetAll();
	Task<int> UpdateContactSukkotInviteDate(int id);
	Task<List<Download>> GetDownloads(bool selectAll, bool testEmails);
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

	public async Task<List<ContactQuery>> GetAll()  // bool selectAll
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
			var rows = await connection.QueryAsync<ContactQuery>(sql: base.Sql);  //, param: base.Parms
			return rows.ToList();
		});
	}

	// ToDo: This is a work in progress
	public async Task<int> UpdateContactSukkotInviteDate(int id)
	{
		DateTime azdt = DateTime.UtcNow.AddHours(Utc.ArizonaUtcMinus7);
		base.Parms = new DynamicParameters(new { Id = id, SukkotInviteDate = azdt });
		base.Sql = "UPDATE dbo.Contact SET SukkotInviteDate = @SukkotInviteDate WHERE Id=@id";

		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return count;
		});
	}

	// ToDo: This is a work in progress
	public async Task<List<Download>> GetDownloads(bool selectAll, bool testEmails)
	{
		const string TOP = "TOP 250 ";
		string Selected = selectAll ? " 1 AS Selected" : " 0 AS Selected";
		//string Where = testEmails? " WHERE FamilyName LIKE 'Marsing%' OR (FirstName = 'Mark' AND FamilyName = 'Webb') OR (FirstName = 'Ralphie' AND FamilyName = 'Cratty')" : "";
		//string Where = testEmails ? " WHERE FamilyName LIKE 'Marsing%' " : "";
		string Where = "";
		base.Sql = $@"SELECT {TOP}  {Selected}, ROW_NUMBER() OVER(ORDER BY Id ASC)-1 AS ZeroBasedRowCnt, Id, FamilyName, FirstName, SpouseName, EMail FROM Sukkot.Download {Where} ";

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Download>(sql: base.Sql);
			return rows.ToList();
		});
	}
}
