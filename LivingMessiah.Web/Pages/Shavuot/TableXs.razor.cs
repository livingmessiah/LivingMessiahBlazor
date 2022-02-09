using Microsoft.AspNetCore.Components;
using static LivingMessiah.Web.Pages.Shavuot.Domain.OmerGematriaFactory;

namespace LivingMessiah.Web.Pages.Shavuot;

public partial class TableXs
{
		[Parameter]
		public int CurrentDay { get; set; }

		protected int dayXs = 0;

		protected string FaCheck(int day)
		{
				if (day <= CurrentDay)
				{
						return "<span class='text-warning'><i class='fas fa-check'></i></span>";
				}
				else
				{
						return "";
				}
		}

		protected string DetailXs(int day)
		{
				if (day == CurrentDay)
				{
						return $"<span class=''><span class='text-danger hebrew16'><b>{GetHebrew(day)}</b></span></span>";
				}
				else
				{
						return $"<span class=''><span class='text-white hebrew16'>{GetHebrew(day)}</span></span>";
				}
		}
}
