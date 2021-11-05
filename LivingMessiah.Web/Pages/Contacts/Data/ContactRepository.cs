using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using LivingMessiah.Data;

/*
using LivingMessiah.Web.Pages.SukkotAdmin.Registration;
using LivingMessiah.Web.Pages.SukkotAdmin.Enums;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
*/

namespace LivingMessiah.Web.Pages.Contacts.Data
{
	public interface IContactRepository
	{
		string BaseSqlDump { get; }
		Task<List<Domain.ContactVM>> GetAll();  

	}

	public class ContactRepository : BaseRepositoryAsync, IContactRepository
	{
		public ContactRepository(IConfiguration config, ILogger<ContactRepository> logger) : base(config, logger)
		{
		}

		public string BaseSqlDump
		{
			get { return base.SqlDump; }
		}

		public async Task<List<Domain.ContactVM>> GetAll()  // bool selectAll
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
				var rows = await connection.QueryAsync<Domain.ContactVM>(sql: base.Sql);  //, param: base.Parms
				return rows.ToList();
			});
		}

	}
}
