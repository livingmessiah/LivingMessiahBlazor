using LivingMessiah.Web.Infrastructure;
using FeastDayType = LivingMessiah.Web.Features.Calendar.Enums.FeastDay;
using System;

namespace LivingMessiah.Web.Features.FeastDayPlanner.Data;

public interface IService
{
	HeaderServiceModel GetHeaderServiceModel(FeastDayType feastDay);
}

public class Service : IService
{
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
