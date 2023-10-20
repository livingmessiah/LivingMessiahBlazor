using LivingMessiah.Web.Pages.Sukkot.ManageRegistration.Index;

namespace LivingMessiah.Web.Pages.Sukkot.ManageRegistration;

public static class Constants
{
	public static PageHeaderVM GetPageHeaderForIndexVM()
	{
		return new PageHeaderVM
		{
			Title = "Index",
			Icon = "fas fa-list",
			Color = "text-primary",
			Id = 0
		};
	}

	public static class RepopulateButton
	{
		public const string Icon = "fas fa-retweet";
		public const string Text = " Re-populate";
		public const string Color = "text-danger";
		public const string ButtonColor = "btn btn-outline-warning";  //text-warning btn-sm  float-end
	}

	public static class SaveButton
	{
		public const string Icon = "fas fa-save";
		public const string Color = "btn btn-outline-success btn-sm";
	}

	public static class CancelButton
	{
		public const string Icon = "far fa-window-close";
		public const string Text = "Cancel";
		public const string Color = "btn btn-outline-secondary btn-sm";
	}

	public static class Effects
	{
		public const string ResponseMessageFailure = "An invalid operation occurred, contact your administrator";
		public const string RepopulateMessage = " *** CLICK THE REPOPULATE BUTTON! ***";
	}

	public static class HRA
	{

		public static class ShowButton
		{
			public const string Icon = "fas fa-signature";
			public const string Text = "Show HRA Form";
			public const string ButtonColor = "btn btn-outline-danger";
		}

		public static class PageHeader
		{
			public const string Icon = "fas fa-signature";
			public const string Text = "Add HRA";
			public const string Color = "text-danger";
		}

		public static class AgreeButton
		{
			public const string Icon = "fas fa-thumbs-up"; 
			public const string Text = " Yes, I Agree";
			public const string Color = "text-danger";
			public const string ButtonColor = "btn btn-outline-success";  
		}

		public static class DidNotAgreeButton
		{
			public const string Icon = "far fa-hand-paper";
			public const string Text = " No, I Do NOT Agree";
			public const string Color = "text-danger";
			public const string ButtonColor = "btn btn-outline-danger";
			public const string ResponseMsg = "Not agreeing to the House Rules Agreement terminates the Registration Process";
		}
	}
}
// Ignore Spelling: HRA
