using LivingMessiah.Web.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using LivingMessiah.Domain.KeyDates.Enums;
using LivingMessiah.Domain.KeyDates.Queries;

namespace LivingMessiah.Web.Services
{
	public interface IUpcomingEventService
	{
		Task<List<UpcomingEvent>> GetEvents(int daysAhead, int daysPast);
		Task<CalendarYear> GetHebrewYearAndChildren(RelativeYearEnum relativeYear);
	}

	public class UpcomingEventService : IUpcomingEventService
	{
		#region Constructor and DI
		private readonly LivingMessiah.Data.IUpcomingEventsRepository db;
		private IMemoryCache memoryCache;
		private readonly ILogger log;

		public UpcomingEventService(
			LivingMessiah.Data.IUpcomingEventsRepository dbRepository
			, IMemoryCache cache
			, ILogger<UpcomingEventService> logger
			)
		{
			db = dbRepository;
			memoryCache = cache;
			log = logger;
		}
		#endregion

		public async Task<List<UpcomingEvent>> GetEvents(int daysAhead, int daysPast)
		{
			return await db.GetEvents(daysAhead, daysPast);
		}

		public async Task<CalendarYear> GetHebrewYearAndChildren(RelativeYearEnum relativeYear)
		{
			var cacheKey = Settings.Constants.HebrewYearAndChildrenCache.Key;

			log.LogDebug($"Inside {nameof(UpcomingEventService)}!{nameof(GetHebrewYearAndChildren)}; cacheKey:{cacheKey}; relativeYear: {(int)relativeYear}");
			if (!memoryCache.TryGetValue(cacheKey, out CalendarYear hebrewYearAndChildren))
			{
				log.LogDebug($"...Key NOT found in cache, calling {nameof(db.GetHebrewYearAndChildren)}");
				hebrewYearAndChildren = await db.GetHebrewYearAndChildren(relativeYear);
				log.LogDebug($"...After calling {nameof(db.GetHebrewYearAndChildren)}");  //; keyDates.Count: {keyDates.Count}

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
					log.LogInformation($"...hebrewYearAndChildren.Year == 0 WHICH IS WRONG!!!, so NOT saving to memoryCache. See 779-Bug-...");
				}
			}
			else
			{
				log.LogDebug($"...Key found in cache");
			}

			log.LogDebug($"...Just before returning {nameof(hebrewYearAndChildren)}; hebrewYearAndChildren.ToString(){hebrewYearAndChildren}");
			return hebrewYearAndChildren;

		}

	}
}
