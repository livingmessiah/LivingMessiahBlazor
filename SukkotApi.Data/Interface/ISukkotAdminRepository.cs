using SukkotApi.Domain;
using SukkotApi.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using SukkotApi.Domain.Donations.Commands;
using SukkotApi.Domain.Donations.Enums;
using SukkotApi.Domain.Donations.Queries;
using SukkotApi.Domain.Registrations.Enums;

namespace SukkotApi.Data
{
	public interface ISukkotAdminRepository
	{
		Task<List<vwRegistration>> GetAll(RegistrationSortEnum sort);
		Task<List<Notes>> GetNotes(RegistrationSortEnum sort);

		Task<int> InsertRegistrationDonation(Donation donation);
		Task<List<PreviousDonation>> GetRegistrationDonations(int id);
		Task<List<DonationReport>> GetDonationReport(int , string sortAndOrder);
		Task<List<DonationDetail>> GetDonationsByRegistrationId(int id);
		Task<List<DonationsByRegistration>> GetDonationsByRegistration();

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
