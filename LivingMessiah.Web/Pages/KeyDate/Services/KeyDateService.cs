using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.KeyDate.Domain;
using LivingMessiah.Web.Pages.KeyDate.Data;
using System.Linq;

namespace LivingMessiah.Web.Pages.KeyDate.Services
{
	public interface IKeyDateService
	{
		string ExceptionMessage { get; set; }
		Task<List<YearLookup>> GetYearLookupList();
		YearLookup GetYearLookup(string relative);
		Task<List<CalendarEntry>> GetCalendarEntries(int year);
	}

	public class KeyDateService : IKeyDateService
	{
		#region Constructor and DI
		private readonly IKeyDateRepository db;
		private readonly ILogger Logger;

		public KeyDateService(
			IKeyDateRepository keyDateRepository, ILogger<KeyDateService> logger)
		{
			db = keyDateRepository;
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
			List<CalendarEntry> calendarEntries;
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

