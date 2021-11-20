using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

using LivingMessiah.Web.Pages.KeyDates.Data;
using LivingMessiah.Web.Pages.KeyDates.Enums;
using LivingMessiah.Web.Pages.KeyDates.Queries;

namespace LivingMessiah.Web.Pages.KeyDates.Services
{
	public interface IKeyDateService
	{
		string ExceptionMessage { get; set; }
		Task<List<YearLookup>> GetYearLookupList();
		YearLookup GetYearLookup(string relative);
		Task<List<CalendarEntry>> GetCalendarEntries(int year);
		Task<CalendarYear> GetHebrewYearAndChildren(RelativeYearEnum relativeYear); // Called by Pages\KeyDates\HebrewYear.razor
	}

	public class KeyDateService : IKeyDateService
	{
		#region Constructor and DI
		private readonly IKeyDateRepository db;
		private IMemoryCache memoryCache;
		private readonly ILogger Logger;

		public KeyDateService(
			IKeyDateRepository keyDateRepository, IMemoryCache cache, ILogger<KeyDateService> logger)
		{
			db = keyDateRepository;
			memoryCache = cache;
			Logger = logger;
		}
		#endregion

		public string ExceptionMessage { get; set; } = "";
		private List<YearLookup> YearLookupList { get; set; }

		public YearLookup GetYearLookup(string relative)
		{
			YearLookup yl = new YearLookup();
			if (YearLookupList != null)
			{
				yl = YearLookupList.Where(w => w.Text == relative).SingleOrDefault();
				if (yl == null)
				{
					yl.ID = "0";
					yl.Text = "[" + relative + "] Not Found";
				}
			}
			else
			{
				yl.ID = "0";
				yl.Text = "Unknown";
			}
			return yl;
		}

		public async Task<List<YearLookup>> GetYearLookupList()
		{
			Logger.LogDebug(string.Format("Inside {0}", nameof(KeyDateService) + "!" + nameof(GetYearLookupList)));
			if (YearLookupList != null)
			{
				return YearLookupList;
			}
			return await Populate();
		}

		private async Task<List<YearLookup>> Populate()  
		{
			Logger.LogDebug(string.Format("Inside {0}", nameof(KeyDateService) + "!" + nameof(Populate)));
			if (YearLookupList != null)
			{
				return YearLookupList;
			}
			try
			{
				YearLookupList = await db.GetYearLookupList();
				return YearLookupList;
			}
			catch (System.Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(Populate)}";
				Logger.LogError(ex, ExceptionMessage);
				throw new InvalidOperationException(ExceptionMessage);
			}
		}

		public async Task<List<CalendarEntry>> GetCalendarEntries(int year)
		{
			Logger.LogDebug(String.Format("Inside {0}, year:{1}", nameof(KeyDateService) + "!" + nameof(GetCalendarEntries), year));
			List< CalendarEntry > calendarEntries;
			try
			{
				calendarEntries = await db.GetCalendarEntries(year);

				if (calendarEntries == null)
				{
					ExceptionMessage = "Error reading database";
					throw new InvalidOperationException(ExceptionMessage);
				}
				else
				{
					return calendarEntries;
				}
			}
			catch (Exception ex)
			{
				ExceptionMessage = "Error reading database";
				Logger.LogError(ex, ExceptionMessage);
				//throw new KeyDateException(ExceptionMessage);
				throw new InvalidOperationException(ExceptionMessage);
			}
		}

		public async Task<CalendarYear> GetHebrewYearAndChildren(RelativeYearEnum relativeYear)
		{
			var cacheKey = Settings.Constants.HebrewYearAndChildrenCache.Key;

			Logger.LogDebug($"Inside {nameof(KeyDateService)}!{nameof(GetHebrewYearAndChildren)}; cacheKey:{cacheKey}; relativeYear: {(int)relativeYear}");
			if (!memoryCache.TryGetValue(cacheKey, out CalendarYear hebrewYearAndChildren))
			{
				Logger.LogDebug($"...Key NOT found in cache, calling {nameof(db.GetHebrewYearAndChildren)}");
				hebrewYearAndChildren = await db.GetHebrewYearAndChildren(relativeYear);
				Logger.LogDebug($"...After calling {nameof(db.GetHebrewYearAndChildren)}");  //; keyDates.Count: {keyDates.Count}

				if (hebrewYearAndChildren != null && hebrewYearAndChildren.Year != 0)
				{
					var cacheExpiryOptions = new MemoryCacheEntryOptions
					{
						AbsoluteExpiration = DateTime.Now.AddMinutes(Settings.Constants.HebrewYearAndChildrenCache.AbsoluteExpirationInMinutes),
						Priority = CacheItemPriority.High,
						SlidingExpiration = TimeSpan.FromMinutes(Settings.Constants.HebrewYearAndChildrenCache.SlidingExpirationInMinutes)
					};
					memoryCache.Set(cacheKey, hebrewYearAndChildren, cacheExpiryOptions);
				}
				else
				{
					Logger.LogInformation($"...hebrewYearAndChildren.Year == 0 WHICH IS WRONG!!!, so NOT saving to memoryCache. See 779-Bug-...");
				}
			}
			else
			{
				Logger.LogDebug($"...Key found in cache");
			}

			Logger.LogDebug($"...Just before returning {nameof(hebrewYearAndChildren)}; hebrewYearAndChildren.ToString(){hebrewYearAndChildren}");
			return hebrewYearAndChildren;
		}


		#region CustomExceptions Classes
		/*
		
		public class KeyDateException : Exception
		{
			public KeyDateException()
			{
			}
			public KeyDateException(string message)
					: base(message)
			{
			}

			public KeyDateException(string message, Exception inner)
					: base(message, inner)
			{
			}
		}
		*/
		#endregion

	}
}

