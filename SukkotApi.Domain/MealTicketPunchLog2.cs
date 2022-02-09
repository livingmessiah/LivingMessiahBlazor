namespace SukkotApi.Domain;

public class MealTicketPunchLog2
{
		public int Id { get; set; }
		public int RegistrationId { get; set; }
		public string MealType { get; set; }
		public int AgeEnum { get; set; }
		public int MealDateId { get; set; }
		public string MealTime { get; set; }
}
