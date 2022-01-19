using System;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Domain.Parasha.Queries;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;

using Page = LivingMessiah.Web.Links.Parasha;
using CacheSettings = LivingMessiah.Web.Settings.Constants.ParashaCache;

using Microsoft.Extensions.Caching.Memory;

namespace LivingMessiah.Web.Pages.Parasha
{
	public partial class Index
	{
		[Inject] private LivingMessiah.Data.IShabbatWeekRepository db { get; set; }
		[Inject] public IMemoryCache Cache { get; set; }

		[Inject]
		public ILogger<Index> Logger { get; set; }

		[Inject]
		public Services.ILinkService LinkService { get; set; }

		protected LivingMessiah.Domain.Parasha.Queries.Parasha Parasha;

		protected Domain.Link TorahTuesdayLink { get; set; }

		protected string CachedMsg { get; set; }
		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug(string.Format("Inside Page: {0}, Class!Method: {1}", Page.Index, nameof(Index) + "!" + nameof(OnInitializedAsync)));
			TorahTuesdayLink = GetTorahTuesdayLink();			
			CachedMsg = "";
			Parasha = Cache.Get<LivingMessiah.Domain.Parasha.Queries.Parasha>(CacheSettings.Key);

			if (Parasha is null)
			{
				try
				{
					Logger.LogDebug(string.Format("...Key NOT found in cache, calling {0}", nameof(db.GetCurrentParashaAndChildren)));
					Parasha = await db.GetCurrentParashaAndChildren();
					Logger.LogDebug(string.Format("...After calling {0}; Parasha: {1}", nameof(db.GetCurrentParashaAndChildren), Parasha));

					if (Parasha is not null)  
					{
						//CachedMsg = "Data gotten from DATABASE";
						Logger.LogDebug(string.Format("...Parasha gotten from DATABASE, Parasha: {0}", Parasha));
						Cache.Set(CacheSettings.Key, Parasha, TimeSpan.FromMinutes(CacheSettings.FromMinutes));
						Logger.LogDebug(string.Format("...Set Cache Key: {0}, TimeSpan.FromMinutes{1}"
							, CacheSettings.Key, CacheSettings.FromMinutes));
					}
					else
					{
						DatabaseWarning = true;
						DatabaseWarningMsg = "Could not load because Current Parasha Unknown";
						Logger.LogDebug(string.Format("...Parasha NOT found, DatabaseWarningMsg: {0}", DatabaseWarningMsg));
					}

				}
				catch (Exception ex)
				{
					DatabaseError = true;
					DatabaseErrorMsg = $"Error reading database";
					Logger.LogError(ex, string.Format("...Exception, DatabaseErrorMsg: {0}", DatabaseErrorMsg));
				}
			}
			else
			{
				//CachedMsg = "Data gotten from CACHE";
				Logger.LogDebug(string.Format("... Data gotten from CACHE"));
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


		#region ErrorHandling
		protected bool DatabaseInformation = false;
		protected string DatabaseInformationMsg { get; set; } = string.Empty;
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; } = string.Empty;
		protected bool DatabaseError { get; set; }
		protected string DatabaseErrorMsg { get; set; } = string.Empty;
		#endregion

	}
}
