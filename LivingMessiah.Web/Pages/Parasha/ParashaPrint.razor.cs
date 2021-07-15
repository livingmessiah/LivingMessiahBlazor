using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Services;
using LivingMessiah.Domain;

namespace LivingMessiah.Web.Pages.Parasha
{
	public partial class ParashaPrint
	{
		[Inject]
		protected IShabbatWeekCacheService SvcCache { get; set; }

		[Inject]
		protected IShabbatWeekService Svc { get; set; }

		//[Parameter]
		public int BookId { get; set; }

		protected IReadOnlyList<vwParasha> Parasha;
		protected BibleBook Book { get; set; }
		protected vwCurrentParasha CurrentParasha;

		protected override async Task OnInitializedAsync()
		{
			CurrentParasha = await SvcCache.GetCurrentParasha();
			BookId = CurrentParasha.BookId;
			Parasha = await Svc.GetParashotByBookId(BookId);
			Book = await SvcCache.GetCurrentParashaTorahBookById(BookId);
			
		}
	}
}
