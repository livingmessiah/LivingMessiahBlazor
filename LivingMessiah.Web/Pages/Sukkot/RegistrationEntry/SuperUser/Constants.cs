﻿namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser;

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

	public static class ShowAgreementButton
	{
		public const string Icon = "fas fa-hand-point-down";  // fas fa-hand-pointer
		public const string Text = "Show Agreement";
		public const string Color = "btn btn-outline-info btn-sm";
	}

	public static class Effects
	{
		public const string ResponseMessageFailure = "An invalid operation occurred, contact your administrator";
	}

}
