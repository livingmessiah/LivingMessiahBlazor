using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Services
{
	/*
			Min-Max Date Range: 10/20/2021 - 10/28/2021
		Start-End Date Range: 10/21/2021 - 10/27/2021

												 1 2 3 4  5  6  7   8   9
												 - - - - -- -- -- --- ---
			LodgingDateMinMax: 1,2,4,8,16,32,64,128,256
		LodgingDateStartEnd:   2,4,8,16,32,64,128   => Bitwise: 254
								iMinVal:   2
								iMaxVal:                  128
								iMinIdx:	 2
								iMaxIdx:                    8

		Date Range::					10/21/2021 - 10/27/2021
	*/
	public static class BitwiseHelper
	{
		public static DateTime LodgingMinDate = new DateTime(2021, 10, 20);
		public static DateTime LodgingMaxDate = new DateTime(2021, 10, 28);
		public static DateTime AttendanceMinDate = new DateTime(2021, 10, 20);
		public static DateTime AttendanceMaxDate = new DateTime(2021, 10, 28);

		public static int GetDaysBitwise(DateTime? start, DateTime? end)
		{
			/*
			log.LogDebug($"Min-Max Date Range: {LodgingMinDate.ToString("MM/dd/yyyy")} - {LodgingMaxDate.ToString("MM/dd/yyyy")}");
			log.LogDebug($"Start-End Date Range: {LodgingStartDate?.ToString("MM/dd/yyyy")} - {LodgingEndDate?.ToString("MM/dd/yyyy")}");
			*/

			List<int> LodgingDateMinMax = GetDateRange(LodgingMinDate, LodgingMaxDate, 0);
			//log.LogDebug($"LodgingDateMinMax: {DumpGetDateRange(LodgingDateMinMax)}");

			int BeginCount = ((DateTime)start - LodgingMinDate).Days;
			List<int> LodgingDateStartEnd = GetDateRange((DateTime)start,	(DateTime)end,	BeginCount);
			//log.LogDebug($"LodgingDateStartEnd: {DumpGetDateRange(LodgingDateStartEnd)} {Environment.NewLine} Bitwise: {LodgingDateStartEnd.Sum()}");
			return LodgingDateStartEnd.Sum();
		}

		public static string GetLodgingDaysDates(DateTime? start, DateTime? end)
		{
			int BeginCount = ((DateTime)start - LodgingMinDate).Days;
			List<int> LodgingDateStartEnd = GetDateRange((DateTime)start, (DateTime)end, BeginCount);
			List<int> LodgingDateMinMax = GetDateRange(LodgingMinDate, LodgingMaxDate, 0);

			int iMinVal = LodgingDateStartEnd.Min(x => x);
			int iMaxVal = LodgingDateStartEnd.Max(x => x);
			//log.LogDebug($"iMinVal:{iMinVal}; iMaxVal:{iMaxVal}");

			int iMinIdx = 0;
			int iMaxIdx = 0;

			int j = 0;
			foreach (int i in LodgingDateMinMax)
			{
				j += 1;

				if (i == iMinVal)
				{
					iMinIdx = j;
				}
				if (i == iMaxVal)
				{
					iMaxIdx = j;
				}
			}

			/*
			log.LogDebug($"iMinIdx:{iMinIdx}; iMaxIdx:{iMaxIdx}");

			log.LogDebug($"Date Range:" +
				$" {LodgingMinDate.AddDays(iMinIdx - 1).ToString("MM/dd/yyyy")} -" +
				$" {LodgingMinDate.AddDays(iMaxIdx - 1).ToString("MM/dd/yyyy")}");
			*/
			return $" {LodgingMinDate.AddDays(iMinIdx - 1):MM/dd/yyyy} - {LodgingMinDate.AddDays(iMaxIdx - 1):MM/dd/yyyy}";
		}

		public static Tuple<DateTime?, DateTime?> HydrateDatesFromBitwise(int bitWise, DateTime minDate, DateTime maxDate)
		{
			if (bitWise == 0) { return new Tuple<DateTime?, DateTime?>(null, null); }

			List<int> lintMinMax = GetDateRange(minDate, maxDate, 0);

			IEnumerable<DateTime> ldatMinMax = new List<DateTime>();
			ldatMinMax = EachDay(minDate, maxDate);

			DateTime? start = null;
			DateTime? end = null;

			int d = 0;
			int p = 0;

			//log.LogDebug($"{Environment.NewLine}{Environment.NewLine}Inside {nameof(BitwiseHelper)}!{nameof(HydrateDatesFromBitwise)} beginning foreach...");
			foreach (DateTime item in ldatMinMax)
			{
				p = ((int)Math.Pow((double)2, (double)d));
				//log.LogDebug($"...d: {d}, p: {p}, Date: {item.ToString("MM/dd/yyyy")} {((p & bitWise) > 0 ? "*" : "")}");

				if ((p & bitWise) > 0)
				{
					if (start == null)
					{
						start = item;
					}
					end = item;
				}

				d += 1;
			}
			return new Tuple<DateTime?, DateTime?>(start, end);
		}

		public static DateTime[] GetDateArray(DateTime? start, DateTime? end)
		{
			if (start == null | end == null) { return null; }
			int length = ((DateTime)start - (DateTime)end).Days;
			DateTime[] list = new DateTime[length];

			int i = 0;
			foreach (DateTime day in EachDay((DateTime)start, (DateTime)end))
			{
				list[i] = day;
				i += 1;
			}
			return list;

		}

		//ToDo Delete?
		public static List<DateTime> GetDateList(DateTime? Start, DateTime? End)
		{
			if (Start == null | End == null) { return null;	}
			List<DateTime> dateList = new List<DateTime>();
			foreach (DateTime day in EachDay((DateTime)Start, (DateTime)End))
			{
				dateList.Add(day);
			}
			return dateList;
		}


		private static List<int> GetDateRange(DateTime Start, DateTime End, int beginCount = 0)
		{
			List<int> lBitwise = new List<int>();
			int i = beginCount;
			foreach (DateTime day in EachDay((DateTime)Start, (DateTime)End))
			{
				lBitwise.Add((int)Math.Pow((double)2, (double)i));
				i += 1;
			}
			return lBitwise;
		}

		private static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
		{
			for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
				yield return day;
		}

		public static string DumpGetDateRange(List<int> list)
		{
			string sBitwiseConcatenated = "";

			foreach (int i in list)
			{
				sBitwiseConcatenated += i + ",";
			}
			sBitwiseConcatenated = sBitwiseConcatenated.TrimEnd(',');

			return sBitwiseConcatenated;
		}

	}
}
