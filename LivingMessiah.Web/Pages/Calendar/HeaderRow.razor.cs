using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Calendar;

public partial class HeaderRow
{
	[Parameter] public int DateTypeId { get; set; }
	[Parameter] public int Detail { get; set; }
	[Parameter] public string Descr { get; set; }

	public  string Badge()
	{
		if (DateTypeId == Enums.DateTypeHeaderRow.SeasonWinter.Value)
		{
			return $"badge {Enums.Season.FromValue(Detail).BadgeColor}";
		}
		else
		{
			return $"badge {Enums.DateTypeHeaderRow.FromValue((int)DateTypeId).BadgeColor}";
		}

	}

	public  string Icon()
	{
		if (DateTypeId == Enums.DateTypeHeaderRow.SeasonWinter.Value)
		{
			return $"{Enums.Season.FromValue(Detail).Icon}";
		}
		else
		{
			return $"{Enums.DateTypeHeaderRow.FromValue((int)DateTypeId).Icon}";
		}
	}

	public  string Description()
	{
		if (DateTypeId == Enums.DateTypeHeaderRow.SeasonWinter.Value)
		{
			return $"{Enums.Season.FromValue(Detail).Name} {Enums.Season.FromValue(Detail).Type}";
		}
		else
		{
			return Descr;
		}
	}

}
