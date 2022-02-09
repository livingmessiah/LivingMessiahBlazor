namespace SukkotApi.Domain;

public class vwMealTicket
{
		public bool Selected { get; set; }
		public int RegistrationId { get; set; }
		public string NameAndSpouse { get; set; }
		public string OtherNames { get; set; }
		public int AdultReg { get; set; }
		public int AdultVeg { get; set; }
		public int Child6to9Reg { get; set; }
		public int Child6to9Veg { get; set; }
		public int ChildUnder6Reg { get; set; }
		public int ChildUnder6Veg { get; set; }
}
