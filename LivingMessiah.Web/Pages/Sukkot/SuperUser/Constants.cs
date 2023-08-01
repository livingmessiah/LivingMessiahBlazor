using LivingMessiah.Web.Pages.Sukkot.SuperUser.Index;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser;

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
		public static class DidNotAgreeButton
		{
			public const string Text = "Not agreeing to the House Rules Agreement terminates the Registration Process";
			public const string Color = "text-danger";
		}
	}


}
