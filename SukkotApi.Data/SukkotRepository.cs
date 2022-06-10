using Dapper;
using SukkotApi.Domain;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace SukkotApi.Data;

/*
ToDo: Task 251 Test Dapper for SQL Injection
*/

public class SukkotRepository : BaseRepositoryAsync, ISukkotRepository
{
		public SukkotRepository(IConfiguration config, ILogger<SukkotRepository> logger) : base(config, logger)
		{
		}

		public string BaseSqlDump
		{
				get { return base.SqlDump; }
		}

		public async Task<int> Create(RegistrationPOCO registration)
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
						CampId = registration.CampTypeEnum, // registration.CampId,
						StatusId = registration.StatusEnum, // registration.StatusId,

						AttendanceBitwise = registration.AttendanceBitwise,
						LodgingDaysBitwise = registration.LodgingDaysBitwise,

						AssignedLodging = "",
						LmmDonation = 0,
						WillHelpWithMeals = 0,
						Avitar = registration.Avitar,
						Notes = registration.Notes,
				}); ;

				base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

				return await WithConnectionAsync(async connection =>
				{
						base.log.LogDebug($"Inside {nameof(SukkotRepository)}!{nameof(Create)}; About to execute sql:{base.Sql}");
						var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
						int? x = base.Parms.Get<int?>("NewId");
						if (x == null)
						{
								base.log.LogWarning($"NewId is null; returning as 0; Check dbo.ErrorLog for IX_Registration_EMail_Unique duplication Error; registration.EMail: {@registration.EMail}");
								return 0;
						}
						else
						{
								int NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
								base.log.LogDebug($"Return NewId:{NewId}");
								return NewId;
						}

				});
		}


		/*
		 https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql/table-valued-parameters
		 https://www.codeproject.com/Articles/835519/Passing-Table-Valued-Parameters-with-Dapper
		 https://medium.com/dapper-net/sql-server-specific-features-2773d894a6ae
		 */

		public async Task<int> Update(RegistrationPOCO registration)
		{
				//{registration.StatusId},
				//CampId = {registration.CampId},
				base.Sql = $@"
UPDATE Sukkot.Registration SET 
	FamilyName = N'{registration.FamilyName}',
	FirstName = N'{registration.FirstName}',
	SpouseName = N'{registration.SpouseName}',
	OtherNames = N'{registration.OtherNames}',
	EMail = N'{registration.EMail}',
	Phone = N'{registration.Phone}',
	Adults = {registration.Adults},
	ChildBig = {registration.ChildBig},
	ChildSmall = {registration.ChildSmall},

	AttendanceBitwise = {registration.AttendanceBitwise},
	LodgingDaysBitwise = {registration.LodgingDaysBitwise},
	CampId = {(int)registration.CampTypeEnum},
	StatusId = {(int)registration.StatusEnum},  
	WillHelpWithMeals = {registration.WillHelpWithMealsToInt}, 
	LmmDonation = {registration.LmmDonation},
	AssignedLodging = N'{registration.AssignedLodging}',
	Notes = N'{registration.NotesScrubbed}',
	Avitar = N'{registration.Avitar}'
WHERE Id = {registration.Id};
";
				return await WithConnectionAsync(async connection =>
				{
						var count = await connection.ExecuteAsync(sql: base.Sql);
						return count;
				});
		}

		/*
			base.Parms = new DynamicParameters(new {
				RegistrationId = donation.RegistrationId,
				CreateDate = donation.CreateDate
			});		 
			 */

		public async Task<int> Delete(int id)
		{
				base.Sql = "Sukkot.stpRegistrationDelete";
				base.Parms = new DynamicParameters(new { RegistrationId = id });
				return await WithConnectionAsync(async connection =>
				{
						var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
					//if (affectedrows < 0) { throw new Exception($"Registration NOT Deleted"); }
					return affectedrows;
				});
		}

		public async Task<vwRegistration> ById(int id)
		{
				base.Parms = new DynamicParameters(new { Id = id });
				base.Sql = $@"SELECT TOP 1 * FROM Sukkot.vwRegistration WHERE Id = @id";
				return await WithConnectionAsync(async connection =>
				{
						var rows = await connection.QueryAsync<vwRegistration>(sql: base.Sql, param: base.Parms);
						return rows.SingleOrDefault();
				});
		}

		public async Task<RegistrationPOCO> ById2(int id)
		{
				base.Sql = $@"
SELECT TOP 1 
Id, FamilyName, FirstName, SpouseName, OtherNames, EMail, Phone, Adults, ChildBig, ChildSmall
, CampId AS CampTypeEnum, StatusId AS StatusEnum
, AttendanceBitwise, LodgingDaysBitwise, AssignedLodging, LmmDonation, WillHelpWithMeals, Notes, Avitar
, Sukkot.udfLodgingDatesConcat(Id) AS LodgingDatesCSV
, Sukkot.udfAttendanceDatesConcat(Id) AS AttendanceDatesCSV
FROM Sukkot.Registration WHERE Id = {id}";
				return await WithConnectionAsync(async connection =>
				{
						var rows = await connection.QueryAsync<RegistrationPOCO>(sql: base.Sql);
						return rows.SingleOrDefault();
				});
		}

		public async Task<vwRegistrationShell> ByEmail(string email)
		{
				base.Sql = $@"SELECT Id, FamilyName, StatusId, TotalDonation, EMail, MealCount, RegistrationFee, MealCost, CampCost FROM Sukkot.vwRegistrationShell WHERE EMail = @EMail";
				base.Parms = new DynamicParameters(new { EMail = email });
				return await WithConnectionAsync(async connection =>
				{
						var rows = await connection.QueryAsync<vwRegistrationShell>(sql: base.Sql, param: base.Parms);
						return rows.SingleOrDefault();
				});
		}

		public async Task<int> MealInitialization(int registrationId)
		{
				base.Sql = "Sukkot.stpMealInsert ";
				base.Parms = new DynamicParameters();
				Parms.Add("@RegistrationId", registrationId, dbType: DbType.Int32, direction: ParameterDirection.Input);
				return await WithConnectionAsync(async connection =>
				{
						var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
						return count;
				});
		}

		public async Task<Lunch> GetLunch(int registrationId, MealEnum mealEnum)
		{
				int i = (int)mealEnum;
				base.Sql = $@"
SELECT {i} AS MealEnum
, Day01, Day02, Day03, Day04, Day05, Day06, Day07, Day08, MealCount, MealCost
FROM Sukkot.tvfMeal({registrationId},{i})
";
				return await WithConnectionAsync(async connection =>
				{
						var rows = await connection.QueryAsync<Lunch>(sql: base.Sql);
						return rows.SingleOrDefault();
				});
		}

		public async Task<Dinner> GetDinner(int registrationId, MealEnum mealEnum)
		{
				int i = (int)mealEnum;
				base.Sql = $@"
SELECT {i} AS MealEnum
, Day01, Day02, Day03, Day04, Day05, Day06, Day07, Day08, MealCount, MealCost
FROM Sukkot.tvfMeal({registrationId},{i})
";
				return await WithConnectionAsync(async connection =>
				{
						var rows = await connection.QueryAsync<Dinner>(sql: base.Sql);
						return rows.SingleOrDefault();
				});
		}

		public async Task<MealsRelatedRegistrationData> MealsRelatedRegistrationData(int id)
		{
				base.Parms = new DynamicParameters(new { id = id });
				base.Sql = $@"
SELECT Id, EMail, FamilyName, Adults, ChildBig, ChildSmall
, StatusId,  AttendanceBitwise, MealTotalCostAdult, MealTotalCostChildBig
FROM Sukkot.tvfMealSummary(@id)
";
				return await WithConnectionAsync(async connection =>
				{
						var rows = await connection.QueryAsync<MealsRelatedRegistrationData>(base.Sql, base.Parms);
						return rows.SingleOrDefault();
				});
		}

		public async Task<RegistrationSummary> GetRegistrationSummary(int id)
		{
				base.Parms = new DynamicParameters(new { id = id });
				base.Sql = $@"
SELECT Id, EMail, FamilyName, Adults, ChildBig, ChildSmall, StatusId
, AttendanceBitwise, MealTotalCostAdult, MealTotalCostChildBig
, RegistrationFee, CampCost, CampDescr, CampDays, AdultRate, ChildRate, TotalDonation
FROM Sukkot.tvfRegistrationSummary(@id)
";
				return await WithConnectionAsync(async connection =>
				{
						var rows = await connection.QueryAsync<RegistrationSummary>(base.Sql, base.Parms);
						return rows.SingleOrDefault();
				});
		}

		//Task 683 Add IsMealsAvailable to appsettings.json and code for it
		public async Task<int> UpdateMeal(int registrationId, Meal meal, AgeEnum age)
		{
				base.Sql = "Sukkot.stpUpdateMeals ";
				base.Parms = new DynamicParameters(new
				{
						Id = registrationId,
						PersonType = (int)age,
						DinDay01 = meal.Dinner.Day01,
						DinDay02 = meal.Dinner.Day02,
						DinDay03 = meal.Dinner.Day03,
						DinDay04 = meal.Dinner.Day04,
						DinDay05 = meal.Dinner.Day05,
						DinDay06 = meal.Dinner.Day06,
						DinDay07 = meal.Dinner.Day07,
						DinDay08 = meal.Dinner.Day08,
						LunDay01 = meal.Lunch.Day01,
						LunDay02 = meal.Lunch.Day02,
						LunDay03 = meal.Lunch.Day03,
						LunDay04 = meal.Lunch.Day04,
						LunDay05 = meal.Lunch.Day05,
						LunDay06 = meal.Lunch.Day06,
						LunDay07 = meal.Lunch.Day07,
						LunDay08 = meal.Lunch.Day08,
				});
				return await WithConnectionAsync(async connection =>
				{
						var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
						return count;
				});
		}

		public async Task<int> UpdateMealVeg(int registrationId, Meal meal, AgeEnum age)
		{
				base.Sql = "Sukkot.stpUpdateMealsVeg ";
				base.Parms = new DynamicParameters(new
				{
						Id = registrationId,
						PersonType = (int)age,
						DinDay01 = meal.Dinner.Day01,
						DinDay02 = meal.Dinner.Day02,
						DinDay03 = meal.Dinner.Day03,
						DinDay04 = meal.Dinner.Day04,
						DinDay05 = meal.Dinner.Day05,
						DinDay06 = meal.Dinner.Day06,
						DinDay07 = meal.Dinner.Day07,
						DinDay08 = meal.Dinner.Day08,
						LunDay01 = meal.Lunch.Day01,
						LunDay02 = meal.Lunch.Day02,
						LunDay03 = meal.Lunch.Day03,
						LunDay04 = meal.Lunch.Day04,
						LunDay05 = meal.Lunch.Day05,
						LunDay06 = meal.Lunch.Day06,
						LunDay07 = meal.Lunch.Day07,
						LunDay08 = meal.Lunch.Day08,
				});
				return await WithConnectionAsync(async connection =>
				{
						var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
						return count;
				});
		}

		// Task 683 Add IsMealsAvailable to appsettings.json and code for it
		public async Task<int> RegistrationUpdateMealsCompleted(int id)
		{
				base.Sql = "Sukkot.stpRegistrationUpdateMealsCompleted";
				base.Parms = new DynamicParameters(new { id = id });
				return await WithConnectionAsync(async connection =>
				{
						var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
						return count;
				});
		}

		//Task 683 Add IsMealsAvailable to appsettings.json and code for it
		// Called inside SukkotController.Edit(Registration registration) if (registration.ChildBig == 0 | registration.ChildSmall == 0)
		public async Task<int> UpdateMealTicketReset(int id, int childBig, int childSmall)
		{
				base.Sql = "Sukkot.stpUpdateMealTicketReset ";
				base.Parms = new DynamicParameters(new
				{
						Id = id,
						ChildBig = childBig,
						ChildSmall = childSmall
				});
				return await WithConnectionAsync(async connection =>
				{
						var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
						return count;
				});
		}


		/* Message	IDE0037	Member name can be simplified */
		public async Task<int> MealTicketPunchInsert(MealTicketPunchLog mealTicketPunchLog)
		{
				base.Sql = "Sukkot.stpMealTicketPunchLogInsert";
				base.Parms = new DynamicParameters(new
				{
						RegistrationId = mealTicketPunchLog.RegistrationId,
						MealDateTimeId = mealTicketPunchLog.MealDateTimeId,
						MealType = mealTicketPunchLog.MealTypeId,
						AgeEnum = mealTicketPunchLog.AgeId
				});

				base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

				return await WithConnectionAsync(async connection =>
				{
						var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);
						int newID = base.Parms.Get<int>("NewId");
						return newID;
				});
		}


		public async Task<List<MealTicketPunchLog2>> MealTicketPunchLogs()
		{
				base.Sql = $@"SELECT TOP 200 * FROM Sukkot.MealTicketPunchLog";
				return await WithConnectionAsync(async connection =>
				{
						var rows = await connection.QueryAsync<MealTicketPunchLog2>(sql: base.Sql);
						return rows.ToList();
				});
		}


		public async Task<MealTicketPunchLog> MealTicketPunchLogById(int id)
		{
				base.Parms = new DynamicParameters(new { Id = id });
				base.Sql = $@"SELECT TOP 1 * FROM Sukkot.MealTicketPunchLog WHERE Id = @id";
				return await WithConnectionAsync(async connection =>
				{
						var rows = await connection.QueryAsync<MealTicketPunchLog>(sql: base.Sql, param: base.Parms);
						return rows.SingleOrDefault();
				});
		}

		public async Task<int> MealTicketPunchLogUpdate(int id, MealTicketPunchLog mealTicketPunchLog)
		{
				base.Parms = new DynamicParameters(new
				{
						id,
						mealTicketPunchLog.RegistrationId,
						mealTicketPunchLog.MealDateTimeId,
						mealTicketPunchLog.MealTypeId,
						mealTicketPunchLog.AgeId
				});

				base.Sql = $@"
UPDATE Sukkot.MealTicketPunchLog SET 
	RegistrationId = @RegistrationId,
	MealDateTimeId = @MealDateTimeId,
	MealType = @MealTypeString,
	MealTime = @MealTimeString,
	AgeEnum = @Age
WHERE Id = @Id
";
				return await WithConnectionAsync(async connection =>
				{
						var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
						return count;
				});
		}


		public async Task<int> MealTicketPunchLogDelete(int id)
		{
				base.Parms = new DynamicParameters(new { Id = id });
				base.Sql = $@" DELETE Sukkot.MealTicketPunchLog WHERE Id = @Id
";
				return await WithConnectionAsync(async connection =>
				{
						var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
						return count;
				});
		}

}




