//using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using static LivingMessiah.Web.Pages.Shavuot.Domain.OmerGematriaFactory;
using LivingMessiah.Web.Infrastructure;
using System.Collections.Generic;
using static LivingMessiah.Web.Features.Calendar.ScheduleData;
using System.Linq;
using System;

namespace LivingMessiah.Web.Features.Calendar.Data;

// copied from Features\UpcomingEvents\Weekly\CacheService.cs
public interface IService
{
	List<ReadonlyEventsData> GetData();
}

public class Service : IService
{
	#region Constructor and DI
	private readonly IRepository db;
	//private IMemoryCache memoryCache;
	private readonly ILogger Logger;

	public Service(
		IRepository dbRepository
		//, IMemoryCache cache
		, ILogger<Service> logger
		)
	{
		db = dbRepository;
		//memoryCache = cache;
		Logger = logger;
	}

	#endregion

	public List<ReadonlyEventsData> GetData()
	{
		string inside = $"{nameof(Service)}!{nameof(GetData)}";
		int _RunningCount = 0;
		Logger!.LogDebug(string.Format("Inside {0}", inside));
		
		List<ReadonlyEventsData>? _DataList = new List<ReadonlyEventsData>();
		
		var _Tuple1 = LoadFeastDaysExceptHanukkah(_RunningCount, _DataList);
		Logger!.LogDebug(string.Format("...After {0}  _Tuple1.RunningCount: {1}", nameof(LoadFeastDaysExceptHanukkah), _Tuple1.RunningCount));

		var _Tuple2 = LoadFeastDayDetails(_Tuple1.RunningCount, _Tuple1.DataList);
		Logger!.LogDebug(string.Format("...After {0} _Tuple2.RunningCount: {1}", nameof(LoadFeastDayDetails), _Tuple2.RunningCount));

		var _Tuple3 = LoadOmerDates(_Tuple2.RunningCount, _Tuple2.DataList);
		Logger!.LogDebug(string.Format("...After {0} _Tuple3.RunningCount: {1}", nameof(LoadOmerDates), _Tuple3.RunningCount));

		var _Tuple4 = LoadHanukkahDates(_Tuple3.RunningCount, _Tuple3.DataList);
		Logger!.LogDebug(string.Format("...After {0} _Tuple4.RunningCount: {1}", nameof(LoadHanukkahDates), _Tuple4.RunningCount));

		var _Tuple5 = LoadMonths(_Tuple4.RunningCount, _Tuple4.DataList);
		Logger!.LogDebug(string.Format("...After {0} _Tuple5.RunningCount: {1}", nameof(LoadMonths), _Tuple5.RunningCount));

		var _Tuple6 = LoadSeasons(_Tuple5.RunningCount, _Tuple5.DataList);
		Logger!.LogDebug(string.Format("...After {0} _Tuple6.RunningCount: {1}", nameof(LoadSeasons), _Tuple6.RunningCount));

		return _Tuple6.DataList;
	}

	private static (int RunningCount, List<ReadonlyEventsData> DataList) 
		LoadFeastDaysExceptHanukkah(int runningCount, List<ReadonlyEventsData> dataList)
	{
		int i = 0;
		foreach (var fd in Enums.FeastDay.List
												.Where(w => w.Value != Enums.FeastDay.Hanukkah)
												.OrderBy(o => o.Value).ToList())
		{
			i += 1;
			dataList!.Add(new ReadonlyEventsData
			{
				Id = i,
				Subject = fd.CalendarTitle,
				Description = fd.Details,
				StartTime = fd.Date,
				EndTime = fd.Date,
				CategoryColor = Enums.DateType.Feast.CalendarColor,  // ToDo: Use Turquoise for Passover cuz it's not a High Sabbath
				IsAllDay = true,
				IsReadonly = true
			}
			);
		}

		return (runningCount + i, dataList);
	}

	private static (int RunningCount, List<ReadonlyEventsData> DataList) 
		LoadFeastDayDetails(int runningCount, List<ReadonlyEventsData> dataList)
	{
		DateTime date;

		int i = 0;
		foreach (var item in Enums.FeastDayDetail.List.ToList())
		{
			i += 1;
			date = Enums.FeastDay.FromValue(item.ParentFeastDayId).Date;
			dataList!.Add(new ReadonlyEventsData
			{
				Id = i + runningCount,
				Subject = item.Title,
				Description = item.Description,
				StartTime = date.AddDays(item.AddDays),
				EndTime = date.AddDays(item.AddDays),
				CategoryColor = Enums.DateType.Feast.CalendarColor,
				IsAllDay = true,
				IsReadonly = true
			}
			);
		}
		return (runningCount + i, dataList);
	}

	private static (int RunningCount, List<ReadonlyEventsData> DataList)
		 LoadOmerDates(int runningCount, List<ReadonlyEventsData> dataList)
	{
		DateTime startDate = Enums.FeastDay.Passover.Date.AddDays(2);

		int i;
		for (i = 1; i < 50; i++)
		{
			dataList!.Add(new ReadonlyEventsData
			{
				Id = i + runningCount,
				Subject = $"Omer {i} {GetHebrew(i)}",
				Description = "Omer Count, Day " + i,
				StartTime = startDate.AddDays(i - 1),
				EndTime = startDate.AddDays(i - 1),
				CategoryColor = CalendarColors.Info,
				IsAllDay = true,
				IsReadonly = true
			}
			);
		}
		return (runningCount + i, dataList);

	}

	private static (int RunningCount, List<ReadonlyEventsData> DataList)
		LoadHanukkahDates(int runningCount, List<ReadonlyEventsData> dataList)
	{
		DateTime startDate = Calendar.Enums.FeastDay.Hanukkah.Date.AddDays(0);
		string candle = "🕯️";

		int i;
		for (i = 0; i < 8; i++)
		{
			dataList!.Add(new ReadonlyEventsData
			{
				Id = i + runningCount,
				Subject = $"Hanukkah {candle.Repeat(i+1)}",
				Description = "8 Days of Hanukkah; dates determined by Rabbinic sources",
				StartTime = startDate.AddDays(i),
				EndTime = startDate.AddDays(i),
				CategoryColor = Enums.DateType.Feast.CalendarColor,
				IsAllDay = true,
				IsReadonly = true
			}
			);
		}
		return (runningCount + i, dataList);
	}
	
	private static (int RunningCount, List<ReadonlyEventsData> DataList)
		LoadMonths(int runningCount, List<ReadonlyEventsData> dataList)
	{
		string moon = "🌙";

		int i = 0;
		foreach (var month in Enums.LunarMonth.List.ToList())
		{
			i += 1;
			dataList!.Add(new ReadonlyEventsData
			{
				Id = i + runningCount,
				Subject = $"{moon} {month.FullName}",
				Description = month.Description,
				StartTime = month.Date,
				EndTime = month.Date,
				CategoryColor = Enums.DateType.Month.CalendarColor,
				IsAllDay = true,
				IsReadonly = true
			}
			);
		}
		return (runningCount + i, dataList);
	}

	private static (int RunningCount, List<ReadonlyEventsData> DataList)
		LoadSeasons(int runningCount, List<ReadonlyEventsData> dataList)
	{
		int i = 0;
		foreach (var season in Enums.Season.List.ToList())
		{
			i += 1;
			dataList!.Add(new ReadonlyEventsData
			{
				Id = i + runningCount,
				Subject = $"{season.Emoji} {season.Name}",
				Description = $"{season.Name} | {season.Type}",
				StartTime = season.Date,
				EndTime = season.Date,
				CategoryColor = season.CalendarColor,
				IsAllDay = true,
				IsReadonly = true
			}
			);
		}
		return (runningCount + i, dataList);
	}

}
