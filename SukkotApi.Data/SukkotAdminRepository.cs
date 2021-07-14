using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SukkotApi.Domain.Enums;
using SukkotApi.Domain;


namespace SukkotApi.Data
{
	public class SukkotAdminRepository : BaseRepositoryAsync, ISukkotAdminRepository
	{

		public SukkotAdminRepository(IConfiguration config, ILogger<SukkotAdminRepository> logger) : base(config, logger)
		{
		}

		//ToDo: maybe merge with GetDonationsByRegistrationId
		public async Task<List<PreviousDonation>> GetRegistrationDonations(int id)
		{
			//SELECT d.Id, RegistrationId, Detail, Amount, d.Notes, ReferenceId, CreatedBy, CreateDate, r.FamilyName
			base.Parms = new DynamicParameters(new { id = id });
			base.Sql = $@"
SELECT Detail, Amount, d.Notes, ReferenceId, CreatedBy, CreateDate
FROM Sukkot.Donation d
INNER JOIN Sukkot.Registration r on d.RegistrationId = r.Id
WHERE RegistrationId = @id
ORDER BY Detail
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<PreviousDonation>(base.Sql, base.Parms);
				return rows.ToList();
			});
		}

		public async Task<List<DonationReport>> GetDonationReport(DonationStatusEnum donationStatusEnum, string sortField)
		{
			var enumRepl = DonationStatus.FromEnum(donationStatusEnum);

			if (enumRepl.StatusId2 == null)
			{
				base.Parms = new DynamicParameters(new { StatusId1 = enumRepl.StatusId1 });
				base.Sql = $@"
SELECT Id, EMail, FirstName, FamilyName, StatusId, StatusDescr, MealTotalCost, RegistrationFee, CampCost, TotalDonation, AmountDue
FROM Sukkot.vwDonationReport
WHERE StatusId = @StatusId1 OR @StatusId1 IS NULL
ORDER BY {sortField}
";
			}
			else
			{
				base.Parms = new DynamicParameters(new { StatusId1 = enumRepl.StatusId1, StatusId2 = enumRepl.StatusId2 });
				base.Sql = $@"
SELECT Id, EMail, FamilyName, StatusId, StatusDescr, MealTotalCost, CampCost, TotalDonation, AmountDue
FROM Sukkot.vwDonationReport
WHERE StatusId = @StatusId1 OR StatusId = @StatusId2
ORDER BY {sortField}
";
			}

			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<DonationReport>(base.Sql, base.Parms);
				return rows.ToList();
			});
		}

		public async Task<List<vwDonationsByRegistration>> GetDonationsByRegistration()
		{
			base.Sql = $@"SELECT * FROM Sukkot.vwDonationsByRegistration ORDER BY FamilyName, Detail";

			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<vwDonationsByRegistration>(base.Sql);
				return rows.ToList();
			});
		}

		//ToDo: maybe merge with GetRegistrationDonations
		public async Task<List<vwDonationDetail>> GetDonationsByRegistrationId(int id)
		{
			base.Parms = new DynamicParameters(new { Id = id });
			base.Sql = $@"
 SELECT RegistrationId, Detail, Amount, Notes, ReferenceId, CreateDate, CreatedBy, FamilyName
 FROM Sukkot.vwDonationDetail 
 WHERE RegistrationId=@Id
 ORDER BY Detail
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<vwDonationDetail>(base.Sql, base.Parms);
				return rows.ToList();
			});
		}

		public async Task<int> InsertRegistrationDonation(Donation donation)
		{
			base.Sql = "Sukkot.stpDonationInsert ";
			base.Parms = new DynamicParameters(new
			{
				RegistrationId = donation.RegistrationId,
				Amount = donation.Amount,
				Notes = donation.Notes,
				ReferenceId = donation.ReferenceId,
				CreatedBy = donation.CreatedBy,
				CreateDate = donation.CreateDate
			});

			return await WithConnectionAsync(async connection =>
			{
				var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
				return count;
			});
		}


		public async Task<List<vwRegistration>> GetAll(RegistrationSort sort)
		{
			string sortField = (sort == RegistrationSort.FamilyName) ? "FamilyName" : "Id";

			base.Sql = $@"
SELECT TOP 500 Id, FamilyName, FirstName, SpouseName, OtherNames
, EMail, Phone, Adults, ChildBig, ChildSmall, WillHelpWithMeals
, CampId, StatusId, CampCD, StatusCD, Camp, Status, Notes, AssignedLodging
, RegistrationFee, CampCost, LmmDonation
, LodgingDaysBitWise, LodgingDaysTotal
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


		public async Task<List<Notes>> GetNotes(RegistrationSort sort)
		{
			string sortField = (sort == RegistrationSort.FamilyName) ? "FamilyName" : "Id";

			base.Sql = $@"
SELECT TOP 500 Id, FirstName, FamilyName, Notes AS UserNotes, AssignedLodging, CampCD, Phone, EMail
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

		//ToDo: Not being used
		public async Task<List<vwLodgingDaysAll>> GetLodgingDaysAll()
		{
			base.Sql = $@"
SELECT LodgingDay2, Sort, v.CampId, LodgingDays, c.Code AS CampCode
FROM Sukkot.vwLodgingDaysAll v
	JOIN Sukkot.Camp c ON v.CampId = c.Id
ORDER BY Sort
				";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<vwLodgingDaysAll>(sql: base.Sql);
				return rows.ToList();
			});
		}

		public async Task<List<vwLodgingDaysPivotOnCampCode>> GetvwLodgingDaysPivotOnCampCode()
		{
			base.Sql = $@"
SELECT LodgingDay2, Sort
, Tent, [RV Hookup] AS RVHookup, [Cabin/BH] AS CabinBH, c.CabinPeople, [RV DryCamp] AS RVDryCamp
FROM Sukkot.vwLodgingDaysPivotOnCampCode
JOIN Sukkot.vwLodgingDaysCabinsByDaySum AS c ON Sukkot.vwLodgingDaysPivotOnCampCode.Sort = c.LodgeDateId
ORDER BY Sort
				";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<vwLodgingDaysPivotOnCampCode>(sql: base.Sql);
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

		public async Task<List<vwLodgingDetail>> GetvwLodgingDetail()
		{
			base.Sql = $@"
SELECT Id, FamilyName, CampCost, CampDays, CampCD, Status, PeopleCount, LodgingDays, StatusId
FROM Sukkot.vwLodgingDetail
ORDER BY Id
";
			return await WithConnectionAsync(async connection =>
			{
				var rows = await connection.QueryAsync<vwLodgingDetail>(sql: base.Sql);
				return rows.ToList();
			});
		}

		//ToDo: combine this with previous call 
		// https://stackoverflow.com/questions/19337468/multiple-sql-statements-in-one-roundtrip-using-dapper-net
		//public async Task<List<vwLodgingDaysPivotOnCampCode>> GetvwLodgingDaysPivotOnCampCodeAndOffsetCount()
		//{
		//	base.Sql = "";
		//	//using (var multi = connection.QueryMultiple(sql, new {id=selectedId}))
		//	return await WithConnectionAsync(async connection =>
		//	{
		//		var rows = await connection.QueryMultipleAsync<int>(sql: base.Sql);
		//		var customer = multi.Read<Customer>().Single();
		//		var orders = multi.Read<Order>().ToList();
		//		var returns = multi.Read<Return>().ToList();
		//	});
		//}

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
}
