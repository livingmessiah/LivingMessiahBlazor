﻿using System;

namespace LivingMessiah.Web.Pages.Sukkot.Constants

{
	public static class PayPal
	{
		public const string HostedButton = "7S848U88V95UA";
		public const string ItemMessage = "Sukkot 2020 Payment";
		public const string PaymentQuestionsEmail = "mailto:jerry@livingmessiah.com";
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

	public static class PartialViews
	{
		public const string MenuBar = SukkotAdmin.Constants.PartialViews.MenuBar;

		public const string RegistrationBody = "_RegistrationBody";
		public const string RegistrationBodyXs = "_RegistrationBodyXs";
		public const string Step2EmailVerified = "_Step2EmailVerified";
		public const string Step3Registration = "_Step3Registration";
		public const string Step4MealTicket = "_Step4MealTicket";
		public const string Step5Payment = "_Step5Payment";

		public const string Details = "_Details";
		public const string CostAccountStyle = "_CostAccountStyle";
		
		public const string Meals = "_Meals.cshtml";
		public const string MealsPrint = "_MealsPrint.cshtml";

		public const string StatusNavigation = "_StatusNavigation";
		public const string Documents = "_Documents";
		public const string SukkotBlurbFromRoot = "~/Pages/Sukkot/_Blurb.cshtml";
		public const string SukkotBlurb2FromRoot = "~/Pages/Sukkot/_Blurb2.cshtml";
		public const string SukkotTShirt = "~/Pages/Sukkot/_SukkotTShirt.cshtml";
		public const string MealsFootnotes = "_MealsFootnotes";
		public const string MealsTableHeader = "_MealsTableHeader";
		public const string MealsTableHeaderPrint = "_MealsTableHeaderPrint";

		public const string PaymentSummary = "_PaymentSummary";
		public const string PaymentSummaryXs = "_PaymentSummaryXs";

		public const string ErrorRP = "~/Pages/Shared/ErrorRP.cshtml";
		public const string HouseRulesConfirmationModal = "_HouseRulesConfirmationModal";
		public const string Message = "_Message";

	}

	public static class PDFs
	{
		public const string RegistrationWalkThrough = "sukkot-registration-walkthrough-users-manual.pdf";
		public const string PayPalWalkThrough = "sukkot-making-a-payment-with-paypal.pdf";
		
		public const string HouseRules = "sukkot-2020-house-rules.pdf";
		public const string Schedule = "sukkot-2020-schedule.pdf";
		public const string LiabilityWaiver = "sukkot-2020-liability-waiver.pdf";
		public const string ActivitiesForChildren = "sukkot-2019-activities.pdf";

		// Task 682 Explore porting the Sukkot UI
		public const string Menu = "sukkot-2019-menu.pdf";
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

	public static class VDD
	{
		public const string MealGroup = "MealGroup";
		public const string MealCost = "MealCost";
		public const string TableClass = "TableClass";
		public const string IsXs = "IsXs";
		public const string IsXs2 = "IsXs2";
	}

	public static class Other
	{
		public const string Banner = "2020-sukkot-banner-1039-400-3d.jpg";
		public const string BannerAlt = "2020 Sukkot Registration Banner";
		public const string DetailsTitle = "Living Messiah Sukkot Registration 2020";
		public const string MealTicketTitle = "LMM Sukkot 2020 Meal Ticket";
		public const string ReturnUrlSukkotRegistration = "/Sukkot/Registration";
		public const bool IsRvHookupsAvailable = true;
		public const bool IsRegistrationClosed = false;
		public const bool IsSukkotOver = false;  //Task 409
		public const string ModalIdHouseRulesXs = "houserulesxs";
		public const string ModalIdHouseRules = "houserules";

		public const string PayWithCheckModalMessage = @"
By clicking the <i class='fa fa-print'></i> <span class='text-muted'>Print</span> button,
you can choose where to print your registration form.
If for some reason you can not print the form that is OK, it just makes it convenient to process and it shows clearly the intent of your check.
<br /><br />The address of Living Messiah Ministries will be on the bottom of the form.
Make the check payable to <b>Living Messiah</b> and attach it to the printed out form before you mail it.
<br /><br />Please put the <b>registration id</b> on the check and write <b>Sukkot 2020 Payment</b>.  
<br /><br />Thanks!
";
	}

}
