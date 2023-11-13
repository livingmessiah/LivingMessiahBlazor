using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Features.Calendar;

public partial class HeaderRow
{
	[Parameter] public int DateTypeId { get; set; }
	[Parameter] public int EnumId { get; set; }
	[Parameter] public string? Descr { get; set; }

	public  string Badge()
	{
		if (DateTypeId == Enums.DateType.Season.Value)
		{
			return $"badge {Enums.Season.FromValue(EnumId).BadgeColor}";
		}
		else
		{
			return $"badge {Enums.DateType.FromValue((int)DateTypeId).BadgeColor}";
		}

	}

	public  string Icon()
	{
		if (DateTypeId == Enums.DateType.Season.Value)
		{
			return $"{Enums.Season.FromValue(EnumId).Icon}";
		}
		else
		{
			return $"{Enums.DateType.FromValue((int)DateTypeId).Icon}";
		}
	}

	public  string Description()
	{
		if (DateTypeId == Enums.DateType.Season.Value)
		{
			return $"{Enums.Season.FromValue(EnumId).Name} {Enums.Season.FromValue(EnumId).Type}";
		}
		else
		{
			return Descr!;
		}
	}

}

// Ignore Spelling: Descr