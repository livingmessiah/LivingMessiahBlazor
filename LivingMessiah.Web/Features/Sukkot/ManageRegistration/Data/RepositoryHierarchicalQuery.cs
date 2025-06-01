/*
I can't figure out how to make `BaseRepositoryAsync`  handle multiple queries like what `GetDisplayAndDonationsById` does.
See [explanation](LivingMessiah.Web\Data\Docs\DapperBaseRepository.md) for more details
*/
using Dapper;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Features.Sukkot.ManageRegistration.Data;

public interface IRepositoryHierarchicalQuery
{
	string BaseSqlDump { get; }
	Task<Detail.DetailAndDonationsHierarchicalQuery?> GetDisplayAndDonationsById(int id);
}

public class RepositoryHierarchicalQuery : BaseRepositoryHierarchicalQueryAsync, IRepositoryHierarchicalQuery
{
	const string configationConnectionKey = "ConnectionStrings:Sukkot";
	
	public string BaseSqlDump
	{
		get { return base.SqlDump ?? ""; }
	}


	#region Constructor
	private readonly IConfiguration config;
	protected readonly ILogger log;

	public RepositoryHierarchicalQuery(IConfiguration config, ILogger<RepositoryHierarchicalQuery> logger)
	{
		this.config = config;
		log = logger;
	}
	#endregion

	public async Task<Detail.DetailAndDonationsHierarchicalQuery?> GetDisplayAndDonationsById(int id)
	{
		string connectionString = config[configationConnectionKey]!;

		if (string.IsNullOrEmpty(connectionString))
		{
			string err = $"Inside {GetType().FullName}.{nameof(GetDisplayAndDonationsById)}; Connection string is null or empty.  configationConnectionKey={configationConnectionKey}";
			log.LogWarning(err!);
			throw new ArgumentException(err);
		}

		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $@"
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

		log.LogDebug(String.Format("Inside {0}, Sql={1}"
			, nameof(RepositoryHierarchicalQuery) + "!" + nameof(GetDisplayAndDonationsById), base.Sql));

		Detail.DetailAndDonationsHierarchicalQuery? qry = new();
		string errMsg = "";
		try
		{
			using (var connection = new SqlConnection(connectionString))
			{
				//using (var multi = await connection.QueryMultipleAsync(MySql, MyParms))
				using (var multi = await connection.QueryMultipleAsync(base.Sql, base.Parms))
				{
					qry = await multi.ReadSingleOrDefaultAsync<Detail.DetailAndDonationsHierarchicalQuery>();
					if (qry is not null)
					{
						var childItems = await multi.ReadAsync<DonationDetailQuery>();
						qry.Donations = childItems.ToList();
					}
					return qry!;
				}
			}

		}
		catch (TimeoutException ex)
		{
			errMsg = $"{GetType().FullName}.{nameof(GetDisplayAndDonationsById)} experienced a Sql Timeout <br /><br /> Sql...<br />[{SqlDump}] <br /><br />";
			log.LogError(ex, errMsg);
			throw new Exception(errMsg, ex);
		}
		catch (SqlException ex)
		{
			errMsg = $"{GetType().FullName}.{nameof(GetDisplayAndDonationsById)} experienced a Sql Exception <br /><br /> Sql...<br />[{SqlDump}] <br /><br />";
			log.LogError(ex, errMsg);
			if (ex.Message != null) { errMsg += "<br /> ex.Message:" + ex.Message; }
			throw new Exception(errMsg, ex);
		}
		catch (InvalidOperationException ex)
		{
			errMsg = $"{GetType().FullName}.{nameof(GetDisplayAndDonationsById)} experienced a Invalid Operation Exception <br /><br /> Sql...<br />[{SqlDump}] <br /><br />";
			log.LogError(ex, errMsg);
			throw new Exception(errMsg, ex);
		}
		catch (Exception ex)
		{
			errMsg = $"{GetType().FullName}.{nameof(GetDisplayAndDonationsById)} Generic Exception <br /><br /> Sql...<br />[{SqlDump}] <br /><br />";
			log.LogError(ex, errMsg);
			throw new Exception(errMsg, ex);
		}


	}

	public new string? Sql { get; set; }
	public DynamicParameters? Params { get; set; }  // using Dapper; Note, only place dependent on Dapper

	public new string? SqlDump
	{
		get
		{
			string s = "";
			s = Sql ?? "SQL IS NULL";
			if (Params != null)
			{
				/* See Notes in LivingMessiah.Data!BaseRepositoryAsync*/
			}
			return s;
		}
	}
}




// Ignore Spelling: Parms Sql

/*

var parent = await multi.ReadSingleOrDefaultAsync<ReportVM>(); //FirstAsync<ReportVM>

					qry.Donations = await multi.ReadAsync<DonationDetailQuery>(); //.ToList();

				using (var multi = connection.QueryMultiple(query, null))
				{
					int countA = multi.Read<int>().Single();
					int countB = multi.Read<int>().Single();
				}   

		return await WithConnectionAsync(async connection =>
		{
			//using (var multi = await connection.QueryMultipleAsync
			var multi = await connection.QueryMultipleAsync<ManageRegistration.Detail.ReportVM>(MySql: Sql, param: MyParms); //sql: Sql, param: Parms
		return multi;
		});



public RepositoryNoBase(IConfiguration config, ILogger<RepositoryNoBase> logger) : (config, logger)
		Parms = new DynamicParameters(new { Id = id });
		Sql = $@"


public async Task<Tuple<ManageRegistration.Detail.ReportVM, int, string>> GetDisplayAndDonationsById(int id)
return new Tuple<List<ManageRegistration.Detail.ReportVM>>, int, string>(qry, DonationDetailQuery, DetailCount, SprocReturnValue, ReturnMsg);
	int DetailCount = 0;
int SprocReturnValue = 0;
string ReturnMsg = "";
*/
