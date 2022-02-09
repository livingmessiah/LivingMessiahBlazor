namespace SukkotApi.Domain;

public class tvfMealTicketPunchLog
{
		public int Id { get; set; }
		public int RegistrationId { get; set; }
		public int MealDateTimeId { get; set; }
		public int MealTypeId { get; set; } //  (R)egular or (V)egetarian
		public AgeEnum AgeEnum { get; set; }
}
