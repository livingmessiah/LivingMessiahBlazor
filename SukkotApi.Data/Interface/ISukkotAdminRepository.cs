using SukkotApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using SukkotApi.Domain.Registrations.Enums;

namespace SukkotApi.Data;

public interface ISukkotAdminRepository
{
		Task<List<vwRegistration>> GetAll(RegistrationSortEnum sort);
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
