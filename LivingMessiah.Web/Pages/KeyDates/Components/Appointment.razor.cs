using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
//using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.KeyDates.Queries;

namespace LivingMessiah.Web.Pages.KeyDates.Components
{
	public partial class Appointment
	{
/*
		[Inject]
		public IKeyDateRepository db { get; set; }

		[Inject]
		public ILogger<Appointment> Logger { get; set; }
*/
		[Parameter]
		public List<AppointmentData> AppointmentDataList { get; set; }
		
		protected int NumberOfMonths { get; set; } = 16;
		protected int FirstMonthOfYear { get; set; } = 9;

		public string[] ResourceName = { "Categories" };

		public List<ResourceData> TaskData { get; set; } = new List<ResourceData> {
				new ResourceData{ Text = "Month", Id= 1, Color = "#df5286" },
				new ResourceData{ Text = "Feast", Id= 2, Color = "#7fa900" },
				new ResourceData{ Text = "Season", Id= 3, Color = "#ea7a57" }
		};

		public class ResourceData
		{
			public int Id { get; set; }
			public string Text { get; set; }
			public string Color { get; set; }
		}

	}
}
