namespace LivingMessiah.Web.Pages.SukkotAdmin.Constants
{
	public static class ErrorLog
	{
		public static class Handler
		{
			public const string LogErrorTest = "LogErrorTest";
			public const string EmptyErrorLog = "EmptyErrorLog";
		}
	}

	public static class Donations
	{
		public static class Handler
		{
			public const string Create = "Post";
		}
	}

	public static class Meals
	{
		public static class Handler
		{
			public const string EditMealPlan = "EditMealPlan";
			public const string PunchTicket = "PunchTicket";
		}
	}

	public static class KitchenWork
	{
		public static class Handler
		{
			public const string Edit = "Edit";
		}
	}


	public static class Anchors
	{
		public const string Index = "/SukkotAdmin/Index";
		public const string RegistrationList = "/SukkotAdmin/RegistrationList";
		public const string Notes = "/SukkotAdmin/Notes";

		public static class Meals
		{
			public const string Index = "/SukkotAdmin/Meals/Index";  // MealTickets = "/SukkotAdmin/Meals/Tickets";
			public const string TicketsPunched = "/SukkotAdmin/Meals/TicketsPunched";
			public const string PlannerReport = "/SukkotAdmin/Meals/PlannerReport";
			public const string ListMealPlanMenu = "/SukkotAdmin/Meals/ListMealPlanMenu";
		}

		public static class KitchenWork
		{
			public const string Index = "/SukkotAdmin/KitchenWork/Index";
			public const string Edit = "/SukkotAdmin/KitchenWork/Edit";
		}

		
		public const string GetMealPlan = "/SukkotAdmin/GetMealPlan";
		public const string EditMealPlan = "/SukkotAdmin/EditMealPlan";
		public const string PunchTicket = "/SukkotAdmin/PunchTicket";

		public const string AttendanceAllFeastDays = "/SukkotAdmin/AttendanceAllFeastDays";
		public const string AttendanceChart = "/SukkotAdmin/AttendanceChart";

		public const string LodgingDaysAll = "/SukkotAdmin/LodgingDaysAll";
		public const string LodgingDaysChart = "/SukkotAdmin/LodgingDaysChart";
		public const string LodgingDaysPivotOnCampCode = "/SukkotAdmin/LodgingDaysPivotOnCampCode";
		public const string LodgingDetails = "/SukkotAdmin/LodgingDetails";

		public const string LogErrorTest = "/SukkotAdmin/LogErrorTest";
		public const string ErrorLog = "/SukkotAdmin/ErrorLog";
		public const string ErrorLogEmpty = "/SukkotAdmin/ErrorLogEmpty";


		public static class Donations
		{
			public const string Index = "/SukkotAdmin/Donations/Index";
			public const string FormInsert = "/SukkotAdmin/Donations/FormInsert";
			public const string ByRegistration = "/SukkotAdmin/Donations/ByRegistration";
			public const string ByRegistrationId = "/SukkotAdmin/Donations/ByRegistrationId";
		}

		public const string CreateDonation = "/SukkotAdmin/CreateDonation";
	}

	public static class PartialViews
	{
		public const string MenuBar = "_SukkotAdminMenubar";
		public const string IndexButtonList = "~/Pages/SukkotAdmin/_IndexButtonList.cshtml";

		public static class Meals
		{
			public const string TicketsFilter = "_TicketsFilter";
			public const string TicketsDiv = "_TicketsDiv";
			public const string TicketsTable = "_TicketsTable2";
			public const string TicketsTablePrint = "_TicketsTablePrint";
			public const string TicketsPunchedFilter = "_TicketsPunchedFilter";
			public const string TicketsPunchedTable = "_TicketsPunchedTable";
		}

		public static class KitchenWork
		{
			public const string Filter = "_Filter";
			public const string EditModal = "_EditModal";
			public const string ListTable = "_ListTable";
			public const string DummyDataTable = "_DummyDataTable";
		}


		public static class Donations
		{
			public const string IndexFilter = "_IndexFilter";
			public const string IndexTable = "_IndexTable";
			public const string ByRegistrationTable = "_ByRegistrationTable";
			public const string PreviousDonationsTable = "_PreviousDonationsTable";
		}

		public const string ErrorRP = "~/Features/Shared/ErrorRP.cshtml";
		//public const string KitchenWorkEditModal = "_KitchenWorkEditModal";
		public const string Message = Pages.Sukkot.Constants.PartialViews.Message;   // "_Message";

	}

	public static class VDD
	{
		public const string IsXs = "IsXs";
		public const string IsMealsAvailable = "IsMealsAvailable";
		public const string RegistrationId = "RegistrationId";
		public const string FamilyName = "FamilyName";
	}

}

