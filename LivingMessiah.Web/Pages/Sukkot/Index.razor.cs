using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;

namespace LivingMessiah.Web.Pages.Sukkot
{
	[AllowAnonymous]
	public partial class Index
	{
		public DateTime EarlyRegistrationLastDay = DateTime.Parse("9/20/2021");
		public const decimal EarlyRegistrationFee = 30.0m;
		public DateTime RegistrationLastDay = DateTime.Parse("10/14/2021");
		public const decimal RegistrationFee = 50.0m;

		public const bool IsRegistrationClosed = false;

		//ToDo: fix this
		public const string ContactName = "Ralphie";
		public const string ContactEmail = "ralphie@livingmessiah.com";
		public const string Title1 = "Late Sukkot Registration Question";
		//public const string Dates = "Thursday, October 1, through sundown Saturday, October 10"; <span class="text-warning">
		public const string Dates = "Sundown Wednesday, October 20, through sundown Thursday, October 28th";

		//ToDo: fix this
		public const string ArrivalDate = "Wednesday September 30th";
		public const string CleanupDate = "Saturday, October 10th";
		public const string ShabbatServiceDate = "October 3rd";


		//public RenderFragment DynamicContent = builder =>
		//{
		//	builder.AddContent(1, "<b>Windmill Ranch</b> near <b>Bisbee, AZ</b>");
		//};

	}
}
