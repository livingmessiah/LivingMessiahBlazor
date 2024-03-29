﻿using Microsoft.AspNetCore.Components;
using static LivingMessiah.Web.Features.Shavuot.Domain.OmerGematriaFactory;

namespace LivingMessiah.Web.Features.Shavuot;

public partial class GridSixWideSmMdLgXl
{
		[Parameter]
		public int CurrentDay { get; set; }

		protected int omerLaggingCnt = 0;
		protected int omerActualCnt = 0;

		protected string CalendarFont(int day)
		{
				if (day < CurrentDay)
				{
						return "<span class='text-warning'><i class='far fa-calendar-check'></i></span>";
				}
				else
				{
						return "<i class='far fa-calendar'></i>";
				}
		}

		private bool IsSundayOrMonday(int omerCnt)
		{
				if (omerCnt == 2 | omerCnt == 9 | omerCnt == 16 | omerCnt == 23 | omerCnt == 30 | omerCnt == 37 | omerCnt == 44)
				{
						return true;
				}
				else
				{
						return false;
				}
		}

		public string Content(int omerCnt)
		{
				string color = omerCnt == CurrentDay ? "text-danger " : "text-muted ";

				string data;
				if (IsSundayOrMonday(omerCnt))
				{
						data = $"<sup><small>{GetHebrew(omerCnt)}/{GetHebrew(omerCnt - 1)}</small></sup>";
				}
				else
				{
						data = $"<span class='{color}'>{GetHebrew(omerCnt)}</span>";
				}
				return data;
		}

		protected string Footer(int omerCnt)
		{
				if (IsSundayOrMonday(omerCnt))
				{
						return $"<p class='float-end'><span class='badge bg-info'>{omerCnt - 1}</span>/<span class='badge bg-info'>{omerCnt}</span></p>";
				}
				else
				{
						return $"<p class='ms-5'><span class='badge bg-info'>{omerCnt}</span></p>";
				}
		}
}
