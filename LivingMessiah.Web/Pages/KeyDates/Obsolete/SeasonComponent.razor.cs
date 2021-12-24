using System;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.KeyDates.Queries;

namespace LivingMessiah.Web.Pages.KeyDates.Obsolete
{
	public partial class SeasonComponent
	{
		[Parameter]
		public DateTime Date { get; set; }

		[Parameter]
		public Season Season { get; set; }
	}
}
