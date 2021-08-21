using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Services;
using LivingMessiah.Domain.Parasha.Queries;

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

		protected IReadOnlyList<ParashaList> ParashaList;
		//protected LivingMessiah.Domain.Parasha.Queries.BibleBook Book { get; set; }
		protected LivingMessiah.Domain.Parasha.Queries.Parasha CurrentParasha;

		protected override async Task OnInitializedAsync()
		{
			CurrentParasha = await SvcCache.GetCurrentParasha();
			BookId = CurrentParasha.BookId;
			ParashaList = await Svc.GetParashotByBookId(BookId);
			//Book = await SvcCache.GetCurrentParashaTorahBookById(BookId);
		}
	}
}