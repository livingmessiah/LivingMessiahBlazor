using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.Admin.VideoMasterDetail.MasterDetail;
using LivingMessiah.Data;  // for public string BaseSqlDump { get { return SqlDump!; } }

namespace LivingMessiah.Web.Pages.Admin.VideoMasterDetail.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

	//Task<List<vwSuperUser>> GetAll();
	//Task<VideoMasterDetail.AddEdit.FormVM> Get(int id);

	Task<Tuple<int, int, string>> WeeklyVideoInsert(VideoMasterDetail.AddEdit.FormVM formVM);
	//Task<Tuple<int, int, string>> UpdateRegistration(VideoMasterDetail.AddEdit.FormVM formVM);
	//Task<Tuple<int, int, string>> DeleteRegistration(int id);
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

	//	#region Registration used by FluxorStore

	//	public async Task<List<vwSuperUser>> GetAll()
	//	{
	//		Sql = $@"
	//SELECT Id, EMail, FullName, StatusId, Phone, Notes
	//, TotalDonation, DonationRowCount
	//, IdHra
	//FROM Sukkot.vwSuperUser 
	//ORDER BY FullName
	//";
	//		return await WithConnectionAsync(async connection =>
	//		{
	//			var rows = await connection.QueryAsync<vwSuperUser>(sql: Sql);
	//			return rows.ToList();
	//		});
	//	}

	//	public async Task<VideoMasterDetail.AddEdit.FormVM> Get(int id)
	//	{
	//		Parms = new DynamicParameters(new { Id = id });
	//		Sql = $@"
	//		--DECLARE @id int= 4
	//SELECT
	//Id, FamilyName, FirstName, SpouseName, OtherNames
	//, EMail, Phone, Adults, ChildBig, ChildSmall
	//, StatusId
	//, AttendanceBitwise
	//, Notes
	//, LmmDonation
	//FROM Sukkot.Registration
	//WHERE Id = @Id";

	//		return await WithConnectionAsync(async connection =>
	//		{
	//			var rows = await connection.QueryAsync<VideoMasterDetail.AddEdit.FormVM>(sql: Sql, param: Parms);
	//			return rows.SingleOrDefault()!;
	//		});
	//	}

	public async Task<Tuple<int, int, string>> WeeklyVideoInsert(VideoMasterDetail.AddEdit.FormVM formVM)
	{
		Sql = "dbo.stpWeeklyVideoAddInsert";
		Parms = new DynamicParameters(new
		{
			formVM.ShabbatWeekId,
			formVM.WeeklyVideoTypeId,
			formVM.@YouTubeId,
			formVM.@Title,
			formVM.@Book,
			formVM.@Chapter
		});

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
		return await WithConnectionAsync(async connection =>
		{
			string parameters = $"ShabbatWeekId: {formVM.ShabbatWeekId}, WeeklyVideoTypeId: {formVM.WeeklyVideoTypeId}";
			string inside = $"{nameof(Repository)}!{nameof(WeeklyVideoInsert)}; about to execute SPROC: {Sql}";
			log.LogDebug(string.Format("Inside {0}, parameters:{1}", inside, parameters));

			var affectedRows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");
			int? x = Parms.Get<int?>("NewId");
			if (x is null)
			{
				//if (SprocReturnValue == 0) 
				//{
					ReturnMsg = $"Database call failed; video.ShabbatWeekId: {formVM.ShabbatWeekId}; SprocReturnValue: {SprocReturnValue}";
					log.LogWarning(string.Format("inside {0}, NewId is null, parameters:{1}, ReturnMsg:{2}, {3} Sql: {4}"
						, inside, parameters, ReturnMsg, Environment.NewLine, Sql));
				//}
			}
			else
			{
				NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				ReturnMsg = $"video created for {formVM.ShabbatWeekId}/{formVM.WeeklyVideoTypeId}; NewId={NewId}";
				log.LogDebug(string.Format("...NewId {0}", NewId));
			}

			return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

		});
	}

	//	public async Task<Tuple<int, int, string>> UpdateRegistration(VideoMasterDetail.AddEdit.FormVM formVM)
	//	{
	//		Sql = "Sukkot.stpRegistrationUpdate";
	//		Parms = new DynamicParameters(new
	//		{
	//			formVM.
	//			formVM.FamilyName,
	//			formVM.FirstName,
	//			formVM.SpouseName,
	//			formVM.OtherNames,
	//			formVM.EMail,
	//			formVM.Phone,
	//			formVM.Adults,
	//			formVM.ChildBig,
	//			formVM.ChildSmall,
	//			formVM.StatusId,
	//			formVM.LmmDonation,
	//			Notes = DTOHelper.Scrub(formVM.Notes),
	//			Avatar = string.Empty
	//		});

	//		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

	//		int RowsAffected = 0;
	//		int SprocReturnValue = 0;
	//		string ReturnMsg = "";

	//		// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
	//		return await WithConnectionAsync(async connection =>
	//		{
	//			string inside = $"{nameof(Repository)}!{nameof(UpdateRegistration)}, Id: {formVM.Id}; Email: {formVM.EMail}; about to execute SPROC: {Sql}";
	//			log.LogDebug(string.Format("Inside {0}", inside));
	//			RowsAffected = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
	//			SprocReturnValue = Parms.Get<int>("ReturnValue");

	//			if (SprocReturnValue != 0) // ReturnValueOk
	//			{
	//				if (SprocReturnValue == 2601) // Unique Index Violation
	//				{
	//					ReturnMsg = $"Database call did not update the record because it caused a Unique Index Violation; formVM.EMail: {@formVM.EMail}; ";
	//					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
	//				}
	//				else
	//				{
	//					ReturnMsg = $"Database call failed; {nameof(formVM.Id)}:{formVM.Id}, {nameof(formVM.EMail)}:{formVM.EMail}; SprocReturnValue: {SprocReturnValue}";
	//					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
	//				}
	//			}
	//			else
	//			{
	//				ReturnMsg = $"Registration updated for {formVM.FamilyName}/{formVM.EMail}";
	//			}

	//			return new Tuple<int, int, string>(RowsAffected, SprocReturnValue, ReturnMsg);
	//		});
	//	}

	//	public async Task<Tuple<int, int, string>> DeleteRegistration(int id)
	//	{
	//		Sql = "Sukkot.stpRegistrationDelete";
	//		Parms = new DynamicParameters(new { RegistrationId = id });

	//		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

	//		int RowsAffected = 0;
	//		int SprocReturnValue = 0;
	//		string ReturnMsg = "";

	//		return await WithConnectionAsync(async connection =>
	//		{
	//			string inside = $"{nameof(Repository)}!{nameof(DeleteRegistration)}, RegistrationId: {id}; about to execute SPROC: {Sql}";
	//			log.LogDebug(string.Format("Inside {0}", inside));
	//			RowsAffected = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
	//			SprocReturnValue = Parms.Get<int>("ReturnValue");

	//			if (SprocReturnValue != 0) // ReturnValueOk
	//			{
	//				if (SprocReturnValue == 51000) // Can not have donation rows when deleting registration
	//				{
	//					ReturnMsg = $"Database call did not delete the registration record because it has donation rows; RegistrationId: {id}; Manually delete the donation row(s) then delete the registration.";
	//					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
	//				}
	//				else
	//				{
	//					ReturnMsg = $"Database call failed to delete RegistrationId: {id}; SprocReturnValue: {SprocReturnValue}";
	//					log.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
	//				}
	//			}
	//			else
	//			{
	//				ReturnMsg = $"Registration deleted for RegistrationId: {id}";
	//			}

	//			return new Tuple<int, int, string>(RowsAffected, SprocReturnValue, ReturnMsg);

	//		});
	//	}

	//	#endregion

}

/*
# Footnotes

FN1. Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md

*/
