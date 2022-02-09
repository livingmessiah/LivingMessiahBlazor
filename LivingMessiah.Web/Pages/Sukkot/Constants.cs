namespace LivingMessiah.Web.Pages.Sukkot.Constants;

public static class PayPal
{
		public const string HostedButton = "7S848U88V95UA";
		public const string ItemMessage = "Sukkot 2021 Payment";
		public const string PaymentQuestionsEmail = "mailto:peribeth@livingmessiah.com";
}

public static class Routing
{
		public const string PaymentConfirm = "Sukkot/PaymentConfirm";
		public const string PaymentConfirmHtml = "Sukkot/PaymentConfirm.html";
		public const string PaymentCanceled = "Sukkot/PaymentCanceled";
		public const string PaymentCanceledHtml = "Sukkot/PaymentCanceled.html";
}


public static class PostActions
{
		public const string Create = "Create";
		public const string Edit = "Edit";
		public const string EditMeals = "EditMeals";
		public const string KitchenWork = "KitchenWork";
}

public static class PDFs
{
		public const string RegistrationWalkThrough = "sukkot-registration-walkthrough-users-manual.pdf";
		public const string PayPalWalkThrough = "sukkot-making-a-payment-with-paypal.pdf";



		//welcome-vision-mission-resolve.pdf
		//public const string HouseRules = "sukkot-2020-house-rules.pdf";

		public const string WildernessRanchInfoPacket = "sukkot-2021-wilderness-ranch.pdf";
		public const string WindmillRanchSupplementalRules = "windmill-ranch-supplemental-rules.pdf";
		public const string WindmillRanchSurroundingArea = "windmill-ranch-surrounding-area.pdf";
		public const string Schedule = "sukkot-2021-schedule.pdf";
		public const string LiabilityWaiver = "sukkot-2021-liability-waiver.pdf";
		//public const string ActivitiesForChildren = "sukkot-2019-activities.pdf";

		// Task 682 Explore porting the Sukkot UI
		//public const string Menu = "sukkot-2019-menu.pdf";
		public const string Passport = "passport-v1.pdf";
}

public static class Blobs
{
		private const string root = "https://livingmessiahstorage.blob.core.windows.net/images/";
		private const string maps = "https://livingmessiahstorage.blob.core.windows.net/images/events/";
		private const string pdf = "https://livingmessiahstorage.blob.core.windows.net/pdfs/";
		private const string events = "https://livingmessiahstorage.blob.core.windows.net/images/events/";

		public static string Url(string blob)
		{
				return maps + blob;
		}
		public static string UrlPDF(string blob)
		{
				return pdf + blob;
		}

		public static string UrlRoot(string blob)
		{
				return root + blob;
		}

		public static string UrlEvents(string blob)
		{
				return events + blob;
		}
}

public static class RegistrationMeta
{
		public static System.DateTime EarlyRegistrationLastDay = new System.DateTime(2021, 9, 20);
		public const decimal EarlyRegistrationFee = 30.0m;
		public static System.DateTime RegistrationLastDay = new System.DateTime(2021, 10, 14);
		public const decimal RegistrationFee = 50.0m;
}

public static class Other
{
		public const string Banner = "2021-sukkot-banner-1024-385-3d.jpg"; // 2021-sukkot-banner-1039-x-530.jpg
		public const string BannerAlt = "2021 Sukkot Registration Banner";
		public const string DetailsTitle = "Living Messiah Sukkot Registration 2021";
		public const string MealTicketTitle = "LMM Sukkot 2020 Meal Ticket";
		//public const string ReturnUrlSukkotRegistration = "/Sukkot/Registration";
		public const bool IsRvHookupsAvailable = true;
		public const string ModalIdHouseRulesXs = "houserulesxs";
		public const string ModalIdHouseRules = "houserules";

		public const string PayWithCheckModalMessage = @"
By clicking the <i class='fa fa-print'></i> <span class='text-muted'>Print</span> button,
you can choose where to print your registration form.
If for some reason you can not print the form that is OK, it just makes it convenient to process and it shows clearly the intent of your check.
<br /><br />The address of Living Messiah Ministries will be on the bottom of the form.
Make the check payable to <b>Living Messiah</b> and attach it to the printed out form before you mail it.
<br /><br />Please put the <b>registration id</b> on the check and write <b>Sukkot 2021 Payment</b>.  
<br /><br />Thanks!
";
}

// ToDo: maybe this does not belong here?
public static class SqlServer
{
		public const int ReturnValueOk = 0;
		public const int ReturnValueViolationInUniqueIndex = 2601;
		public const string ReturnValueName = "ReturnValue";
		public const string ReturnValueParm = "@ReturnValue";
}
