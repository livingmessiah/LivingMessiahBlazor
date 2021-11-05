using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using LivingMessiah.Web.Settings;

namespace LivingMessiah.Web.Pages.Sukkot
{

	public partial class Index
	{
		[Inject]
		public IOptions<AppSettings> AppSettings { get; set; }

		private bool SukkotIsOpen;
		protected override void OnInitialized()
		{
			SukkotIsOpen = AppSettings.Value.SukkotIsOpen;
		}

		public const bool IsRegistrationClosed = false;

		//ToDo: fix this
		public const string ContactName = "Ralphie";
		public const string ContactEmail = "ralphie@livingmessiah.com";
		public const string Title1 = "Late Sukkot Registration Question";
		

		//ToDo: fix this
		//public const string ArrivalDate = "Wednesday September 30th";
		//public const string CleanupDate = "Saturday, October 10th";
		//public const string ShabbatServiceDate = "October 3rd";


		//public RenderFragment DynamicContent = builder =>
		//{
		//	builder.AddContent(1, "<b>Windmill Ranch</b> near <b>Bisbee, AZ</b>");
		//};

	}
}
