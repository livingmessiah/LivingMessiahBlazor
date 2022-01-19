using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using ParashaDomain = LivingMessiah.Domain.Parasha.Queries;
using CacheSettings = LivingMessiah.Web.Settings.Constants.ParashaPrintTupleCache;
using Page = LivingMessiah.Web.Links.Parasha;

namespace LivingMessiah.Web.Pages.Parasha
{
	public partial class ParashaPrint
	{
	
		[Inject] private LivingMessiah.Data.IShabbatWeekRepository db { get; set; }
		[Inject] public IMemoryCache Cache { get; set; }
		[Inject] public ILogger<ParashaPrint> Logger { get; set; }

		protected ParashaDomain.BibleBook BibleBook;
		protected List<ParashaDomain.ParashaList> ParashaList;

		protected string CachedMsg { get; set; }
		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug(string.Format("Inside Page: {0}, Class!Method: {1}", Page.IndexPrint, nameof(ParashaPrint) + "!" + nameof(OnInitializedAsync)));
			CachedMsg = "";
			Tuple<ParashaDomain.BibleBook, List<ParashaDomain.ParashaList>> ParashaListTuple;

			BibleBook = Cache.Get<ParashaDomain.BibleBook>(CacheSettings.Keys.Item1BibleBook);
			ParashaList = Cache.Get<List<ParashaDomain.ParashaList>>(CacheSettings.Keys.Item2ParashaList);

			Logger.LogDebug(string.Format("...TOP BibleBook {0} null, ParashaList {1} null"
				, BibleBook == null ? "is" : "is NOT"
				, ParashaList == null ? "is" : "is NOT"));

			if (BibleBook is null | ParashaList is null)
			{
				try
				{
					ParashaListTuple = await db.GetParashotForCurrentBook();
					if (ParashaListTuple is not null)
					{
						BibleBook = ParashaListTuple.Item1;
						Cache.Set(CacheSettings.Keys.Item1BibleBook, BibleBook, TimeSpan.FromMinutes(CacheSettings.FromMinutes));
						ParashaList = ParashaListTuple.Item2;
						CachedMsg = "Data gotten from DATABASE";
						Cache.Set(CacheSettings.Keys.Item2ParashaList, ParashaList, TimeSpan.FromMinutes(CacheSettings.FromMinutes));
						Logger.LogDebug(string.Format("...Cache Set; Keys.Item1BibleBook: {0}, Keys.Item2ParashaList: {1}; FromMinutes:{2}"
							, CacheSettings.Keys.Item1BibleBook
							, CacheSettings.Keys.Item2ParashaList
							, CacheSettings.FromMinutes));

					}
					else
					{
						DatabaseWarning = true;
						DatabaseWarningMsg = $"{nameof(BibleBook)} or {nameof(ParashaList)} NOT FOUND";
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
				CachedMsg = "Data gotten from CACHE";
			}

			Logger.LogDebug(string.Format("...BOT BibleBook {0} null, ParashaList {1} null"
				, BibleBook == null ? "is" : "is NOT"
				, ParashaList == null ? "is" : "is NOT"));

		}

		#region ErrorHandling
		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }
		#endregion
	}
}