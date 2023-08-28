using Dapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.Sukkot.SuperUser.Data;

using ReportVM = LivingMessiah.Web.Pages.Sukkot.SuperUser.Detail.ReportVM;

namespace LivingMessiah.Web.Data;

public interface IRepositoryNoBase
{
	Task<ReportVM?> GetDisplayAndDonationsById(int id);
}

public class RepositoryNoBase : IRepositoryNoBase
{
	const string configationConnectionKey = "ConnectionStrings:Sukkot";

	#region Constructor
	private readonly IConfiguration config;
	protected readonly ILogger log;

	public RepositoryNoBase(IConfiguration config, ILogger<RepositoryNoBase> logger)
	{
		this.config = config;
		log = logger;
	}
	#endregion

	public DynamicParameters? MyParms { get; set; }
	public string? MySql { get; set; }

	public async Task<ReportVM?> GetDisplayAndDonationsById(int id)
	{
		string connectionString = config[configationConnectionKey]!;

		if (string.IsNullOrEmpty(connectionString))
		{
			string err = $"Inside {GetType().FullName}.{nameof(GetDisplayAndDonationsById)}; Connection string is null or empty.  configationConnectionKey={configationConnectionKey}";
			log.LogWarning(err!);
			throw new ArgumentException(err);
		}

		MyParms = new DynamicParameters(new { Id = id });
		MySql = $@"
		--DECLARE @id int= 1
SELECT TOP 1
Id, HouseRulesAgreementId
, FamilyName, FirstName, SpouseName, OtherNames
, Name, NameAndSpouse, NameAndSpouseWithOther
, EMail, Phone
, Adults, ChildBig, ChildSmall
, StatusId, Status, RegistrationFeeAdjusted
, AttendanceBitwise
, AttendanceTotal
, HouseRulesAgreementDate
, Notes
, LmmDonation
FROM Sukkot.vwRegistration WHERE Id = @id;

SELECT Id, Detail, Amount, Notes, ReferenceId, CreateDate, CreatedBy --, FamilyName
FROM Sukkot.vwDonationDetail 
WHERE RegistrationId=@Id
ORDER BY Detail
";

		ReportVM reportVM = new();

		using (var connection = new SqlConnection(connectionString))
		{
			using (var multi = await connection.QueryMultipleAsync(MySql, MyParms))
			{
				reportVM = await multi.ReadSingleOrDefaultAsync<ReportVM>();
				if (reportVM is not null)
				{
					var childItems = await multi.ReadAsync<vwDonationDetail>();
					reportVM.Donations = childItems.ToList();
				}
				return reportVM!;
			}
		}
	}


}

// Ignore Spelling: Parms Sql

/*

var parent = await multi.ReadSingleOrDefaultAsync<ReportVM>(); //FirstAsync<ReportVM>

					reportVM.Donations = await multi.ReadAsync<vwDonationDetail>(); //.ToList();

				using (var multi = connection.QueryMultiple(query, null))
				{
					int countA = multi.Read<int>().Single();
					int countB = multi.Read<int>().Single();
				}   

		return await WithConnectionAsync(async connection =>
		{
			//using (var multi = await connection.QueryMultipleAsync
			var multi = await connection.QueryMultipleAsync<SuperUser.Detail.ReportVM>(MySql: Sql, param: MyParms); //sql: Sql, param: Parms
		return multi;
		});



public RepositoryNoBase(IConfiguration config, ILogger<RepositoryNoBase> logger) : (config, logger)
		Parms = new DynamicParameters(new { Id = id });
		Sql = $@"


public async Task<Tuple<SuperUser.Detail.ReportVM, int, string>> GetDisplayAndDonationsById(int id)
return new Tuple<List<SuperUser.Detail.ReportVM>>, int, string>(reportVM, vwDonationDetail, DetailCount, SprocReturnValue, ReturnMsg);
	int DetailCount = 0;
int SprocReturnValue = 0;
string ReturnMsg = "";
*/
