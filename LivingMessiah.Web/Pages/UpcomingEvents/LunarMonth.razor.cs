using Microsoft.AspNetCore.Components;
using LivingMessiah.Domain.KeyDates.Enums;

namespace LivingMessiah.Web.Pages.UpcomingEvents
{
	partial class LunarMonth : BaseKeyDates
	{
		[Parameter]
		public LunarMonthLocal LunarMonthLocal { get; set; }

	}
}
