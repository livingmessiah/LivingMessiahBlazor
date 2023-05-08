using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Infrastructure;
using System;

namespace LivingMessiah.Web.Shared;

public partial class ShabbatMeter 
{
	[Parameter]	public bool IsPrinterFriendly { get; set; }
	[Parameter]	public bool IsMini { get; set; }
	[Parameter]	public string? Format { get; set; } // "MMMM dd" // "MMMM dd yyyy"
	[Parameter]	public int AdjustDaysOverride { get; set; }

	protected bool _isPrinterFriendly;
	protected bool _isMini;

	protected int DowInt;
	protected int DowWidthPerc;
	protected string? NextShabbat;

	protected override void OnInitialized()
	{
		DateTime nextShabbat = DateUtil.GetNextWeekday(DateTime.Today, DayOfWeek.Saturday);
		NextShabbat = nextShabbat.ToString(Format);
		_isPrinterFriendly = IsPrinterFriendly;
		_isMini = IsMini;
		Calculate();
	}

	private void Calculate()
	{
		int DayOfWeekAdjust = (AdjustDaysOverride == 0) ? 1 : AdjustDaysOverride;
		DowInt = (int)DateTime.Today.DayOfWeek + DayOfWeekAdjust;  // + 1;

		int dowWidth;
		dowWidth = DowInt * 100;
		DowWidthPerc = dowWidth / 7;
	}
}



