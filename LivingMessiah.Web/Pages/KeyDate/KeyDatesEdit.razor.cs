using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;

using LivingMessiah.Web.Pages.UpcomingEvents.Data;
using LivingMessiah.Web.Pages.KeyDates.Commands;
using LivingMessiah.Web.Pages.KeyDates.Enums;

using LivingMessiah.Web.Pages.UpcomingEvents.Data.Commands;


using Syncfusion.Blazor.Grids;

namespace LivingMessiah.Web.Pages.KeyDate
{
	[Authorize(Roles = Roles.AdminOrKeyDates)]
	public partial class KeyDatesEdit
	{
		[Inject]
		public IUpcomingEventsRepository db { get; set; }

		[Inject]
		public IUpcomingEvents dbCommands { get; set; }

		[Inject]
		public ILogger<KeyDatesEdit> Logger { get; set; }

		[Parameter]
		public RelativeYearEnum RelativeYear { get; set; } = RelativeYearEnum.Next;

		public List<LivingMessiah.Web.Pages.KeyDates.Queries.AppointmentData> AppointmentDataList;

		protected List<DateUnion> DateUnionList;

		protected int NumberOfMonths { get; set; } = 16;
		protected int FirstMonthOfYear { get; set; } = 9;

		public List<ResourceData> TaskData { get; set; } = new List<ResourceData> {
				new ResourceData{ Text = "Month", Id= 1, Color = "#df5286" },
				new ResourceData{ Text = "Feast", Id= 2, Color = "#7fa900" },
				new ResourceData{ Text = "Season", Id= 3, Color = "#ea7a57" }
		};

		public string[] ResourceName = { "Categories" };

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(KeyDatesEdit)}!{nameof(OnInitializedAsync)}");
			try
			{
				DateUnionList = await db.GetDateUnionList(RelativeYear);
				if (DateUnionList == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = "DateUnionList NOT FOUND";
				}
				else
				{
					LoadAppointmentDataLista();
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}


		}

		private void LoadAppointmentDataLista()
		{
			AppointmentDataList = new List<LivingMessiah.Web.Pages.KeyDates.Queries.AppointmentData>();

			foreach (var item in DateUnionList)
			{
				AppointmentDataList.Add(new LivingMessiah.Web.Pages.KeyDates.Queries.AppointmentData
				{
					Id = item.Id,
					Subject = item.Descr,
					Description = item.Descr,
					StartTime = item.Date,
					EndTime = item.Date,
					IsAllDay = true
				}
				);
			}


		}

		public async Task OnSave(BeforeBatchSaveArgs<DateUnion> Args)
		{
			var BatchChanges = Args.BatchChanges;
			int rows = 0;

			if (BatchChanges.ChangedRecords.Count > 0)
			{
				Logger.LogDebug($"Changed Records: {BatchChanges.ChangedRecords.Count}");
				try
				{
					foreach (var item in BatchChanges.ChangedRecords)
					{
						rows += await dbCommands.UpdateKeyDate(item.Id, item.Date);
					}

				}
				catch (Exception ex)
				{
					DatabaseError = true;
					DatabaseErrorMsg = $"Error updating database";
					Logger.LogError(ex, $"...{DatabaseErrorMsg}");
				}
				Logger.LogDebug($"...rows: {rows}");
			}

			//if (BatchChanges.AddedRecords.Count > 0)
			//{
			//	Logger.LogDebug($"Added Records: {BatchChanges.AddedRecords.Count}");
			//	//Insert data into your database 
			//}

			//if (BatchChanges.DeletedRecords.Count > 0)
			//{
			//	Logger.LogDebug($"Deleted Records: {BatchChanges.DeletedRecords.Count}");
			//	//delete record from your database 
			//}
		}

		public class ResourceData
		{
			public int Id { get; set; }
			public string Text { get; set; }
			public string Color { get; set; }
		}
	}
}
