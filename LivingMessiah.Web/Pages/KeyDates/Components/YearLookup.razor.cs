using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.KeyDates.Queries;

using LivingMessiah.Web.Pages.KeyDates.Services;
using LivingMessiah.Web.Pages.KeyDates.Data;

using Syncfusion.Blazor.DropDowns;

namespace LivingMessiah.Web.Pages.KeyDates.Components
{
	public partial class YearLookup
	{
		[Inject]
		public IKeyDateService svc { get; set; }

		[Inject]
		public IKeyDateRepository db { get; set; }

		[Inject]
		public ILogger<YearLookup> Logger { get; set; }
		
		[Parameter]
		public string YearId { get; set; }
		//public int YearId { get; set; }

		//[Parameter]
		//public EventCallback<string> YearChanged { get; set; }
		//public EventCallback<int> YearChanged { get; set; }

		protected List<YearLookupVM> YearLookupList { get; set; }

		public string ChangedID { get; set; }
		public string ChangedText { get; set; }

		#region email
		[Parameter]
		public string Email { get; set; }

		[Parameter]
		public EventCallback<string> EmailChanged { get; set; }

		private Task OnEmailChanged(ChangeEventArgs e)
		{
			Email = e.Value.ToString();
			return EmailChanged.InvokeAsync(Email);
		}
		#endregion

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug(string.Format("Inside {0}", nameof(CalendarGrid) + "!" + nameof(OnInitializedAsync)));
			try
			{
				//YearLookupList = await svc.GetYearLookupList();
				YearLookupList = await db.GetYearLookupVMList();

				//	int currentYear = int.TryParse(yearLookup.ID, out currentYear) ? currentYear : 0;
				//	ChangedID = currentYear.ToString();
				//	ChangedText = yearLookup.Text;
				//	return currentYear;
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, String.Format("...ChangedID={0}, ChangedText={1}", ChangedID, ChangedText));
			}
		}

		int currentYear;
		//async Task 
		//public Task OnChange(ChangeEventArgs<string, YearLookupVM> args)
		//private Task OnChange(ChangeEventArgs<string, YearLookupVM> args)
		//public 
		public void OnChange(ChangeEventArgs<string, YearLookupVM> args)
		{
			//Logger.LogDebug(String.Format("Inside {0}, Previous ChangedId:{1}"
			//	, nameof(YearLookup) + "!" + nameof(OnChange), ChangedID));

			ChangedID = args.ItemData.ID;
			ChangedText = args.ItemData.Text;

			currentYear = int.TryParse(args.ItemData.ID, out currentYear) ? currentYear : 0;

			//YearId = currentYear;
			YearId = ChangedID;
			Email = ChangedID;

			//Logger.LogDebug(String.Format("...ChangedID:{0}, ChangedText:{1}, currentYear:{2}"
			//	, ChangedID, ChangedText, currentYear));


			StateHasChanged();

			//return YearChanged.InvokeAsync(currentYear);
			//return YearChanged.InvokeAsync(YearId);
			

			//await PopulateCalendarEntries(currentYear);
			//ReturnYear();
		}

		//private Task ReturnYear()
		//{
		//	return YearChanged.InvokeAsync(currentYear);
		//}

	}
}
