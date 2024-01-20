using LivingMessiah.Web.Infrastructure;
using FeastDayType = LivingMessiah.Web.Features.Calendar.Enums.FeastDay;
using LunarMonthType = LivingMessiah.Web.Features.Calendar.Enums.LunarMonth;

using System;

namespace LivingMessiah.Web.Features.FeastDayPlanner.Data;

public interface IService
{
	HeaderServiceModel GetHeaderServiceModel(FeastDayType feastDay);
	LunarMonths.ProgressBarVM GetHeaderServiceModelLunarMonth(LunarMonthType lunarMonth);

}

public class Service : IService
{
	public LunarMonths.ProgressBarVM GetHeaderServiceModelLunarMonth(LunarMonthType lunarMonth)
	{
		DateTime today = DateUtil.GetDateTimeWithoutTime
			(
				DateTime.Now.AddDays
				(
					Constants.Test.AddDays).AddHours(Utc.ArizonaUtcMinus7
				)
			);

		LunarMonths.ProgressBarVM model = new LunarMonths.ProgressBarVM();

		model.GregorianDate = today.ToString(DateFormat.FeastDayPlanner);
		model.HebrewDate = today.ToTransliteratedHebrewDateString();

		decimal avgDaysPerMonth = 29.5m;

		// .Where(w => w.Date >= dateTimeWithoutTime)
		if (today >= lunarMonth!.Date && today <= lunarMonth!.Date)
		{
			model.BadgeColor = "bg-success-subtle";
			model.SuffixDescription = $"day";
			model.DaysDifferent = (int)(today - lunarMonth!.Date).TotalDays;
			model.DaysDifferentFormat = $"{model.DaysDifferent}{DateUtil.GetDaySuffix(model.DaysDifferent)}";
			model.PercentUntilNewMoon = (int)Math.Round((model.DaysDifferent / avgDaysPerMonth)*100);
			model.DaysOld = 100-model.PercentUntilNewMoon;
		}
		else
		{
			if (today < lunarMonth!.Date)
			{
				model.BadgeColor = "bg-warning-subtle";
				model.SuffixDescription = "days ahead";
				model.DaysDifferent = (int)(lunarMonth!.Date - today).TotalDays;
				model.DaysDifferentFormat = model.DaysDifferent.ToString();
				model.PercentUntilNewMoon = (int)Math.Round((model.DaysDifferent / avgDaysPerMonth) * 100);
				model.DaysOld = 100 - model.PercentUntilNewMoon;
			}
			else
			{
				model.BadgeColor = "bg-danger-subtle";
				model.SuffixDescription = "days in the past";
				model.DaysDifferent = (int)(today - lunarMonth!.Date).TotalDays;
				model.DaysDifferentFormat = model.DaysDifferent.ToString();
				model.PercentUntilNewMoon = (int)Math.Round((model.DaysDifferent / avgDaysPerMonth) * 100);
				model.DaysOld = 100 - model.PercentUntilNewMoon;
			}
		}

		return model;
	}

	public HeaderServiceModel GetHeaderServiceModel(FeastDayType feastDay)
	{
		DateTime today = DateUtil.GetDateTimeWithoutTime
			(
				DateTime.Now.AddDays
				(
					Constants.Test.AddDays).AddHours(Utc.ArizonaUtcMinus7
				)
			);

		HeaderServiceModel model = new HeaderServiceModel();

		model.GregorianDate = today.ToString(DateFormat.FeastDayPlanner);
		model.HebrewDate = today.ToTransliteratedHebrewDateString();

		if (today >= feastDay!.Range.Min && today <= feastDay!.Range.Max)
		{
			model.BadgeColor = "bg-success-subtle";
			model.SuffixDescription = $"day";
			model.DaysDifferent = (int)(today - feastDay!.Range.Min).TotalDays;
			model.DaysDifferentFormat = $"{model.DaysDifferent}{DateUtil.GetDaySuffix(model.DaysDifferent)}";
		}
		else
		{
			if (today < feastDay!.Range.Min)
			{
				model.BadgeColor = "bg-warning-subtle";
				model.SuffixDescription = "days ahead";
				model.DaysDifferent = (int)(feastDay!.Range.Min - today).TotalDays;
				model.DaysDifferentFormat = model.DaysDifferent.ToString();
			}
			else
			{
				model.BadgeColor = "bg-danger-subtle";
				model.SuffixDescription = "days in the past";
				model.DaysDifferent = (int)(today - feastDay!.Range.Max).TotalDays;
				model.DaysDifferentFormat = model.DaysDifferent.ToString();
			}
		}

		return model;
	}
}
