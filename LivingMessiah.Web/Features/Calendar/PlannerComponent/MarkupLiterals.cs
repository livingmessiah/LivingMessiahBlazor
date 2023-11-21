using Microsoft.AspNetCore.Components;
using System;

namespace LivingMessiah.Web.Features.Calendar.PlannerComponent;

public class MarkupLiterals
{
	//const string DateCSS = IsXsOrSm ? "h6" : "h4";

	public static MarkupString FeastDayCalendarDetails(DateTime date, Enums.FeastDayDetail enumValue) 
	{
		string s = $@"
<div class='col-1'>&nbsp;</div>
<div class='col-4'>{GetFeastDetailDate(date, Enums.FeastDayDetail.FromValue(enumValue).AddDays)}</div>			
<div class='col-7'>{Enums.FeastDayDetail.FromValue(enumValue).Description}</div>
";
		return (MarkupString)(s);
	}

	private static string GetFeastDetailDate(DateTime date, int addedDays) 
	{
		double d = (double)addedDays;
		return date.AddDays(d).ToString(DateFormat.ddd_mm_dd);
	}

	public static MarkupString FormatedDate(bool IsXsOrSm, DateTime date)
	{
		const string DateFormat = "ddd, MMMM dd, yyyy"; //DateFormat = IsXsOrSm ? "yyyy/MM/dd" : "ddd, MMMM dd, yyyy";
		return IsXsOrSm ?
			(MarkupString)$"<span class='h6'>{date.ToString(DateFormat)}</span>" :
			(MarkupString)$"<span class='h4'>{date.ToString(DateFormat)}</span>";
	}

	public static MarkupString HeaderSeason(DateTime date, Enums.Season enumValue)
	{
		string s = $@"
	<span class='h5'>
		<span class='badge {Enums.Season.FromValue(enumValue).BadgeColor}'>
			<i class='{Enums.Season.FromValue(enumValue).Icon}'></i>
			<b>{Enums.Season.FromValue(enumValue).Name} {Enums.Season.FromValue(enumValue).Type}</b>
		</span>
	</span>
";
		return (MarkupString)s;
	}


	public static MarkupString HeaderOther(DateTime date, Enums.DateType enumValue, string description)
	{
		string s = $@"
	<span class='h5'>
		<span class='badge {Enums.DateType.FromValue(enumValue).BadgeColor}'>
			<i class='{Enums.DateType.FromValue(enumValue).Icon}'></i>
			<b>{description}</b>
		</span>
	</span>
";
		return (MarkupString)s;
	}

}
