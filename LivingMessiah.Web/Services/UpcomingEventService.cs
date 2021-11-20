using LivingMessiah.Web.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using LivingMessiah.Web.Pages.KeyDates.Enums;
using LivingMessiah.Web.Pages.KeyDates.Queries;

namespace LivingMessiah.Web.Services
{
	public interface IUpcomingEventService
	{
		Task<List<UpcomingEvent>> GetEvents(int daysAhead, int daysPast); // Called by: UpcomingEvents\Index.razor.cs
	}

	public class UpcomingEventService : IUpcomingEventService
	{
		#region Constructor and DI
		private readonly LivingMessiah.Web.Pages.UpcomingEvents.Data.IUpcomingEventsRepository db;
		private IMemoryCache memoryCache;
		private readonly ILogger log;

		public UpcomingEventService(
			LivingMessiah.Web.Pages.UpcomingEvents.Data.IUpcomingEventsRepository dbRepository
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

	}
}
