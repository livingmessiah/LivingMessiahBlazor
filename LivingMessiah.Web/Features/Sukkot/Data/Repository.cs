using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Features.Sukkot.Services; // needed for DTO.cs
using LivingMessiah.Web.Features.Sukkot.NormalUser;

using LivingMessiah.Web.Data;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;

namespace LivingMessiah.Web.Features.Sukkot.Data;

public interface IRepository
{
	string BaseSqlDump { get; }


	// used by both ManageRegistration &  RegistrationStep
	Task<Tuple<int, int, string>> InsertHouseRulesAgreement(string email, string timeZone);  // Also used by RegistrationSteps!AgreementButtons
	Task<int> DeleteHRA(int id);  // stpHRADelete
	Task<Tuple<int, int, string>> DeleteRegistration(int id);

	// Used by Sukkot\Services\Service
	Task<EntryFormVM> GetById2(int id);   //ViewModel_RE_DELETE
	Task<Tuple<int, int, string>> Create(DTO registration);
	Task<Tuple<int, int, string>> Update(DTO registration);
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

	#region HRA
	public async Task<Tuple<int, int, string>> InsertHouseRulesAgreement(string email, string timeZone)
	{
		Sql = "Sukkot.stpHouseRulesAgreementInsert";
		Parms = new DynamicParameters(new
		{
			EMail = email,
			TimeZone = timeZone
		});
		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			log.LogDebug($"Inside {nameof(Repository)}!{nameof(InsertHouseRulesAgreement)}; About to execute sql:{Sql}");
			var affectedRows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = base.Parms.Get<int>("ReturnValue");
			int? x = Parms.Get<int?>("NewId");

			if (x == null)
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not insert a new HRA record because it caused a Unique Index Violation; email: {email}; ";
					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed; email: {email ?? "NULL!!"}; SprocReturnValue: {SprocReturnValue}";
					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
			}
			else
			{
				NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				ReturnMsg = $"House Rules Agreement created for {email}; NewId={NewId}";
				log.LogDebug($"...Return NewId:{NewId}");
			}

			return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

		});
	}

	public async Task<int> DeleteHRA(int id)
	{
		Sql = "Sukkot.stpHRADelete";
		Parms = new DynamicParameters(new { Id = id });
		return await WithConnectionAsync(async connection =>
		{
			var affectedRows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			return affectedRows;
		});
	}

	public async Task<Tuple<int, int, string>> DeleteRegistration(int id)
	{
		Sql = "Sukkot.stpRegistrationDelete";
		Parms = new DynamicParameters(new { RegistrationId = id });

		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int RowsAffected = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			string inside = $"{nameof(Repository)}!{nameof(DeleteRegistration)}, RegistrationId: {id}; about to execute SPROC: {Sql}";
			log.LogDebug(string.Format("Inside {0}", inside));
			RowsAffected = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");

			if (SprocReturnValue != 0) // ReturnValueOk
			{
				if (SprocReturnValue == 51000) // Can not have donation rows when deleting registration
				{
					ReturnMsg = $"Database call did not delete the registration record because it has donation rows; RegistrationId: {id}; Manually delete the donation row(s) then delete the registration.";
					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed to delete RegistrationId: {id}; SprocReturnValue: {SprocReturnValue}";
					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
			}
			else
			{
				ReturnMsg = $"Registration deleted for RegistrationId: {id}";
			}

			return new Tuple<int, int, string>(RowsAffected, SprocReturnValue, ReturnMsg);

		});
	}

	#endregion



	#region Registration used by Service

	public async Task<EntryFormVM> GetById2(int id)  //ViewModel_RE_DELETE
	{
		Parms = new DynamicParameters(new { Id = id });
		Sql = $@"
--DECLARE @id int=4
SELECT TOP 1 
Id, FamilyName, FirstName, SpouseName, OtherNames, EMail, Phone, Adults, ChildBig, ChildSmall
, StatusId
, AttendanceBitwise, LmmDonation, Notes
--, Avatar
FROM Sukkot.Registration 
WHERE Id = @Id";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<EntryFormVM>(sql: Sql, param: Parms);  //ViewModel_RE_DELETE
			return rows.SingleOrDefault()!;
		});
	}

	public async Task<Tuple<int, int, string>> Create(DTO registration)
	{
		Sql = "Sukkot.stpRegistrationInsert";
		Parms = new DynamicParameters(new
		{
			registration.FamilyName,
			registration.FirstName,
			registration.SpouseName,
			registration.OtherNames,
			Email = registration.EMail,
			registration.Phone,
			registration.Adults,
			registration.ChildBig,
			registration.ChildSmall,
			registration.StatusId,
			registration.AttendanceBitwise,
			LmmDonation = 0,
			registration.Avatar,
			registration.Notes,
		});

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
		return await WithConnectionAsync(async connection =>
		{
			log.LogDebug($"Inside {nameof(Repository)}!{nameof(Create)}, {nameof(registration.EMail)}:{registration.EMail}; a about to execute SPROC: {Sql}");
			var affectedRows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");
			int? x = Parms.Get<int?>("NewId");
			if (x == null)
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not insert a new record because it caused a Unique Index Violation; registration.EMail: {@registration.EMail}; ";
					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed; registration.EMail: {@registration.EMail}; SprocReturnValue: {SprocReturnValue}";
					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
			}
			else
			{
				NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				ReturnMsg = $"Registration created for {registration.FamilyName}/{registration.EMail}; NewId={NewId}";
				log.LogDebug($"...Return NewId:{NewId}");
			}

			return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

		});
	}

	public async Task<Tuple<int, int, string>> Update(DTO registration)
	{
		Sql = "Sukkot.stpRegistrationUpdate";
		Parms = new DynamicParameters(new
		{
			registration.Id,
			registration.FamilyName,
			registration.FirstName,
			registration.SpouseName,
			registration.OtherNames,
			registration.EMail,
			registration.Phone,
			registration.Adults,
			registration.ChildBig,
			registration.ChildSmall,
			registration.AttendanceBitwise,
			registration.StatusId,
			registration.LmmDonation,
			Notes = LivingMessiah.Web.Data.Helper.Scrub(registration.Notes),
			registration.Avatar
		});

		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int RowsAffected = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
		return await WithConnectionAsync(async connection =>
		{
			log.LogDebug($"Inside {nameof(Repository)}!{nameof(Update)}, {nameof(registration.Id)}:{registration.Id}; about to execute SPROC: {Sql}");
			RowsAffected = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");

			if (SprocReturnValue != 0) // ReturnValueOk
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not update the record because it caused a Unique Index Violation; registration.EMail: {@registration.EMail}; ";
					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed; {nameof(registration.Id)}:{registration.Id}, {nameof(registration.EMail)}:{registration.EMail}; SprocReturnValue: {SprocReturnValue}";
					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
			}
			else
			{
				ReturnMsg = $"Registration updated for {registration.FamilyName}/{registration.EMail}";
			}

			return new Tuple<int, int, string>(RowsAffected, SprocReturnValue, ReturnMsg);
		});
	}


	#endregion


}

/*
# Footnotes

FN1. Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md

*/
