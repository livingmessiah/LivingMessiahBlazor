using LivingMessiah.Web.Features.Admin.Video.Index;

namespace LivingMessiah.Web.Features.Admin.Video;

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

	public static class ShowTableButton
	{
		public const string Icon = "fas fa-table";
		public const string Text = "Show Table";
		public const string ButtonColor = "btn btn-outline-danger";
		
		public static class PageHeader
		{
			public const string Icon = ShowTableButton.Icon;
			public const string Text = "Weekly Videos";
			public const string Color = "text-danger";
		}
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
		public const int WeekCount = 3;
		public const int WeekVideoCount = 6;
	}

	public static class FluxorStores
	{
		public const string Index = "VMD_List";
		public const string AddEdit = "VMD_AddEdit";
		public const string MasterList = "VMD_MasterList";
	}

}
