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

		protected bool LoadFailed;

		protected IReadOnlyList<vwParasha> Parasha;
		protected BibleBook Book { get; set; }
		protected vwCurrentParasha CurrentParasha;

		[Parameter]
		public bool IsXs { get; set; }

		//[Parameter]
		public int BookId { get; set; }

		protected string Colspan;
		protected int prevGregorianYear = 0;

		protected override async Task OnInitializedAsync()
		{
			try
			{
				LoadFailed = false;
				Colspan = (!IsXs) ? "8" : "6";
				CurrentParasha = await SvcCache.GetCurrentParasha();
				BookId = CurrentParasha.BookId;  // why can't I pass this in as a Parameter? 
				Parasha = await Svc.GetParashotByBookId(BookId);
				Book = await SvcCache.GetCurrentParashaTorahBookById(BookId);
			}
			catch (System.Exception ex)
			{
				LoadFailed = true;
				Logger.LogError(ex, $"Inside {nameof(IndexTable)}");
			}
		}

		public static string CurrentReadDateTextFormat(DateTime readDate, string textColor = "text-success")
		{
			DateTime compareDate = DateTime.Today;
			if (readDate >= compareDate & readDate <= compareDate.AddDays(6))
			{
				return "text-danger";
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
