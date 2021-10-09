using System;
using System.Threading.Tasks;
using LivingMessiah.Web.Pages.Sukkot.CreateEdit;
using SukkotApi.Domain;
using SukkotApi.Domain.Enums;
using LivingMessiah.Web.Pages.Sukkot;
using LivingMessiah.Web.Pages.Sukkot.Constants;
using LivingMessiah.Web.Infrastructure;
using LivingMessiah.Web.Services;
using Microsoft.Extensions.Logging;
using SukkotApi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sukkot.Web.Service
{
	public interface ISukkotService
	{
		string ExceptionMessage { get; set; }
		Task<vwRegistration> Details(int id, ClaimsPrincipal user, bool showPrintInstructionMessage = false);
		Task<vwRegistration> DeleteConfirmation(int id, ClaimsPrincipal user);
		Task<RegistrationVM> Update(int id, ClaimsPrincipal user);
		Task<int> Create(RegistrationVM registration, ClaimsPrincipal user);
		Task<int> Edit(RegistrationVM registration, ClaimsPrincipal user);
		Task<int> DeleteConfirmed(int id);
		Task<EditMealsVM> Meals(int registrationId, ClaimsPrincipal user);
		Task<int> UpdateMeals(EditMealsVM vm);
		Task<RegistrationSummary> Summary(int id, ClaimsPrincipal user);
		//Task<KitchenWorkVM> KitchenJobs(int registrationId, ClaimsPrincipal user); NOT YET IMPLEMENTED
	}

	//ToDo: Where does SukkotService use  ControllerBase
	//public class SukkotService : ControllerBase, ISukkotService
	public class SukkotService : ISukkotService
	{
		#region Constructor and DI
		private readonly ISukkotRepository db;
		private readonly ILogger Logger;

		public SukkotService(
			ISukkotRepository sukkotRepository, ILogger<SukkotService> logger)
		{
			db = sukkotRepository;
			Logger = logger;
		}
		#endregion

		public string ExceptionMessage { get; set; } = "";

		public async Task<RegistrationSummary> Summary(int id, ClaimsPrincipal user)
		{
			var vm = new RegistrationSummary();
			try
			{
				vm = await db.GetRegistrationSummary(id);
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(Summary)}, {nameof(db.GetRegistrationSummary)}";
				Logger.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}

			//ToDo: id is not used
			if (!IsUserAuthoirized(vm.EMail, id, user))
			{
				ExceptionMessage = $"Inside {nameof(Summary)}, logged in user:{vm.EMail} lacks authority for id={id}";
				Logger.LogWarning(ExceptionMessage);
				throw new UserNotAuthoirizedException(ExceptionMessage);
			}

			return vm;
		}

		public async Task<vwRegistration> Details(int id, ClaimsPrincipal user, bool showPrintInstructionMessage = false)
		{
			var vm = new vwRegistration();
			try
			{
				vm = await db.ById(id);
				if (showPrintInstructionMessage)
				{
					vm.PayWithCheckMessage = Other.PayWithCheckModalMessage;
				}
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(Details)}, {nameof(db.ById)}";
				Logger.LogError(ex, ExceptionMessage, id, showPrintInstructionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}

			if (!IsUserAuthoirized(vm.EMail, id, user))
			{
				ExceptionMessage = $"Inside {nameof(Details)}, logged in user:{vm.EMail} lacks authority for id={id}";
				Logger.LogWarning(ExceptionMessage);
				throw new UserNotAuthoirizedException(ExceptionMessage);
			}

			return vm;
		}

		public async Task<vwRegistration> DeleteConfirmation(int id, ClaimsPrincipal user)
		{
			var vm = new vwRegistration();
			try
			{
				vm = await db.ById(id);
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(DeleteConfirmation)}, {nameof(db.ById)}";
				Logger.LogError(ex, ExceptionMessage, id);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}

			if (!IsUserAuthoirized(vm.EMail, id, user))
			{
				ExceptionMessage = $"Inside {nameof(DeleteConfirmation)}, logged in user:{vm.EMail} lacks authority for id={id}";
				Logger.LogWarning(ExceptionMessage);
				throw new UserNotAuthoirizedException(ExceptionMessage);
			}

			return vm;
		}

		//
		public async Task<RegistrationVM> Update(int id, ClaimsPrincipal user)
		{
			Logger.LogInformation($"Inside {nameof(SukkotService)}!{nameof(Update)}, id={id}");
			RegistrationPOCO registrationPOCO = new RegistrationPOCO();
			try
			{
				registrationPOCO = await db.ById2(id);
				if (!IsUserAuthoirized(registrationPOCO.EMail, id, user))
				{
					ExceptionMessage = $"Inside {nameof(Update)}, logged in user:{registrationPOCO.EMail} lacks authority for id={id}";
					Logger.LogWarning(ExceptionMessage);
					throw new UserNotAuthoirizedException(ExceptionMessage);
				}
				//if (registrationPOCO.StatusId == (int)Status.FullyPaid & !AdminOrSukkotOverride(user))
				if (registrationPOCO.StatusEnum == StatusEnum.FullyPaid & !AdminOrSukkotOverride(user))
				{
					throw new RegistratationException("Can not edit registration that has been fully paid.");
				}
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(Update)}";
				Logger.LogError(ex, ExceptionMessage, id);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}

			//Logger.LogDebug($"...Calling {nameof(UpdateDTO)}");
			//Logger.LogDebug($".....AttendanceDateList: {DumpDateRange(registrationPOCO.AttendanceDateList)}");
			//Logger.LogDebug($".....LodgingDateList: {DumpDateRange(registrationPOCO.LodgingDateList)}");

			return UpdateDTO(registrationPOCO);
		}

		public async Task<int> Create(RegistrationVM registration, ClaimsPrincipal user)
		{
			int newId = 0;

			try
			{
				Logger.LogInformation($"Calling {nameof(db.Create)}");

				if (user.GetRoles() == Auth0.Roles.Admin | user.GetRoles() == Auth0.Roles.Sukkot)
				{
					// This is nonsensical and superfalous 
					// I think it's here because making the if a Not makes it hard to understand
					registration.StatusEnum = registration.StatusEnum;
				}
				else
				{
					registration.StatusEnum = StatusEnum.RegistrationFormCompleted;
				}

				registration.AttendanceBitwise = GetDaysBitwise(registration.AttendanceDateList, DateRangeEnum.AttendanceDays);
				registration.LodgingDaysBitwise = GetDaysBitwise(registration.LodgingDateList, DateRangeEnum.LodgingDays);

				newId = await db.Create(DTO(registration));
				Logger.LogInformation($"Registration created for {registration.FamilyName}/{registration.EMail}; newId={newId}, registration.StatusId={registration.StatusEnum}");
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(Create)}, {nameof(db.Create)}";
				Logger.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return newId;
		}
		
		public static string DumpDateRange(DateTime[] dateList)
		{
			if (dateList == null) { return ""; }
			string s = "";
			foreach (DateTime day in dateList)
			{
				s += day.ToString("MM/dd") + ", ";
			}
			//s = s.TrimEnd(",");
			//s.TrimEnd("");

			//s += "; Length: " + s.Length;
			return s;
		}
	
		private int GetDaysBitwise(DateTime[] dateList, DateRangeEnum dateRangeEnum)
		{
			if (dateList == null) { return 0; }

			//Logger.LogDebug($"Inside: {nameof(SukkotService)}!{nameof(GetDaysBitwise)}, dateRangeEnum: {dateRangeEnum}");
			DateRangeLocal DateRangeLocal = DateRangeLocal.FromEnum(dateRangeEnum);

			int bitwise = 0;

			if (dateRangeEnum == DateRangeEnum.AttendanceDays)
			{
				int a = 0;
				foreach (DateTime day in dateList)
				{
					a = DateFactory.GetAttendanceBitwise(day);
					//Logger.LogDebug($"......a:{a} for day:{day}");
					bitwise = bitwise + a;
				}
			}
			else
			{
				foreach (DateTime day in dateList)
				{
					int l = 0;
					l = DateFactory.GetLodgingBitwise(day);
					//Logger.LogDebug($"......l:{l} for day:{day}");
					bitwise = bitwise + l;
				}
			}
			//Logger.LogDebug($"...bitwise: {bitwise}");
			return bitwise;
		}

		private RegistrationPOCO DTO(RegistrationVM registration)
		{
			RegistrationPOCO poco = new RegistrationPOCO
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
				LocationEnum = registration.LocationEnum,
				CampTypeEnum = registration.CampTypeEnum,  //CampId = registration.CampTypeEnum,
				StatusEnum = registration.StatusEnum,  //StatusId = registration.StatusEnum,
				AttendanceBitwise = registration.AttendanceBitwise,
				LodgingDaysBitwise = registration.LodgingDaysBitwise,
				AssignedLodging = registration.AssignedLodging,
				LmmDonation = registration.LmmDonation,
				WillHelpWithMeals = registration.WillHelpWithMeals,
				Avitar = registration.Avitar,
				Notes = registration.Notes
			};
			return poco;
		}

		private RegistrationVM UpdateDTO(RegistrationPOCO poco)
		{
			RegistrationVM registration = new RegistrationVM
			{
				Id = poco.Id,
				FamilyName = poco.FamilyName,
				FirstName = poco.FirstName,
				SpouseName = poco.SpouseName,
				OtherNames = poco.OtherNames,
				EMail = poco.EMail,
				Phone = poco.Phone,
				Adults = poco.Adults,
				ChildBig = poco.ChildBig,
				ChildSmall = poco.ChildSmall,
				CampTypeEnum = poco.CampTypeEnum,
				LocationEnum = poco.LocationEnum,
				StatusEnum = poco.StatusEnum, // poco.StatusId,
				AttendanceBitwise = poco.AttendanceBitwise,
				AttendanceDateList = poco.AttendanceDateList,
				LodgingDaysBitwise = poco.LodgingDaysBitwise,
				LodgingDateList = poco.LodgingDateList,
				AssignedLodging = poco.AssignedLodging,
				LmmDonation = poco.LmmDonation,
				WillHelpWithMeals = poco.WillHelpWithMeals,
				Avitar = poco.Avitar,
				Notes = poco.Notes
			};

			Logger.LogDebug($"Inside {nameof(SukkotService)}!{nameof(UpdateDTO)}");
			//Logger.LogDebug($"...registration.StatusEnum: {registration.StatusEnum}, registration.CampTypeEnum: {registration.CampTypeEnum}");
			//Logger.LogDebug($"...AttendanceDateList: {registration.AttendanceDateList}; LodgingDateList: {registration.LodgingDateList}");
			//Logger.LogDebug($"...AttendanceBitwise: {registration.AttendanceBitwise}; LodgingDaysBitwise: {registration.LodgingDaysBitwise}");
			//Logger.LogDebug($"...LocationEnum: {registration.LocationEnum}");
			return registration;
		}

		public async Task<int> Edit(RegistrationVM registration, ClaimsPrincipal user)
		{
			Logger.LogInformation($"Inside {nameof(SukkotService)}!{nameof(Edit)}");
			int count = 0;
			try
			{
				if (user.GetRoles() == Auth0.Roles.Admin | user.GetRoles() == Auth0.Roles.Sukkot)
				{
					registration.StatusEnum = registration.StatusEnum;
				}
				else
				{
					//registration.StatusEnum = (int)Status.RegistrationFormCompleted;
					registration.StatusEnum = StatusEnum.RegistrationFormCompleted;
				}

				registration.AttendanceBitwise = GetDaysBitwise(registration.AttendanceDateList, DateRangeEnum.AttendanceDays);
				registration.LodgingDaysBitwise = GetDaysBitwise(registration.LodgingDateList, DateRangeEnum.LodgingDays);

				Logger.LogInformation($"Calling {nameof(db.Update)}");
				count = await db.Update(DTO(registration));
				Logger.LogInformation($"Registration updated for {registration.FamilyName}/{registration.EMail}; count={count}");

				/*
				// Task 683 Add IsMealsAvailable to appsettings.json and code for it 
				if (registration.ChildBig == 0 | registration.ChildSmall == 0)
				{
					count = await db.UpdateMealTicketReset(registration.Id, registration.ChildBig, registration.ChildSmall);
				}
				*/
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(Edit)}, {nameof(db.Update)}";
				Logger.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return count;
		}

		public async Task<int> DeleteConfirmed(int id)
		{
			int count = 0;
			try
			{
				Logger.LogInformation($"Delete meals and registration in one call");
				count = await db.Delete(id);
				Logger.LogInformation($"Registration and meals deleted for id={id}; affected rows={count}");
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(DeleteConfirmed)}, {nameof(db.Delete)}, id={id}";
				Logger.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return count;
		}

		public async Task<EditMealsVM> Meals(int registrationId, ClaimsPrincipal user)
		{
			Logger.LogInformation($"Inside {nameof(Meals)}, registrationId={registrationId}");
			MealsRelatedRegistrationData RegistrationData = new MealsRelatedRegistrationData();
			RegistrationData = await GetRegistrationDataAndCheckAuthority(registrationId, user);
			var vm = new EditMealsVM();
			vm = await GetMeals(RegistrationData);
			return vm;
		}

		private async Task<MealsRelatedRegistrationData> GetRegistrationDataAndCheckAuthority(int registrationId, ClaimsPrincipal user)
		{
			MealsRelatedRegistrationData RegistrationData;
			try
			{
				RegistrationData = await db.MealsRelatedRegistrationData(registrationId);
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(GetRegistrationDataAndCheckAuthority)}, {nameof(db.MealsRelatedRegistrationData)}, registrationId={registrationId}";
				Logger.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}

			if (!IsUserAuthoirized(RegistrationData.EMail, registrationId, user))
			{
				ExceptionMessage = $"Inside {nameof(GetRegistrationDataAndCheckAuthority)}, logged in user:{RegistrationData.EMail} lacks authority for registrationId={registrationId}";
				Logger.LogWarning(ExceptionMessage);
				throw new UserNotAuthoirizedException(ExceptionMessage);
			}

			return RegistrationData;
		}

		private async Task<EditMealsVM> GetMeals(MealsRelatedRegistrationData registrationData)
		{
			var vm = new EditMealsVM();
			Logger.LogInformation("Get Meals");

			vm.RegistrationId = registrationData.Id;
			vm.Title = Other.MealTicketTitle;
			vm.FamilyName = registrationData.FamilyName;
			vm.StatusId = registrationData.StatusId;

			vm.AdultCount = registrationData.Adults;
			vm.ChildBigCount = registrationData.ChildBig;
			vm.ChildSmallCount = registrationData.ChildSmall;

			// Used only by DetailsMealTicket.cshtml
			vm.MealTotalCostAdult = registrationData.MealTotalCostAdult;
			vm.MealTotalCostChildBig = registrationData.MealTotalCostChildBig;
			vm.MealRateAdult = registrationData.MealRateAdult;
			vm.MealRateChildBig = registrationData.MealRateChildBig;

			try
			{
				//Regular
				vm.MealAdult = await GetMealAdultAsync(registrationData.Id, registrationData.Adults, MealAges.Adults);
				vm.MealChildBig = await GetMealChildBigAsync(registrationData.Id, registrationData.ChildBig, MealAges.ChildBig);
				vm.MealChildSmall = await GetMealChildSmallAsync(registrationData.Id, registrationData.ChildSmall, MealAges.ChildSmall);

				//Vegetarian
				vm.MealAdultVeg = await GetMealAdultVegAsync(registrationData.Id, registrationData.Adults, MealAges.Adults);
				vm.MealChildBigVeg = await GetMealChildBigVegAsync(registrationData.Id, registrationData.ChildBig, MealAges.ChildBig);
				vm.MealChildSmallVeg = await GetMealChildSmallVegAsync(registrationData.Id, registrationData.ChildSmall, MealAges.ChildSmall);
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(GetMeals)}, registrationData.Id={registrationData.Id}";
				Logger.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return vm;
		}

		/*
		NOTE IMPLMENTED YET, COMMENTED OUT BECAUSE OF WARNING

		public async Task<KitchenWorkVM> KitchenJobs(int registrationId, ClaimsPrincipal user)
		{
			log.LogInformation($"Inside {nameof(KitchenJobs)}, registrationId={registrationId}");
			MealsRelatedRegistrationData RegistrationData = new MealsRelatedRegistrationData();
			RegistrationData = await GetRegistrationDataAndCheckAuthority(registrationId, user);
			var vm = new KitchenWorkVM();
			vm = await GetKitchenJobs(RegistrationData);
			return vm;
		}

		private async Task<KitchenWorkVM> GetKitchenJobs(MealsRelatedRegistrationData registrationData)
		{
			var vm = new KitchenWorkVM();
			log.LogInformation("Get Kitchen Jobs");

			vm.RegistrationId = registrationData.Id;
			vm.Title = Other.MealTicketTitle;
			vm.FamilyName = registrationData.FamilyName;
			//vm.StatusId = registrationData.StatusId;

			//vm.AdultCount = registrationData.Adults;
			//vm.ChildBigCount = registrationData.ChildBig;
			//vm.ChildSmallCount = registrationData.ChildSmall;

			try
			{
				//vm.MealAdult = await GetMealAdultAsync(registrationData.Id, registrationData.Adults, MealAges.Adults);
				//vm.MealChildBig = await GetMealChildBigAsync(registrationData.Id, registrationData.ChildBig, MealAges.ChildBig);
				//vm.MealChildSmall = await GetMealChildSmallAsync(registrationData.Id, registrationData.ChildSmall, MealAges.ChildSmall);
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(GetKitchenJobs)}, registrationData.Id={registrationData.Id}";
				log.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}

			return vm;
		}
		*/

		private async Task<Meal> GetMealAdultAsync(int id, int count, MealAges mealAges)
		{
			Meal meal = null;
			if (count > 0)
			{
				meal = new Meal
				{
					Lunch = new Lunch(),
					Dinner = new Dinner()
				};
				meal.Lunch = await db.GetLunch(id, MealEnum.AdultLunch);
				meal.Dinner = await db.GetDinner(id, MealEnum.AdultDinner);
			}
			return meal;
		}

		private async Task<Meal> GetMealChildBigAsync(int id, int count, MealAges mealAges)
		{
			Meal meal = null;
			if (count > 0)
			{
				meal = new Meal
				{
					Lunch = new Lunch(),
					Dinner = new Dinner()
				};
				meal.Lunch = await db.GetLunch(id, MealEnum.ChildBigLunch);
				meal.Dinner = await db.GetDinner(id, MealEnum.ChildBigDinner);
			}
			return meal;
		}

		private async Task<Meal> GetMealChildSmallAsync(int id, int count, MealAges mealAges)
		{
			Meal meal = null;
			if (count > 0)
			{
				meal = new Meal
				{
					Lunch = new Lunch(),
					Dinner = new Dinner()
				};
				meal.Lunch = await db.GetLunch(id, MealEnum.ChildSmallLunch);
				meal.Dinner = await db.GetDinner(id, MealEnum.ChildSmallDinner);
			}
			return meal;
		}

		private async Task<Meal> GetMealAdultVegAsync(int id, int count, MealAges mealAges)
		{
			Meal meal = null;
			if (count > 0)
			{
				meal = new Meal
				{
					Lunch = new Lunch(),
					Dinner = new Dinner()
				};
				meal.Lunch = await db.GetLunch(id, MealEnum.AdultLunchVeg);
				meal.Dinner = await db.GetDinner(id, MealEnum.AdultDinnerVeg);
			}
			return meal;
		}

		private async Task<Meal> GetMealChildBigVegAsync(int id, int count, MealAges mealAges)
		{
			Meal meal = null;
			if (count > 0)
			{
				meal = new Meal
				{
					Lunch = new Lunch(),
					Dinner = new Dinner()
				};
				meal.Lunch = await db.GetLunch(id, MealEnum.ChildBigLunchVeg);
				meal.Dinner = await db.GetDinner(id, MealEnum.ChildBigDinnerVeg);
			}
			return meal;
		}

		private async Task<Meal> GetMealChildSmallVegAsync(int id, int count, MealAges mealAges)
		{
			Meal meal = null;
			if (count > 0)
			{
				meal = new Meal
				{
					Lunch = new Lunch(),
					Dinner = new Dinner()
				};
				meal.Lunch = await db.GetLunch(id, MealEnum.ChildSmallLunchVeg);
				meal.Dinner = await db.GetDinner(id, MealEnum.ChildSmallDinnerVeg);
			}
			return meal;
		}

		public async Task<int> UpdateMeals(EditMealsVM vm)
		{
			int count = 0;
			Logger.LogInformation($"Calling {nameof(db.UpdateMeal)}");

			try
			{
				if (vm.AdultCount > 0)
				{
					count = await db.UpdateMeal(vm.RegistrationId, vm.MealAdult, AgeEnum.Adult);
					count = await db.UpdateMealVeg(vm.RegistrationId, vm.MealAdultVeg, AgeEnum.Adult);
				}
				if (vm.ChildBigCount > 0)
				{
					count = await db.UpdateMeal(vm.RegistrationId, vm.MealChildBig, AgeEnum.ChildBig);
					count = await db.UpdateMealVeg(vm.RegistrationId, vm.MealChildBigVeg, AgeEnum.ChildBig);
				}
				if (vm.ChildSmallCount > 0)
				{
					count = await db.UpdateMeal(vm.RegistrationId, vm.MealChildSmall, AgeEnum.ChildSmall);
					count = await db.UpdateMealVeg(vm.RegistrationId, vm.MealChildSmallVeg, AgeEnum.ChildSmall);
				}

				count += await db.RegistrationUpdateMealsCompleted(vm.RegistrationId);
				//log.LogDebug($"Meal Ticket(s) updated for {nameof(Registration)}; count={count}");
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(UpdateMeals)}";
				Logger.LogError(ex, ExceptionMessage);
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return count;
		}


		private bool AdminOrSukkotOverride(ClaimsPrincipal user)
		{
			if (user.GetRoles() == Auth0.Roles.Admin | user.GetRoles() == Auth0.Roles.Sukkot)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool IsUserAuthoirized(string registrationEmail, int id, ClaimsPrincipal user)
		{
			string userEmail = user.GetUserEmail();
			if (userEmail == registrationEmail) { return true; }

			if (user.RoleHasAdminOrSukkot())
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}

	#region CustomExceptions Classes

	public class UserNotAuthoirizedException : Exception
	{
		public UserNotAuthoirizedException()
		{
		}
		public UserNotAuthoirizedException(string message)
				: base(message)
		{
		}

		public UserNotAuthoirizedException(string message, Exception inner)
				: base(message, inner)
		{
		}
	}

	public class RegistratationException : Exception
	{
		public RegistratationException()
		{
		}
		public RegistratationException(string message)
				: base(message)
		{
		}

		public RegistratationException(string message, Exception inner)
				: base(message, inner)
		{
		}
	}

	/*
	 # Notes on Exceptions
	 http://blog.abodit.com/2010/03/using-exception-data-to-add-additional-information-to-an-exception/
	 catch (RegistratationException e) when (e.Data != null)
	foreach (DictionaryEntry de in e.Data)
		Console.WriteLine("    Key: {0,-20}      Value: {1}", 
												 "'" + de.Key.ToString() + "'", de.Value);
	*/

	#endregion
}
