using SukkotApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SukkotApi.Data;

public interface ISukkotRepository
{
		string BaseSqlDump { get; }
		Task<vwRegistration> ById(int id);
		Task<RegistrationPOCO> ById2(int id);
		Task<vwRegistrationShell> ByEmail(string email);

		Task<int> Create(RegistrationPOCO registration);
		Task<int> Update(RegistrationPOCO registration);
		Task<int> Delete(int id);
		Task<int> MealInitialization(int registrationId);
		Task<int> UpdateMeal(int registrationId, Meal meal, AgeEnum age);
		Task<int> UpdateMealVeg(int registrationId, Meal meal, AgeEnum age);
		Task<Lunch> GetLunch(int registrationId, MealEnum mealEnum);
		Task<Dinner> GetDinner(int registrationId, MealEnum mealEnum);
		Task<MealsRelatedRegistrationData> MealsRelatedRegistrationData(int id);
		Task<RegistrationSummary> GetRegistrationSummary(int id);
		Task<int> RegistrationUpdateMealsCompleted(int id);
		Task<int> UpdateMealTicketReset(int id, int childBig, int childSmall);

		Task<List<MealTicketPunchLog2>> MealTicketPunchLogs(); // <MealTicketPunchLog>
		Task<int> MealTicketPunchInsert(MealTicketPunchLog mealTicketPunchLog);
		Task<MealTicketPunchLog> MealTicketPunchLogById(int id);
		Task<int> MealTicketPunchLogUpdate(int id, MealTicketPunchLog mealTicketPunchLog);
		Task<int> MealTicketPunchLogDelete(int id);
}
