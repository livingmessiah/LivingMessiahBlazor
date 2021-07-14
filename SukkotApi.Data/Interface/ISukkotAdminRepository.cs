﻿using SukkotApi.Domain;
using SukkotApi.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SukkotApi.Data
{
	public interface ISukkotAdminRepository
	{
		Task<List<vwRegistration>> GetAll(RegistrationSort sort);
		Task<List<Notes>> GetNotes(RegistrationSort sort);

		Task<int> InsertRegistrationDonation(Donation donation);
		Task<List<PreviousDonation>> GetRegistrationDonations(int id);
		Task<List<DonationReport>> GetDonationReport(DonationStatusEnum donationStatusEnum, string sort);
		Task<List<vwDonationDetail>> GetDonationsByRegistrationId(int id);
		Task<List<vwDonationsByRegistration>> GetDonationsByRegistration();

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

		Task<List<vwLodgingDaysAll>> GetLodgingDaysAll();
		Task<List<vwLodgingDaysPivotOnCampCode>> GetvwLodgingDaysPivotOnCampCode();
		Task<int> GetOffsiteCount();
		Task<List<vwLodgingDetail>> GetvwLodgingDetail();

		Task<List<vwMealPlanMenu>> ListMealPlans();
		Task<MealPlan> GetMealPlan(int Id);
		Task<int> EditMealPlan(MealPlan mealPlan);
		Task<int> UpdateKitchenWork(KitchenWork kitchenWork);
	}
}
