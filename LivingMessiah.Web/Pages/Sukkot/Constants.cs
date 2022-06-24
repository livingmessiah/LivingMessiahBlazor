namespace LivingMessiah.Web.Pages.Sukkot.Constants;


public static class AttendanceDateRange
{
	public const string Min = "10/09/2022";
	public const string Max = "10/17/2022";
}


public static class Year
{
	public const int Int = 2022;
	public const string String = "2022";
}

public static class RegistrationClosedEmail
{
	public static string Subject { get; set; } = $"Late Sukkot {Year.String} Registration Question";
	public const string Name = "Ralphie";
	public const string Email = "ralphie@livingmessiah.com";
}

public static class PayPal
{
	public const string HostedButton = "7S848U88V95UA";
	public static string ItemMessage { get; set; } = $"Sukkot {Year.String} Payment";
	public const string PaymentQuestionsEmail = "mailto:peribeth@livingmessiah.com";
}

public static class Routing
{
	public const string PaymentConfirm = "Sukkot/PaymentConfirm";
	public const string PaymentConfirmHtml = "Sukkot/PaymentConfirm.html";
	public const string PaymentCanceled = "Sukkot/PaymentCanceled";
	public const string PaymentCanceledHtml = "Sukkot/PaymentCanceled.html";
}

public static class TShirts
{
	public static bool IsAvailableForSale { get; set; } = false;
	public static string ForSaleMessage { get; set; } = "T-Shirts are available for sale, click the image below.";
	public static string ComingSoonMessage { get; set; } = "T-Shirts not yet available, check back later. ";

	public static string ImgSrc
	{
		get
		{
			return  Blobs.UrlRoot("2021-sukkot-tshirt-1024-385.jpg");
		}
	}
	public static string Href { get; set; } = "https://aboveallimages.net/shop/ols/categories/sukkot";
}

public static class Features
{
	public static bool IsTrackingPassports { get; set; } = false; 
}

public static class PostActions
{
	public const string Create = "Create";
	public const string Edit = "Edit";
}

public static class PDFs
{
	public const string RegistrationWalkThrough = "sukkot-registration-walkthrough-users-manual.pdf";
	public const string PayPalWalkThrough = "sukkot-making-a-payment-with-paypal.pdf";
	public const string WindmillRanchSupplementalRules = "windmill-ranch-supplemental-rules.pdf";
	public const string WindmillRanchSurroundingArea = "windmill-ranch-surrounding-area.pdf";
	public const string Schedule = "sukkot-2022-schedule.pdf";
	public const string LiabilityWaiver = "sukkot-2022-liability-waiver.pdf";
	//public const string ActivitiesForChildren = "sukkot-2019-activities.pdf";
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
	public static bool IsThereEarlyRegistration { get; set; } = false;
	public static System.DateTime EarlyRegistrationLastDay = new System.DateTime(Year.Int, 9, 20);
	public const decimal EarlyRegistrationFee = 60.0m;
	public static System.DateTime RegistrationLastDay = new System.DateTime(Year.Int, 9, 20);
	public const decimal RegistrationFee = 60.0m;

	// Represented in the Sql Server table Sukkot.Constants; columngs AttendanceMinDate and AttendanceMaxDate
	public const string IntroductionDates = "sundown Sunday, October 9, through sundown Monday, October 17th"; 
}



public static class Other
{
	public const string Banner = "2022-sukkot-banner-1023-582-3d.jpg"; 
	public const string BannerAlt = $"{Year.String} Sukkot Registration Banner";
	public static string DetailsTitle { get; set; } = $"Living Messiah Sukkot Registration {Year.String}";
	 
	public const bool IsRvHookupsAvailable = true;
	public const string ModalIdHouseRulesXs = "houserulesxs";
	public const string ModalIdHouseRules = "houserules";

	public const string PayWithCheckModalMessage = @$"
By clicking the <i class='fa fa-print'></i> <span class='text-muted'>Print</span> button,
you can choose where to print your registration form.
If for some reason you can not print the form that is OK, it just makes it convenient to process and it shows clearly the intent of your check.
<br /><br />The address of Living Messiah Ministries will be on the bottom of the form.
Make the check payable to <b>Living Messiah</b> and attach it to the printed out form before you mail it.
<br /><br />Please put the <b>registration id</b> on the check and write <b>Sukkot {Year.String} Payment</b>.  
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
