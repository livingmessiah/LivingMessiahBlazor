using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SukkotApi.Domain;
using SukkotApi.Domain.Registrations.Enums;

namespace SukkotApi.Data;


public interface ISukkotAdminRepository
{
	Task<List<vwRegistration>> GetAll(RegistrationSortEnum sort, bool isAscending);
	Task<List<Notes>> GetNotes(RegistrationSortEnum sort);

	//ToDo: 
	Task<int> LogErrorTest();
	Task<List<zvwErrorLog>> GetzvwErrorLog();
	Task<int> EmptyErrorLog();

	Task<List<vwMealTicket>> GetMealTickets(int mealDateTimeId, bool selectAll);
	Task<List<vwMealTicketPunchLogPivot>> GetMealTicketPunchLogPivots(int mealDateTimeId);
	Task<int> MealTicketPunchInsert(MealTicketPunchLog mealTicketPunchLog);
	Task<MealDateTime> GetMealDateTime(MealTicketEnum mealTicketEnum);
	Task<List<vwKitchenWork>> GetKitchenWorkList(int mealDateTimeId);
	Task<KitchenWork> GetKitchenWork(int id);

	Task<List<vwMealPlannerReport>> GetMealPlanner();

	Task<List<vwAttendanceAllFeastDays>> GetAttendanceAllFeastDays();
	Task<vwAttendancePeopleSummary> GetAttendancePeopleSummary();
	Task<List<vwAttendanceChart>> GetAttendanceChart();

	Task<List<vwMealPlanMenu>> ListMealPlans();
	Task<MealPlan> GetMealPlan(int Id);
	Task<int> EditMealPlan(MealPlan mealPlan);
	Task<int> UpdateKitchenWork(KitchenWork kitchenWork);
}

public class SukkotAdminRepository : BaseRepositoryAsync, ISukkotAdminRepository
{

	public SukkotAdminRepository(IConfiguration config, ILogger<SukkotAdminRepository> logger) : base(config, logger)
	{
	}

	public async Task<List<vwRegistration>> GetAll(RegistrationSortEnum sort, bool isAscending)
	{
		base.log.LogDebug(string.Format("Inside {0} , sort:{1}, isAscending: {2}"
				, nameof(SukkotAdminRepository) + "!" + nameof(GetAll), sort, isAscending));
		string sortField = sort switch
		{
			RegistrationSortEnum.Id => "Id",
			RegistrationSortEnum.LastName => "FamilyName",
			RegistrationSortEnum.FirstName => "FirstName",
			_ => "Id",
		};

		sortField += isAscending ? "" : " DESC";

		base.Sql = $@"
SELECT TOP 500 Id, FamilyName, FirstName, SpouseName, OtherNames
, EMail, Phone, Adults, ChildBig, ChildSmall, WillHelpWithMeals
, CampId, StatusId, CampCD, StatusCD, Camp, Status, Notes
, RegistrationFee, LmmDonation
, AttendanceBitwise, AttendanceTotal
,	TotalAdultLun, TotalAdultDin, TotalChildBigLun, TotalChildBigDin, TotalChildSmallLun, TotalChildSmallDin
,	TotalAdultLunVeg, TotalAdultDinVeg, TotalChildBigLunVeg, TotalChildBigDinVeg, TotalChildSmallLunVeg, TotalChildSmallDinVeg
FROM Sukkot.vwRegistration
ORDER BY {sortField}
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwRegistration>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});

		//https://stackoverflow.com/questions/17840526/dapper-order-by-parameters
		//http://bobby-tables.com/
		//https://xkcd.com/327/
		//base.Parms = new DynamicParameters(new { SortField = sortField });
		//ORDER BY @SortField

	}


	public async Task<List<Notes>> GetNotes(RegistrationSortEnum sort)
	{
		string sortField = (sort == RegistrationSortEnum.LastName) ? "FamilyName" : "Id";

		base.Sql = $@"
SELECT TOP 500 Id, FirstName, FamilyName, Notes AS UserNotes, CampCD, Phone, EMail
FROM Sukkot.vwRegistration
WHERE Notes IS NOT NULL AND TRIM(Notes) <> ''
ORDER BY {sortField}
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Notes>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}


	public async Task<int> LogErrorTest()
	{
		base.Sql = "dbo.stpLogErrorTest ";
		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql, commandType: System.Data.CommandType.StoredProcedure);
			return count;
		});
	}

	public async Task<List<zvwErrorLog>> GetzvwErrorLog()
	{
		base.Sql = $@"SELECT TOP 75 * FROM zvwErrorLog ORDER BY ErrorLogID DESC";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<zvwErrorLog>(sql: base.Sql);
			return rows.ToList();
		});
	}

	public async Task<int> EmptyErrorLog()
	{
		base.Sql = "dbo.stpLogErrorEmpty";
		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, commandType: System.Data.CommandType.StoredProcedure);
			return affectedrows;
		});
	}

	public async Task<List<vwMealTicket>> GetMealTickets(int mealDateTimeId, bool selectAll)
	{

		// RegistrationId, MealDateTimeId, AdultReg, AdultVeg, Child6to9Reg, Child6to9Veg, ChildUnder6Reg, ChildUnder6Veg
		string Selected = selectAll ? " 1 AS Selected" : " 0 AS Selected";
		base.Parms = new DynamicParameters(new { MealDateTimeId = mealDateTimeId });
		base.Sql = $@"
SELECT *, {Selected}
FROM Sukkot.vwMealDateTimeFilter
PIVOT(SUM(Meals)
FOR PivotHeading IN ([AdultReg], [AdultVeg], [Child6to9Reg], [Child6to9Veg], [ChildUnder6Reg], [ChildUnder6Veg])) AS PVTTable
WHERE MealDateTimeId=@mealDateTimeId
ORDER BY NameAndSpouse  
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwMealTicket>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}

	public async Task<List<vwMealTicketPunchLogPivot>> GetMealTicketPunchLogPivots(int mealDateTimeId)
	{
		base.Parms = new DynamicParameters(new { MealDateTimeId = mealDateTimeId });
		base.Sql = $@"
SELECT *
FROM Sukkot.vwMealTicketPunchLogPivot 
PIVOT(SUM(PunchCount)
FOR PivotHeading IN ([AdultReg], [AdultVeg], [Child6to9Reg], [Child6to9Veg], [ChildUnder6Reg], [ChildUnder6Veg])) AS PVTTable
WHERE MealDateTimeId=@mealDateTimeId
ORDER BY RegistrationId  
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwMealTicketPunchLogPivot>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}


	//ToDo: Duplicate
	/* Message	IDE0037	Member name can be simplified */
	public async Task<int> MealTicketPunchInsert(MealTicketPunchLog mealTicketPunchLog)
	{
		base.Sql = "Sukkot.stpMealTicketPunchLogInsert";
		base.Parms = new DynamicParameters(new
		{
			RegistrationId = mealTicketPunchLog.RegistrationId,
			MealDateTimeId = mealTicketPunchLog.MealDateTimeId,
			MealTypeId = mealTicketPunchLog.MealTypeId,
			AgeId = mealTicketPunchLog.AgeId,
			PunchCount = mealTicketPunchLog.PunchCount
		});

		base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
			int newID = base.Parms.Get<int>("NewId");
			return newID;
		});
	}

	public async Task<List<vwMealPlannerReport>> GetMealPlanner()
	{
		base.Sql = $@"
SELECT 
	MealDay, BruOrDin, MealTypeDescr, Menu, Adult, ChildBig, ChildSmall, TotalMeals
-- MealDateTimeId, MealTypeId 
FROM Sukkot.vwMealPlannerReport
ORDER BY MealDateId, MealTypeId
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwMealPlannerReport>(sql: base.Sql);
			return rows.ToList();
		});
	}

	public async Task<MealDateTime> GetMealDateTime(MealTicketEnum mealTicketEnum)  //  = MealTicketEnum.UpCommingMeal
	{
		const int ArizonaUtcMinus7 = -7;
		DateTime azdt;

		if (mealTicketEnum == MealTicketEnum.UpCommingMeal)
		{
			azdt = DateTime.UtcNow.AddHours(ArizonaUtcMinus7);
		}
		else
		{
			azdt = MealTicket.FromEnum(mealTicketEnum).MealTicketDateTime.AddHours(ArizonaUtcMinus7);
		}

		base.Parms = new DynamicParameters(new { UpCommingMealDateTime = azdt });
		base.Sql = $@"
SELECT Id, DateTime, Descr  
FROM Sukkot.MealDateTime mdt
	INNER JOIN
		(
		SELECT Min(DateTime) MinDate
		FROM Sukkot.Sukkot.MealDateTime 
		WHERE DateTime > @UpCommingMealDateTime
		) as i
		ON mdt.DateTime = i.MinDate";

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<MealDateTime>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault();
		});
	}

	public async Task<List<vwKitchenWork>> GetKitchenWorkList(int mealDateTimeId)
	{
		base.Parms = new DynamicParameters(new { MealDateTimeId = mealDateTimeId });
		base.Sql = $@"
SELECT Id, Name, Volunteer, WorkType, DateTime
FROM Sukkot.vwKitchenWork 
WHERE MealDateTimeId = @MealDateTimeId
ORDER BY KitchenWorkTypeId
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwKitchenWork>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}

	public async Task<KitchenWork> GetKitchenWork(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $@"
 SELECT Id, KitchenWorkTypeId, Volunteer, RegistrationId, MealDay, MealTime, WorkType
 FROM Sukkot.KitchenWork 
 WHERE Id=@Id
 ORDER BY KitchenWorkTypeId
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<KitchenWork>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault();
		});
	}

	public async Task<List<vwAttendanceAllFeastDays>> GetAttendanceAllFeastDays()
	{
		base.Sql = "SELECT FeastDay2, Id,  Adults, ChildBig, ChildSmall, TotalPeeps FROM Sukkot.vwAttendanceAllFeastDays ORDER BY Id";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwAttendanceAllFeastDays>(sql: base.Sql);
			return rows.ToList();
		});
	}

	public async Task<vwAttendancePeopleSummary> GetAttendancePeopleSummary()
	{
		base.Sql = "SELECT * FROM Sukkot.vwAttendancePeopleSummary";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwAttendancePeopleSummary>(sql: base.Sql);
			return rows.SingleOrDefault();
		});
	}

	public async Task<List<vwAttendanceChart>> GetAttendanceChart()
	{
		base.Sql = "SELECT FeastDay2, AgeDesc, Days FROM Sukkot.vwAttendanceChart ORDER BY Id, AgeSort";  // Id, AgeSort
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwAttendanceChart>(sql: base.Sql);
			return rows.ToList();
		});
	}


	public async Task<int> GetOffsiteCount()
	{
		base.Sql = "SELECT COUNT(*) AS OffsiteCount FROM Sukkot.Registration WHERE CampId	= 0";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<int>(sql: base.Sql);
			return rows.SingleOrDefault();
		});
	}

	//public async Task<int> UpdateContactSukkotInviteDate(int id)
	//{
	//	const int ArizonaUtcMinus7 = -7;
	//	DateTime azdt = DateTime.UtcNow.AddHours(ArizonaUtcMinus7);
	//	base.Parms = new DynamicParameters(new { Id = id, SukkotInviteDate = azdt });
	//	base.Sql = "UPDATE dbo.Contact SET SukkotInviteDate = @SukkotInviteDate WHERE Id=@id";

	//	return await WithConnectionAsync(async connection =>
	//	{
	//		var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
	//		return count;
	//	});
	//}


	public async Task<List<vwMealPlanMenu>> ListMealPlans()
	{
		base.Sql = "SELECT Id, MealDay, BruOrDin, Menu FROM Sukkot.vwMealPlanMenu";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwMealPlanMenu>(sql: base.Sql);
			return rows.ToList();
		});
	}


	public async Task<MealPlan> GetMealPlan(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = "SELECT Id, Menu FROM Sukkot.MealPlan WHERE Id	= @Id";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<MealPlan>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault();
		});
	}


	public async Task<int> UpdateKitchenWork(KitchenWork kitchenWork)
	{
		base.Parms = new DynamicParameters(new
		{ Id = kitchenWork.Id, Volunteer = kitchenWork.Volunteer, KitchenWorkTypeId = kitchenWork.KitchenWorkTypeId, RegistrationId = kitchenWork.RegistrationId });
		base.Sql = $@"
UPDATE Sukkot.KitchenWork SET
  KitchenWorkTypeId = @KitchenWorkTypeId
, Volunteer = @Volunteer
, RegistrationId = @RegistrationId
 WHERE Id	= @Id
";

		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return count;
		});
	}

	public async Task<int> EditMealPlan(MealPlan mealPlan)
	{
		base.Parms = new DynamicParameters(new { Id = mealPlan.Id, Menu = mealPlan.Menu });
		base.Sql = "UPDATE Sukkot.MealPlan SET Menu = @Menu WHERE Id=@Id";

		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return count;
		});
	}


}
