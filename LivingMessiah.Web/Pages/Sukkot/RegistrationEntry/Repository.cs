using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SukkotAdmin.Data; // for BaseRepositoryAsync
using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.Detail;
using LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes.Domain;
using LivingMessiah.Web.Pages.Sukkot.Data;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry;

public interface IRepository
{
	string BaseSqlDump { get; }
	Task<List<SuperUser.Data.vwSuperUser>> GetAll();
	Task<SuperUser.Data.vwRegistration> GetById(int id);

	Task<List<RegistrationEntry.ViewModel>> GetAll2();
	Task<RegistrationEntry.ViewModel> GetById2(int id);

	Task<AddOrEdit.FormVM> GetAddOrEditId(int id);
	Task<Detail.DisplayVM> GetDisplayById(int id);
	Task<Tuple<int, int, string>> CreateRegistration(AddOrEdit.FormVM formVM);
	Task<Tuple<int, int, string>> UpdateRegistration(AddOrEdit.FormVM formVM);


	// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
	Task<Tuple<int, int, string>> Create(DTO registration);
	Task<Tuple<int, int, string>> Update(DTO registration);

	Task<int> InsertHouseRulesAgreement(string email, string timeZone);

	Task<int> Delete(int id);
	Task<int> DeleteHRA(int id);

	Task<List<RegistrationLookup>> PopulateRegistrationLookup(); // used by: EditRegistrationForm (SukkotAdmin.Registration)
}


public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger) : base(config, logger)
	{
	}

	public string BaseSqlDump
	{
		get { return base.SqlDump!; }
	}

	public async Task<List<RegistrationLookup>> PopulateRegistrationLookup()
	{
		base.Sql = $@"
SELECT Id AS ID, Sukkot.udfFormatName(1, FamilyName, FirstName, NULL, NULL) AS Text
FROM Sukkot.Registration
ORDER BY FirstName
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<RegistrationLookup>(base.Sql);
			return rows.ToList();
		});
	}

	public async Task<List<SuperUser.Data.vwSuperUser>> GetAll()
	{
		base.Sql = $@"
SELECT Id, EMail, FullName, StatusId, Phone, Notes
, IdHra
FROM Sukkot.vwSuperUser 
ORDER BY FullName
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<SuperUser.Data.vwSuperUser>(sql: base.Sql); 
			return rows.ToList();
		});
	}

	public async Task<SuperUser.Data.vwRegistration> GetById(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $@"
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
			var rows = await connection.QueryAsync<SuperUser.Data.vwRegistration>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault()!;
		});
	}


	public async Task<List<RegistrationEntry.ViewModel>> GetAll2()
	{
		base.Sql = $@"
SELECT Id, FamilyName, FirstName, SpouseName, OtherNames, EMail, Phone
, Adults, ChildBig, ChildSmall
, StatusId
--, AttendanceBitwise, Notes
--, LmmDonation, Avatar
FROM Sukkot.Registration
ORDER BY FirstName
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<RegistrationEntry.ViewModel>(sql: base.Sql);
			return rows.ToList();
		});
	}

	public async Task<AddOrEdit.FormVM> GetAddOrEditId(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $@"
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
			var rows = await connection.QueryAsync<AddOrEdit.FormVM>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault()!;
		});
	}



	public async Task<RegistrationEntry.ViewModel> GetById2(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $@"
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
			var rows = await connection.QueryAsync<RegistrationEntry.ViewModel>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault()!;
		});
	}

	public async Task<Detail.DisplayVM> GetDisplayById(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $@"
--DECLARE @id int=1
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
--, Avatar
FROM Sukkot.vwRegistration WHERE Id = @id";

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<DisplayVM>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault()!;
		});
	}


	public async Task<Tuple<int, int, string>> CreateRegistration(AddOrEdit.FormVM formVM)
	{
		base.Sql = "Sukkot.stpRegistrationInsert";
		base.Parms = new DynamicParameters(new
		{
			FamilyName = formVM.FamilyName,
			FirstName = formVM.FirstName,
			SpouseName = formVM.SpouseName,
			OtherNames = formVM.OtherNames,
			Email = formVM.EMail,
			Phone = formVM.Phone,
			Adults = formVM.Adults,
			ChildBig = formVM.ChildBig,
			ChildSmall = formVM.ChildSmall,
			StatusId = formVM.StatusId,
			AttendanceBitwise = formVM.AttendanceBitwise,
			LmmDonation = 0,
			Notes = formVM.Notes,
			Avatar = string.Empty
		});

		base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		base.Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug($"Inside {nameof(Repository)}!{nameof(CreateRegistration)}, {nameof(formVM.EMail)}:{formVM.EMail}; a about to execute SPROC: {base.Sql}");
			var affectedRows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			SprocReturnValue = base.Parms.Get<int>("ReturnValue");
			int? x = base.Parms.Get<int?>("NewId");
			if (x == null)
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not insert a new record because it caused a Unique Index Violation; registration.EMail: {formVM.EMail}; ";
					base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed; registration.EMail: {formVM.EMail ?? "NULL!!" }; SprocReturnValue: {SprocReturnValue}";
					base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
				}
			}
			else
			{
				NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				ReturnMsg = $"Registration created for {formVM.FamilyName}/{formVM.EMail}; NewId={NewId}";
				base.log.LogDebug($"...Return NewId:{NewId}");
			}

			return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

		});
	}

	public async Task<Tuple<int, int, string>> UpdateRegistration(AddOrEdit.FormVM formVM)
	{
		base.Sql = "Sukkot.stpRegistrationUpdate";
		base.Parms = new DynamicParameters(new
		{
			Id = formVM.Id,
			FamilyName = formVM.FamilyName,
			FirstName = formVM.FirstName,
			SpouseName = formVM.SpouseName,
			OtherNames = formVM.OtherNames,
			EMail = formVM.EMail,
			Phone = formVM.Phone,
			Adults = formVM.Adults,
			ChildBig = formVM.ChildBig,
			ChildSmall = formVM.ChildSmall,
			AttendanceBitwise = formVM.AttendanceBitwise,
			StatusId = formVM.StatusId,
			LmmDonation = formVM.LmmDonation,
			Notes = DTOHelper.Scrub(formVM.Notes),
			Avatar = string.Empty
		});

		base.Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int RowsAffected = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug($"Inside {nameof(Repository)}!{nameof(UpdateRegistration)}, {nameof(formVM.Id)}:{formVM.Id}; about to execute SPROC: {base.Sql}");
			RowsAffected = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			SprocReturnValue = base.Parms.Get<int>("ReturnValue");

			if (SprocReturnValue != 0) // ReturnValueOk
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not update the record because it caused a Unique Index Violation; formVM.EMail: {@formVM.EMail}; ";
					base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed; {nameof(formVM.Id)}:{formVM.Id}, {nameof(formVM.EMail)}:{formVM.EMail}; SprocReturnValue: {SprocReturnValue}";
					base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
				}
			}
			else
			{
				ReturnMsg = $"Registration updated for {formVM.FamilyName}/{formVM.EMail}";
			}

			return new Tuple<int, int, string>(RowsAffected, SprocReturnValue, ReturnMsg);
		});
	}




	public async Task<Tuple<int, int, string>> Create(DTO registration)
	{
		base.Sql = "Sukkot.stpRegistrationInsert";
		base.Parms = new DynamicParameters(new
		{
			FamilyName = registration.FamilyName,
			FirstName = registration.FirstName,
			SpouseName = registration.SpouseName,
			OtherNames = registration.OtherNames,
			Email = registration.EMail,
			Phone = registration.Phone,
			Adults = registration.Adults,
			ChildBig = registration.ChildBig,
			ChildSmall = registration.ChildSmall,
			StatusId = registration.StatusId,
			AttendanceBitwise = registration.AttendanceBitwise,
			LmmDonation = 0,
			Avatar = registration.Avatar,
			Notes = registration.Notes,
		});

		base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		base.Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug($"Inside {nameof(Repository)}!{nameof(Create)}, {nameof(registration.EMail)}:{registration.EMail}; a about to execute SPROC: {base.Sql}");
			var affectedRows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			SprocReturnValue = base.Parms.Get<int>("ReturnValue");
			int? x = base.Parms.Get<int?>("NewId");
			if (x == null)
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not insert a new record because it caused a Unique Index Violation; registration.EMail: {@registration.EMail}; ";
					base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed; registration.EMail: {@registration.EMail}; SprocReturnValue: {SprocReturnValue}";
					base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
				}
			}
			else
			{
				NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				ReturnMsg = $"Registration created for {registration.FamilyName}/{registration.EMail}; NewId={NewId}";
				base.log.LogDebug($"...Return NewId:{NewId}");
			}

			return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

		});
	}

	public async Task<Tuple<int, int, string>> Update(DTO registration)
	{
		base.Sql = "Sukkot.stpRegistrationUpdate";
		base.Parms = new DynamicParameters(new
		{
			Id = registration.Id,
			FamilyName = registration.FamilyName,
			FirstName = registration.FirstName,
			SpouseName = registration.SpouseName,
			OtherNames = registration.OtherNames,
			EMail = registration.EMail,
			Phone = registration.Phone,
			Adults = registration.Adults,
			ChildBig = registration.ChildBig,
			ChildSmall = registration.ChildSmall,
			AttendanceBitwise = registration.AttendanceBitwise,
			StatusId = registration.StatusId,
			LmmDonation = registration.LmmDonation,
			Notes = DTOHelper.Scrub(registration.Notes),
			Avatar = registration.Avatar
		});

		base.Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int RowsAffected = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug($"Inside {nameof(Repository)}!{nameof(Update)}, {nameof(registration.Id)}:{registration.Id}; about to execute SPROC: { base.Sql}");
			RowsAffected = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			SprocReturnValue = base.Parms.Get<int>("ReturnValue");

			if (SprocReturnValue != 0) // ReturnValueOk
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not update the record because it caused a Unique Index Violation; registration.EMail: {@registration.EMail}; ";
					base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed; {nameof(registration.Id)}:{registration.Id}, {nameof(registration.EMail)}:{registration.EMail}; SprocReturnValue: {SprocReturnValue}";
					base.log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {base.Sql}");
				}
			}
			else
			{
				ReturnMsg = $"Registration updated for {registration.FamilyName}/{registration.EMail}";
			}

			return new Tuple<int, int, string>(RowsAffected, SprocReturnValue, ReturnMsg);
		});
	}



	public async Task<int> InsertHouseRulesAgreement(string email, string timeZone)
	{
		base.Sql = "Sukkot.stpHouseRulesAgreementInsert";
		base.Parms = new DynamicParameters(new
		{
			EMail = email,
			TimeZone = timeZone
		});
		base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		return await WithConnectionAsync(async connection =>
		{
			base.log.LogDebug($"Inside {nameof(Repository)}!{nameof(InsertHouseRulesAgreement)}; About to execute sql:{base.Sql}");
			var affectedRows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			int? x = base.Parms.Get<int?>("NewId");
			if (x == null)
			{
				base.log.LogWarning($"NewId is null; returning as 0; Check dbo.ErrorLog for IX_HouseRulesAgreement_Unique  duplication Error; email: {email}");
				return 0;
			}
			else
			{
				int NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				base.log.LogDebug($"...Return NewId:{NewId}");
				return NewId;
			}
		});
	}

	public async Task<int> Delete(int id)
	{
		base.Sql = "Sukkot.stpRegistrationDelete";
		base.Parms = new DynamicParameters(new { RegistrationId = id });
		return await WithConnectionAsync(async connection =>
		{
			var affectedRows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			//if (affectedRows < 0) { throw new Exception($"Registration NOT Deleted"); }
			return affectedRows;
		});
	}


	public async Task<int> DeleteHRA(int id)
	{
		base.Sql = "Sukkot.stpHRADelete";
		base.Parms = new DynamicParameters(new { Id = id });
		return await WithConnectionAsync(async connection =>
		{
			var affectedRows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			return affectedRows;
		});
	}

}
