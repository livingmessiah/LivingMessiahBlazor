using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using LivingMessiah.Domain;
using LivingMessiah.Web.Services;

namespace LivingMessiah.Web.Shared
{
	public partial class ParashaCalendar
	{
		protected vwCurrentParasha Parasha;

		[Inject]
		public IShabbatWeekCacheService Svc { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Parasha = await Svc.GetCurrentParasha();
		}


	}
}
