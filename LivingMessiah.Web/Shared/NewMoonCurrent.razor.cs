using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using LivingMessiah.Web.Pages.KeyDates.Queries;

namespace LivingMessiah.Web.Shared
{
	public partial class NewMoonCurrent
	{
		[Parameter]
		public int Id { get; set; }

		// ToDo: Task-786
		public LunarMonth Month { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await Task.Delay(0);

			// ToDo: Task-786
			//Month = KeyDateFactory.LunarMonths().Where(w => w.Id == Id).SingleOrDefault();
		}
	}
}
