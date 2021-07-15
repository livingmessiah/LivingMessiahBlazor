using Microsoft.AspNetCore.Components;
using LivingMessiah.Domain.KeyDates.Queries;
using System.Collections.Generic;

namespace LivingMessiah.Web.Pages.KeyDate
{
	public partial class LunarMonthComponent
	{
		[Parameter]
		public int DateIdBeg { get; set; }

		[Parameter]
		public int DateIdEnd { get; set; }

		[Parameter]
		public bool IsXsOrSm { get; set; }

		[Parameter] 
		public List<LunarMonth> LunarMonths { get; set; }

		protected string ml4 = "";
		protected string pb0 = "";

	}
}


