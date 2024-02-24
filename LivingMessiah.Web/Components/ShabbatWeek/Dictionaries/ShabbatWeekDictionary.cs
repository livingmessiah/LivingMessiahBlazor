using System;
using System.Collections.Generic;

using LivingMessiah.Web.Infrastructure;

namespace LivingMessiah.Web.Components.ShabbatWeek.Dictionaries;

/*

Gotten by `dbo.tvfShabbatWeekDateCodeGen`

<p>GetCurrentShabbatDateUTC: @GetCurrentShabbatDateUTC()</p>
<p>GetCurrentShabbatDateUTC DateOnly: @GetCurrentShabbatDateOnlyUTC()</p>
<p>GetShabbatId via Date: @GetShabbatId2(GetCurrentShabbatDateOnlyUTC())</p> 

 */

public static class ShabbatWeekDictionary
{
	public static DateTime? GetCurrentShabbatDateUTC()
	{
		DateTime today = DateTime.UtcNow.AddHours(Utc.ArizonaUtcMinus7);
		DateTime nextSaturday = today.AddDays(((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7);
		return nextSaturday;
	}


	public static DateOnly? GetCurrentShabbatDateOnlyUTC()
	{
		DateTime today = DateTime.UtcNow.AddHours(Utc.ArizonaUtcMinus7);
		DateTime nextSaturday = today.AddDays(((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7);
		return DateUtil.ToNullableDateOnly(nextSaturday);
	}

	public static int? GetShabbatId2(DateOnly? dateOnly)
	{
	if (dateOnly == null) { return null; }

		bool found;
		found = _dictionaryByDate.TryGetValue((DateOnly)dateOnly!, out int id);
		if (found)
		{
			return id;
		}
		else
		{
			return null;
		}
	}


	public static int? GetShabbatId(DateOnly dateOnly)
	{
		bool found;
		found = _dictionaryByDate.TryGetValue(dateOnly, out int id);
		if (found)
		{
			return id;
		}
		else
		{
			return null;
		}
	}
	
	private static readonly Dictionary<DateOnly, int> _dictionaryByDate = new()
	{
		{DateOnly.FromDateTime(Convert.ToDateTime("2021-10-02")), 1 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2021-10-09")), 2 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2021-10-16")), 3 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2021-10-23")), 4 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2021-10-30")), 5 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2021-11-06")), 6 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2021-11-13")), 7 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2021-11-20")), 8 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2021-11-27")), 9 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2021-12-04")), 10 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2021-12-11")), 11 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2021-12-18")), 12 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2021-12-25")), 13 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-01-01")), 14 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-01-08")), 15 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-01-15")), 16 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-01-22")), 17 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-01-29")), 18 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-02-05")), 19 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-02-12")), 20 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-02-19")), 21 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-02-26")), 22 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-03-05")), 23 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-03-12")), 24 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-03-19")), 25 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-03-26")), 26 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-04-02")), 27 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-04-09")), 28 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-04-16")), 29 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-04-23")), 30 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-04-30")), 31 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-05-07")), 32 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-05-14")), 33 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-05-21")), 34 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-05-28")), 35 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-06-04")), 36 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-06-11")), 37 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-06-18")), 38 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-06-25")), 39 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-07-02")), 40 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-07-09")), 41 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-07-16")), 42 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-07-23")), 43 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-07-30")), 44 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-08-06")), 45 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-08-13")), 46 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-08-20")), 47 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-08-27")), 48 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-09-03")), 49 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-09-10")), 50 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-09-17")), 51 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-09-24")), 52 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-10-01")), 53 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-10-08")), 54 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-10-15")), 55 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-10-22")), 56 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-10-29")), 57 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-11-05")), 58 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-11-12")), 59 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-11-19")), 60 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-11-26")), 61 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-12-03")), 62 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-12-10")), 63 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-12-17")), 64 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-12-24")), 65 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2022-12-31")), 66 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-01-07")), 67 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-01-14")), 68 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-01-21")), 69 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-01-28")), 70 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-02-04")), 71 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-02-11")), 72 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-02-18")), 73 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-02-25")), 74 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-03-04")), 75 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-03-11")), 76 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-03-18")), 77 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-03-25")), 78 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-04-01")), 79 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-04-08")), 80 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-04-15")), 81 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-04-22")), 82 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-04-29")), 83 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-05-06")), 84 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-05-13")), 85 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-05-20")), 86 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-05-27")), 87 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-06-03")), 88 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-06-10")), 89 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-06-17")), 90 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-06-24")), 91 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-07-01")), 92 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-07-08")), 93 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-07-15")), 94 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-07-22")), 95 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-07-29")), 96 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-08-05")), 97 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-08-12")), 98 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-08-19")), 99 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-08-26")), 100 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-09-02")), 101 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-09-09")), 102 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-09-16")), 103 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-09-23")), 104 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-09-30")), 105 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-10-07")), 106 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-10-14")), 107 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-10-21")), 108 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-10-28")), 109 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-11-04")), 110 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-11-11")), 111 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-11-18")), 112 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-11-25")), 113 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-12-02")), 114 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-12-09")), 115 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-12-16")), 116 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-12-23")), 117 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2023-12-30")), 118 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-01-06")), 119 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-01-13")), 120 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-01-20")), 121 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-01-27")), 122 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-02-03")), 123 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-02-10")), 124 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-02-17")), 125 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-02-24")), 126 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-03-02")), 127 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-03-09")), 128 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-03-16")), 129 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-03-23")), 130 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-03-30")), 131 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-04-06")), 132 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-04-13")), 133 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-04-20")), 134 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-04-27")), 135 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-05-04")), 136 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-05-11")), 137 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-05-18")), 138 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-05-25")), 139 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-06-01")), 140 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-06-08")), 141 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-06-15")), 142 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-06-22")), 143 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-06-29")), 144 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-07-06")), 145 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-07-13")), 146 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-07-20")), 147 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-07-27")), 148 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-08-03")), 149 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-08-10")), 150 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-08-17")), 151 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-08-24")), 152 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-08-31")), 153 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-09-07")), 154 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-09-14")), 155 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-09-21")), 156 },
		{DateOnly.FromDateTime(Convert.ToDateTime("2024-09-28")), 157 },
	};



	public static DateOnly? GetShabbatDate(int id)
	{
		bool found;
		found = _dictionaryById.TryGetValue(id, out DateOnly shabbatDate);
		if (found)
		{
			return shabbatDate;
		}
		else
		{
			return null;
		}
	}


	private static readonly Dictionary<int, DateOnly> _dictionaryById = new()
	{
		{1, DateOnly.FromDateTime(Convert.ToDateTime("2021-10-02")) },
		{2, DateOnly.FromDateTime(Convert.ToDateTime("2021-10-09")) },
		{3, DateOnly.FromDateTime(Convert.ToDateTime("2021-10-16")) },
		{4, DateOnly.FromDateTime(Convert.ToDateTime("2021-10-23")) },
		{5, DateOnly.FromDateTime(Convert.ToDateTime("2021-10-30")) },
		{6, DateOnly.FromDateTime(Convert.ToDateTime("2021-11-06")) },
		{7, DateOnly.FromDateTime(Convert.ToDateTime("2021-11-13")) },
		{8, DateOnly.FromDateTime(Convert.ToDateTime("2021-11-20")) },
		{9, DateOnly.FromDateTime(Convert.ToDateTime("2021-11-27")) },
		{10, DateOnly.FromDateTime(Convert.ToDateTime("2021-12-04")) },
		{11, DateOnly.FromDateTime(Convert.ToDateTime("2021-12-11")) },
		{12, DateOnly.FromDateTime(Convert.ToDateTime("2021-12-18")) },
		{13, DateOnly.FromDateTime(Convert.ToDateTime("2021-12-25")) },
		{14, DateOnly.FromDateTime(Convert.ToDateTime("2022-01-01")) },
		{15, DateOnly.FromDateTime(Convert.ToDateTime("2022-01-08")) },
		{16, DateOnly.FromDateTime(Convert.ToDateTime("2022-01-15")) },
		{17, DateOnly.FromDateTime(Convert.ToDateTime("2022-01-22")) },
		{18, DateOnly.FromDateTime(Convert.ToDateTime("2022-01-29")) },
		{19, DateOnly.FromDateTime(Convert.ToDateTime("2022-02-05")) },
		{20, DateOnly.FromDateTime(Convert.ToDateTime("2022-02-12")) },
		{21, DateOnly.FromDateTime(Convert.ToDateTime("2022-02-19")) },
		{22, DateOnly.FromDateTime(Convert.ToDateTime("2022-02-26")) },
		{23, DateOnly.FromDateTime(Convert.ToDateTime("2022-03-05")) },
		{24, DateOnly.FromDateTime(Convert.ToDateTime("2022-03-12")) },
		{25, DateOnly.FromDateTime(Convert.ToDateTime("2022-03-19")) },
		{26, DateOnly.FromDateTime(Convert.ToDateTime("2022-03-26")) },
		{27, DateOnly.FromDateTime(Convert.ToDateTime("2022-04-02")) },
		{28, DateOnly.FromDateTime(Convert.ToDateTime("2022-04-09")) },
		{29, DateOnly.FromDateTime(Convert.ToDateTime("2022-04-16")) },
		{30, DateOnly.FromDateTime(Convert.ToDateTime("2022-04-23")) },
		{31, DateOnly.FromDateTime(Convert.ToDateTime("2022-04-30")) },
		{32, DateOnly.FromDateTime(Convert.ToDateTime("2022-05-07")) },
		{33, DateOnly.FromDateTime(Convert.ToDateTime("2022-05-14")) },
		{34, DateOnly.FromDateTime(Convert.ToDateTime("2022-05-21")) },
		{35, DateOnly.FromDateTime(Convert.ToDateTime("2022-05-28")) },
		{36, DateOnly.FromDateTime(Convert.ToDateTime("2022-06-04")) },
		{37, DateOnly.FromDateTime(Convert.ToDateTime("2022-06-11")) },
		{38, DateOnly.FromDateTime(Convert.ToDateTime("2022-06-18")) },
		{39, DateOnly.FromDateTime(Convert.ToDateTime("2022-06-25")) },
		{40, DateOnly.FromDateTime(Convert.ToDateTime("2022-07-02")) },
		{41, DateOnly.FromDateTime(Convert.ToDateTime("2022-07-09")) },
		{42, DateOnly.FromDateTime(Convert.ToDateTime("2022-07-16")) },
		{43, DateOnly.FromDateTime(Convert.ToDateTime("2022-07-23")) },
		{44, DateOnly.FromDateTime(Convert.ToDateTime("2022-07-30")) },
		{45, DateOnly.FromDateTime(Convert.ToDateTime("2022-08-06")) },
		{46, DateOnly.FromDateTime(Convert.ToDateTime("2022-08-13")) },
		{47, DateOnly.FromDateTime(Convert.ToDateTime("2022-08-20")) },
		{48, DateOnly.FromDateTime(Convert.ToDateTime("2022-08-27")) },
		{49, DateOnly.FromDateTime(Convert.ToDateTime("2022-09-03")) },
		{50, DateOnly.FromDateTime(Convert.ToDateTime("2022-09-10")) },
		{51, DateOnly.FromDateTime(Convert.ToDateTime("2022-09-17")) },
		{52, DateOnly.FromDateTime(Convert.ToDateTime("2022-09-24")) },
		{53, DateOnly.FromDateTime(Convert.ToDateTime("2022-10-01")) },
		{54, DateOnly.FromDateTime(Convert.ToDateTime("2022-10-08")) },
		{55, DateOnly.FromDateTime(Convert.ToDateTime("2022-10-15")) },
		{56, DateOnly.FromDateTime(Convert.ToDateTime("2022-10-22")) },
		{57, DateOnly.FromDateTime(Convert.ToDateTime("2022-10-29")) },
		{58, DateOnly.FromDateTime(Convert.ToDateTime("2022-11-05")) },
		{59, DateOnly.FromDateTime(Convert.ToDateTime("2022-11-12")) },
		{60, DateOnly.FromDateTime(Convert.ToDateTime("2022-11-19")) },
		{61, DateOnly.FromDateTime(Convert.ToDateTime("2022-11-26")) },
		{62, DateOnly.FromDateTime(Convert.ToDateTime("2022-12-03")) },
		{63, DateOnly.FromDateTime(Convert.ToDateTime("2022-12-10")) },
		{64, DateOnly.FromDateTime(Convert.ToDateTime("2022-12-17")) },
		{65, DateOnly.FromDateTime(Convert.ToDateTime("2022-12-24")) },
		{66, DateOnly.FromDateTime(Convert.ToDateTime("2022-12-31")) },
		{67, DateOnly.FromDateTime(Convert.ToDateTime("2023-01-07")) },
		{68, DateOnly.FromDateTime(Convert.ToDateTime("2023-01-14")) },
		{69, DateOnly.FromDateTime(Convert.ToDateTime("2023-01-21")) },
		{70, DateOnly.FromDateTime(Convert.ToDateTime("2023-01-28")) },
		{71, DateOnly.FromDateTime(Convert.ToDateTime("2023-02-04")) },
		{72, DateOnly.FromDateTime(Convert.ToDateTime("2023-02-11")) },
		{73, DateOnly.FromDateTime(Convert.ToDateTime("2023-02-18")) },
		{74, DateOnly.FromDateTime(Convert.ToDateTime("2023-02-25")) },
		{75, DateOnly.FromDateTime(Convert.ToDateTime("2023-03-04")) },
		{76, DateOnly.FromDateTime(Convert.ToDateTime("2023-03-11")) },
		{77, DateOnly.FromDateTime(Convert.ToDateTime("2023-03-18")) },
		{78, DateOnly.FromDateTime(Convert.ToDateTime("2023-03-25")) },
		{79, DateOnly.FromDateTime(Convert.ToDateTime("2023-04-01")) },
		{80, DateOnly.FromDateTime(Convert.ToDateTime("2023-04-08")) },
		{81, DateOnly.FromDateTime(Convert.ToDateTime("2023-04-15")) },
		{82, DateOnly.FromDateTime(Convert.ToDateTime("2023-04-22")) },
		{83, DateOnly.FromDateTime(Convert.ToDateTime("2023-04-29")) },
		{84, DateOnly.FromDateTime(Convert.ToDateTime("2023-05-06")) },
		{85, DateOnly.FromDateTime(Convert.ToDateTime("2023-05-13")) },
		{86, DateOnly.FromDateTime(Convert.ToDateTime("2023-05-20")) },
		{87, DateOnly.FromDateTime(Convert.ToDateTime("2023-05-27")) },
		{88, DateOnly.FromDateTime(Convert.ToDateTime("2023-06-03")) },
		{89, DateOnly.FromDateTime(Convert.ToDateTime("2023-06-10")) },
		{90, DateOnly.FromDateTime(Convert.ToDateTime("2023-06-17")) },
		{91, DateOnly.FromDateTime(Convert.ToDateTime("2023-06-24")) },
		{92, DateOnly.FromDateTime(Convert.ToDateTime("2023-07-01")) },
		{93, DateOnly.FromDateTime(Convert.ToDateTime("2023-07-08")) },
		{94, DateOnly.FromDateTime(Convert.ToDateTime("2023-07-15")) },
		{95, DateOnly.FromDateTime(Convert.ToDateTime("2023-07-22")) },
		{96, DateOnly.FromDateTime(Convert.ToDateTime("2023-07-29")) },
		{97, DateOnly.FromDateTime(Convert.ToDateTime("2023-08-05")) },
		{98, DateOnly.FromDateTime(Convert.ToDateTime("2023-08-12")) },
		{99, DateOnly.FromDateTime(Convert.ToDateTime("2023-08-19")) },
		{100, DateOnly.FromDateTime(Convert.ToDateTime("2023-08-26")) },
		{101, DateOnly.FromDateTime(Convert.ToDateTime("2023-09-02")) },
		{102, DateOnly.FromDateTime(Convert.ToDateTime("2023-09-09")) },
		{103, DateOnly.FromDateTime(Convert.ToDateTime("2023-09-16")) },
		{104, DateOnly.FromDateTime(Convert.ToDateTime("2023-09-23")) },
		{105, DateOnly.FromDateTime(Convert.ToDateTime("2023-09-30")) },
		{106, DateOnly.FromDateTime(Convert.ToDateTime("2023-10-07")) },
		{107, DateOnly.FromDateTime(Convert.ToDateTime("2023-10-14")) },
		{108, DateOnly.FromDateTime(Convert.ToDateTime("2023-10-21")) },
		{109, DateOnly.FromDateTime(Convert.ToDateTime("2023-10-28")) },
		{110, DateOnly.FromDateTime(Convert.ToDateTime("2023-11-04")) },
		{111, DateOnly.FromDateTime(Convert.ToDateTime("2023-11-11")) },
		{112, DateOnly.FromDateTime(Convert.ToDateTime("2023-11-18")) },
		{113, DateOnly.FromDateTime(Convert.ToDateTime("2023-11-25")) },
		{114, DateOnly.FromDateTime(Convert.ToDateTime("2023-12-02")) },
		{115, DateOnly.FromDateTime(Convert.ToDateTime("2023-12-09")) },
		{116, DateOnly.FromDateTime(Convert.ToDateTime("2023-12-16")) },
		{117, DateOnly.FromDateTime(Convert.ToDateTime("2023-12-23")) },
		{118, DateOnly.FromDateTime(Convert.ToDateTime("2023-12-30")) },
		{119, DateOnly.FromDateTime(Convert.ToDateTime("2024-01-06")) },
		{120, DateOnly.FromDateTime(Convert.ToDateTime("2024-01-13")) },
		{121, DateOnly.FromDateTime(Convert.ToDateTime("2024-01-20")) },
		{122, DateOnly.FromDateTime(Convert.ToDateTime("2024-01-27")) },
		{123, DateOnly.FromDateTime(Convert.ToDateTime("2024-02-03")) },
		{124, DateOnly.FromDateTime(Convert.ToDateTime("2024-02-10")) },
		{125, DateOnly.FromDateTime(Convert.ToDateTime("2024-02-17")) },
		{126, DateOnly.FromDateTime(Convert.ToDateTime("2024-02-24")) },
		{127, DateOnly.FromDateTime(Convert.ToDateTime("2024-03-02")) },
		{128, DateOnly.FromDateTime(Convert.ToDateTime("2024-03-09")) },
		{129, DateOnly.FromDateTime(Convert.ToDateTime("2024-03-16")) },
		{130, DateOnly.FromDateTime(Convert.ToDateTime("2024-03-23")) },
		{131, DateOnly.FromDateTime(Convert.ToDateTime("2024-03-30")) },
		{132, DateOnly.FromDateTime(Convert.ToDateTime("2024-04-06")) },
		{133, DateOnly.FromDateTime(Convert.ToDateTime("2024-04-13")) },
		{134, DateOnly.FromDateTime(Convert.ToDateTime("2024-04-20")) },
		{135, DateOnly.FromDateTime(Convert.ToDateTime("2024-04-27")) },
		{136, DateOnly.FromDateTime(Convert.ToDateTime("2024-05-04")) },
		{137, DateOnly.FromDateTime(Convert.ToDateTime("2024-05-11")) },
		{138, DateOnly.FromDateTime(Convert.ToDateTime("2024-05-18")) },
		{139, DateOnly.FromDateTime(Convert.ToDateTime("2024-05-25")) },
		{140, DateOnly.FromDateTime(Convert.ToDateTime("2024-06-01")) },
		{141, DateOnly.FromDateTime(Convert.ToDateTime("2024-06-08")) },
		{142, DateOnly.FromDateTime(Convert.ToDateTime("2024-06-15")) },
		{143, DateOnly.FromDateTime(Convert.ToDateTime("2024-06-22")) },
		{144, DateOnly.FromDateTime(Convert.ToDateTime("2024-06-29")) },
		{145, DateOnly.FromDateTime(Convert.ToDateTime("2024-07-06")) },
		{146, DateOnly.FromDateTime(Convert.ToDateTime("2024-07-13")) },
		{147, DateOnly.FromDateTime(Convert.ToDateTime("2024-07-20")) },
		{148, DateOnly.FromDateTime(Convert.ToDateTime("2024-07-27")) },
		{149, DateOnly.FromDateTime(Convert.ToDateTime("2024-08-03")) },
		{150, DateOnly.FromDateTime(Convert.ToDateTime("2024-08-10")) },
		{151, DateOnly.FromDateTime(Convert.ToDateTime("2024-08-17")) },
		{152, DateOnly.FromDateTime(Convert.ToDateTime("2024-08-24")) },
		{153, DateOnly.FromDateTime(Convert.ToDateTime("2024-08-31")) },
		{154, DateOnly.FromDateTime(Convert.ToDateTime("2024-09-07")) },
		{155, DateOnly.FromDateTime(Convert.ToDateTime("2024-09-14")) },
		{156, DateOnly.FromDateTime(Convert.ToDateTime("2024-09-21")) },
		{157, DateOnly.FromDateTime(Convert.ToDateTime("2024-09-28")) },    
	};

}

/*

Gotten by `dbo.tvfShabbatWeekDateCodeGen`

<p>GetCurrentShabbatDate: @GetCurrentShabbatDate()</p>
<p>GetCurrentShabbatDateUTC: @GetCurrentShabbatDateUTC()</p>
<p>GetCurrentShabbatDateUTC DateOnly: @GetCurrentShabbatDateOnlyUTC()</p>
<p>GetShabbatId via Date: @GetShabbatId2(GetCurrentShabbatDateOnlyUTC())</p> 

	public static DateOnly? GetCurrentShabbatDate()
	{
		DateTime nextSaturday = DateTime.Today.AddDays(((int)DayOfWeek.Saturday - (int)DateTime.Today.DayOfWeek + 7) % 7);
		return DateOnly.FromDateTime(nextSaturday);
	}

 */
