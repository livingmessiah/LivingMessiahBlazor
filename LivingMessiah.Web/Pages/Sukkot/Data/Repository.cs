using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.Sukkot.Services; // needed for DTO.cs

using LivingMessiah.Web.Pages.Sukkot.Enums;
using LivingMessiah.Web.Pages.SukkotAdmin.Data; // for BaseRepositoryAsync
using LivingMessiah.Web.Pages.Sukkot.SuperUser.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data;
using LivingMessiah.Web.Pages.Sukkot.NormalUser;
using Serilog.Core;
using System.Data.SqlClient;

//using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data;
//using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain;

namespace LivingMessiah.Web.Pages.Sukkot.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

	// Used by FluxorStore
	Task<List<SuperUser.Data.vwSuperUser>> GetAll();
	Task<SuperUser.Registrant.FormVM> GetAddOrEditId(int id);
	Task<Tuple<int, int, string>> CreateRegistration(SuperUser.Registrant.FormVM formVM);
	Task<Tuple<int, int, string>> UpdateRegistration(SuperUser.Registrant.FormVM formVM);
	Task<Tuple<int, int, string>> DeleteRegistration(int id);

	Task<Tuple<int, int, string>> InsertHouseRulesAgreement(string email, string timeZone);  // Also used by RegistrationSteps!AgreementButtons
	Task<int> DeleteHRA(int id);  // stpHRADelete

	Task<List<SuperUser.Data.vwDonationDetail>> GetByRegistrationId(int registrationId);
	Task<Tuple<int, int, string>> InsertRegistrationDonation(SuperUser.Donations.FormVM donation); //SuperUser.Data.Donation donation
	Task<int> DeleteDonationDetail(int id);

	// Used by Services
	Task<EntryFormVM> GetById2(int id);   //ViewModel_RE_DELETE
	Task<Tuple<int, int, string>> Create(DTO registration);
	Task<Tuple<int, int, string>> Update(DTO registration);

}


public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger) : base(config, logger)
	{
	}

	public string BaseSqlDump
	{
		get { return SqlDump!; }
	}

	#region Registration used by FluxorStore

	public async Task<List<SuperUser.Data.vwSuperUser>> GetAll()
	{
		Sql = $@"
SELECT Id, EMail, FullName, StatusId, Phone, Notes
, TotalDonation
, IdHra
FROM Sukkot.vwSuperUser 
ORDER BY FullName
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<SuperUser.Data.vwSuperUser>(sql: Sql);
			return rows.ToList();
		});
	}

	public async Task<SuperUser.Registrant.FormVM> GetAddOrEditId(int id)
	{
		Parms = new DynamicParameters(new { Id = id });
		Sql = $@"
		--DECLARE @id int= 4
SELECT
Id, FamilyName, FirstName, SpouseName, OtherNames
, EMail, Phone, Adults, ChildBig, ChildSmall
, StatusId
, AttendanceBitwise
, Notes
, LmmDonation
FROM Sukkot.Registration
WHERE Id = @Id";

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<SuperUser.Registrant.FormVM>(sql: Sql, param: Parms);
			return rows.SingleOrDefault()!;
		});
	}

	public async Task<Tuple<int, int, string>> CreateRegistration(SuperUser.Registrant.FormVM formVM)
	{
		Sql = "Sukkot.stpRegistrationInsert";
		Parms = new DynamicParameters(new
		{
			formVM.FamilyName,
			formVM.FirstName,
			formVM.SpouseName,
			formVM.OtherNames,
			Email = formVM.EMail,
			formVM.Phone,
			formVM.Adults,
			formVM.ChildBig,
			formVM.ChildSmall,
			formVM.StatusId,
			AttendanceBitwise = Helper.GetDaysBitwise(formVM.AttendanceDateList!, formVM.AttendanceDateList2ndMonth!, DateRangeType.Attendance),
			LmmDonation = 0,
			formVM.Notes,
			Avatar = string.Empty
		});

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
		return await WithConnectionAsync(async connection =>
		{
			string inside = $"{nameof(Repository)}!{nameof(CreateRegistration)}, Email: {formVM.EMail}; about to execute SPROC: {Sql}";
			log.LogDebug(string.Format("Inside {0}", inside));

			var affectedRows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");
			int? x = Parms.Get<int?>("NewId");
			if (x == null)
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not insert a new record because it caused a Unique Index Violation; registration.EMail: {formVM.EMail}; ";
					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed; registration.EMail: {formVM.EMail ?? "NULL!!"}; SprocReturnValue: {SprocReturnValue}";
					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
			}
			else
			{
				NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				ReturnMsg = $"Registration created for {formVM.FamilyName}/{formVM.EMail}; NewId={NewId}";
				log.LogDebug($"...Return NewId:{NewId}");
			}

			return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

		});
	}

	public async Task<Tuple<int, int, string>> UpdateRegistration(SuperUser.Registrant.FormVM formVM)
	{
		Sql = "Sukkot.stpRegistrationUpdate";
		Parms = new DynamicParameters(new
		{
			formVM.Id,
			formVM.FamilyName,
			formVM.FirstName,
			formVM.SpouseName,
			formVM.OtherNames,
			formVM.EMail,
			formVM.Phone,
			formVM.Adults,
			formVM.ChildBig,
			formVM.ChildSmall,
			AttendanceBitwise = Helper.GetDaysBitwise(formVM.AttendanceDateList!, formVM.AttendanceDateList2ndMonth!, DateRangeType.Attendance),
			formVM.StatusId,
			formVM.LmmDonation,
			Notes = DTOHelper.Scrub(formVM.Notes),
			Avatar = string.Empty
		});

		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int RowsAffected = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
		return await WithConnectionAsync(async connection =>
		{
			string inside = $"{nameof(Repository)}!{nameof(UpdateRegistration)}, Id: {formVM.Id}; Email: {formVM.EMail}; about to execute SPROC: {Sql}";
			log.LogDebug(string.Format("Inside {0}", inside));
			RowsAffected = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");

			if (SprocReturnValue != 0) // ReturnValueOk
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not update the record because it caused a Unique Index Violation; formVM.EMail: {@formVM.EMail}; ";
					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed; {nameof(formVM.Id)}:{formVM.Id}, {nameof(formVM.EMail)}:{formVM.EMail}; SprocReturnValue: {SprocReturnValue}";
					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
			}
			else
			{
				ReturnMsg = $"Registration updated for {formVM.FamilyName}/{formVM.EMail}";
			}

			return new Tuple<int, int, string>(RowsAffected, SprocReturnValue, ReturnMsg);
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
	#endregion


	#region Donation

	public async Task<List<vwDonationDetail>> GetByRegistrationId(int registrationId)
	{
		Sql = $@"
-- DECLARE @registrationId int = 20
SELECT Id, Detail, Amount, Notes, ReferenceId, CreateDate, CreatedBy --, FamilyName
FROM Sukkot.vwDonationDetail 
WHERE RegistrationId=@registrationId
ORDER BY Detail
";
		base.Parms = new DynamicParameters(new { RegistrationId = registrationId });

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwDonationDetail>(sql: Sql, param: Parms);
			return rows.ToList();
		});
	}

	public async Task<Tuple<int, int, string>> InsertRegistrationDonation(SuperUser.Donations.FormVM donation)
	{
		base.Sql = "Sukkot.stpDonationInsert ";
		base.Parms = new DynamicParameters(new
		{
			donation.RegistrationId,
			donation.Amount,
			donation.Notes,
			donation.ReferenceId,
			donation.CreatedBy,
			donation.CreateDate
		});

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		string inside = $"{nameof(Repository)}!{nameof(InsertRegistrationDonation)}; about to execute SPROC: {Sql}";
		log.LogDebug(string.Format("Inside {0}", inside));

		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");
			int? x = base.Parms.Get<int?>("NewId");
			if (x == null)
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not insert a new donation record because it caused a Unique Index Violation; donation.RegistrationId: {donation.RegistrationId}; ";
					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed for donation insert; donation.RegistrationId: {donation.RegistrationId}; SprocReturnValue: {SprocReturnValue}";
					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}

			}
			else
			{
				int NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				ReturnMsg = $"Donation created for {donation.RegistrationId}; NewId={NewId}";
				log.LogDebug($"Return NewId:{NewId}");

			}

			return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);
		});
	}

	public async Task<int> DeleteDonationDetail(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = "DELETE FROM Sukkot.Donation WHERE Id=@Id";

		base.log.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(DeleteDonationDetail)}, Sql: {Sql}, id: {id}");

		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return affectedrows;
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
			Notes = DTOHelper.Scrub(registration.Notes),
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
