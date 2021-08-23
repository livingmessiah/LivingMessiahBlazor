using System;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Domain.Parasha.Queries;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;

namespace LivingMessiah.Web.Pages.Parasha
{
	public partial class Index
	{
		[Inject]
		public Services.IShabbatWeekCacheService Svc { get; set; }

		[Inject]
		public ILogger<Index> Logger { get; set; }

		[Inject]
		public Services.ILinkService LinkService { get; set; }

		protected LivingMessiah.Domain.Parasha.Queries.Parasha Parasha;

		protected Domain.Link TorahTuesdayLink { get; set; }

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(Index)}!{nameof(OnInitializedAsync)}");
			try
			{
				TorahTuesdayLink = GetTorahTuesdayLink();

				Parasha = await Svc.GetCurrentParasha();
				if (Parasha != null)  // if (Parasha is not null C# 9)
				{
					Logger.LogDebug($"Parasha found. Parasha.ToString(): {Parasha}");
				}
				else
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = $"No parasha found";
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
		}

		protected bool MakeModalVisible = false;
		void HebrewMonthsStatic_ButtonClick()
		{
			Logger.LogDebug($"Event: {nameof(HebrewMonthsStatic_ButtonClick)} clicked");
			MakeModalVisible = true;
			StateHasChanged();
		}

		private Domain.Link GetTorahTuesdayLink()
		{
			return LinkService.GetHomeSidebarLinks().Where(w => w.Index == LivingMessiah.Web.Links.TorahTuesday.Index).SingleOrDefault();
		}

	}
}
