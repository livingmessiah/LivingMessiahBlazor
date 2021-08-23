using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Services;
using LivingMessiah.Domain;
using System;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Pages.Parasha
{
	public partial class IndexTable
	{
		[Inject]
		public ILogger<IndexTable> Logger { get; set; }

		[Inject]
		protected IShabbatWeekCacheService SvcCache { get; set; }

		[Inject]
		protected IShabbatWeekService Svc { get; set; }

		protected IReadOnlyList<LivingMessiah.Domain.Parasha.Queries.ParashaList> ParashaList;
		protected BibleBook Book { get; set; }
		protected LivingMessiah.Domain.Parasha.Queries.Parasha Parasha;

		[Parameter]
		public bool IsXsOrSm { get; set; }

		[Parameter]
		public int BookId { get; set; } = 0;

		protected string Colspan;
		protected int prevGregorianYear = 0;

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(IndexTable)}!{nameof(OnInitializedAsync)}");
			try
			{
				Colspan = (!IsXsOrSm) ? "8" : "6";
				if (BookId != 0)
				{
					ParashaList = await Svc.GetParashotByBookId(BookId);
					Book = await SvcCache.GetCurrentParashaTorahBookById(BookId);
				}
				else
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = $"Book Id = 0";
				}
			}
			catch (System.Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
		}

		public static string CurrentReadDateTextFormat(DateTime readDate)
		{
			DateTime compareDate = DateTime.Today;
			if (readDate >= compareDate & readDate <= compareDate.AddDays(6))
			{
				return "text-danger";
				//<span class='badge-danger'>@Title</span>
			}
			else
			{
				return "";
			}
		}

		public static string MyHebrewBibleParashaUrl(int id, string url)
		{
			string url2 = !String.IsNullOrEmpty(url) ? url : "";
			return "https://myhebrewbible.com/Parasha/Triennial/LivingMessiah/" + id.ToString() + "?slug=" + url2;
		}

	}
}
