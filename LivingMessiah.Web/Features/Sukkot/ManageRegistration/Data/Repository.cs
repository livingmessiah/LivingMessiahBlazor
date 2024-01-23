using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Features.Sukkot.Enums;
using LivingMessiah.Web.Features.SukkotAdmin.Donations.Data;
using LivingMessiah.Web.Features.Sukkot.ManageRegistration.MasterDetail;

using SukkotEnumsHelper = LivingMessiah.Web.Features.Sukkot.Enums.Helper;
using DataEnumsDatabase = LivingMessiah.Web.Data.Enums.Database;
using LivingMessiah.Web.Data;

namespace LivingMessiah.Web.Features.Sukkot.ManageRegistration.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

	Task<List<ManageRegistrationQuery>> GetAll();
	Task<ManageRegistration.Registrant.FormVM> Get(int id);

	Task<Tuple<int, int, string>> CreateRegistration(ManageRegistration.Registrant.FormVM formVM);
	Task<Tuple<int, int, string>> UpdateRegistration(ManageRegistration.Registrant.FormVM formVM);

	Task<List<ManageRegistration.Data.DonationDetailQuery>> GetByRegistrationId(int registrationId);
	Task<Tuple<int, int, string>> InsertRegistrationDonation(ManageRegistration.Donations.FormVM donation); //ManageRegistration.Data.Donation donation
	Task<int> DeleteDonationDetail(int id);
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


	#region Registration used by FluxorStore

	public async Task<List<ManageRegistrationQuery>> GetAll()
	{
		Sql = $@"
SELECT Id, EMail, FullName, StatusId, Phone, Notes, AdminNotes, DidNotAttend
, TotalDonation, DonationRowCount
, IdHra
FROM Sukkot.vwManageRegistration
ORDER BY FullName
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<ManageRegistrationQuery>(sql: Sql);
			return rows.ToList();
		});
	}

	public async Task<ManageRegistration.Registrant.FormVM> Get(int id)
	{
		Parms = new DynamicParameters(new { Id = id });
		Sql = $@"
		--DECLARE @id int= 4
SELECT
Id, FamilyName, FirstName, SpouseName, OtherNames
, EMail, Phone, Adults, ChildBig, ChildSmall
, StatusId
, AttendanceBitwise
, Notes, AdminNotes, DidNotAttend
, LmmDonation
FROM Sukkot.Registration
WHERE Id = @Id";

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<ManageRegistration.Registrant.FormVM>(sql: Sql, param: Parms);
			return rows.SingleOrDefault()!;
		});
	}

	public async Task<Tuple<int, int, string>> CreateRegistration(ManageRegistration.Registrant.FormVM formVM)
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
			AttendanceBitwise = SukkotEnumsHelper.GetDaysBitwise(formVM.AttendanceDateList!, formVM.AttendanceDateList2ndMonth!, DateRangeType.Attendance),
			LmmDonation = 0,
			formVM.Notes,
			formVM.AdminNotes,
			formVM.DidNotAttend,
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

	public async Task<Tuple<int, int, string>> UpdateRegistration(ManageRegistration.Registrant.FormVM formVM)
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
			AttendanceBitwise = SukkotEnumsHelper.GetDaysBitwise(formVM.AttendanceDateList!, formVM.AttendanceDateList2ndMonth!, DateRangeType.Attendance),
			formVM.StatusId,
			formVM.LmmDonation,
			Notes = LivingMessiah.Web.Data.Helper.Scrub(formVM.Notes),
			AdminNotes = LivingMessiah.Web.Data.Helper.Scrub(formVM.AdminNotes),
			formVM.DidNotAttend,
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

			log!.LogWarning($" Notes: {formVM.Notes}");
			log!.LogWarning($" AdminNotes: {formVM.AdminNotes}");
			log!.LogWarning($" DidNotAttend: {formVM.DidNotAttend}");


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

	#endregion


	#region Donation

	public async Task<List<DonationDetailQuery>> GetByRegistrationId(int registrationId)
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
			var rows = await connection.QueryAsync<DonationDetailQuery>(sql: Sql, param: Parms);
			return rows.ToList();
		});
	}

	public async Task<Tuple<int, int, string>> InsertRegistrationDonation(ManageRegistration.Donations.FormVM donation)
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



}
