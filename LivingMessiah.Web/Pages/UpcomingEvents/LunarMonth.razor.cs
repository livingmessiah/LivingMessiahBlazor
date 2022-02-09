using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.KeyDates.Enums;

namespace LivingMessiah.Web.Pages.UpcomingEvents;

partial class LunarMonth : BaseKeyDates
{
		public const string Icon = "far fa-moon";

		[Parameter] public BaseLunarMonthSmartEnum BaseLunarMonthSmartEnum { get; set; }

}
