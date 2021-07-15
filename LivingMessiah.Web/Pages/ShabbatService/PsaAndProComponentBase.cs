using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.ShabbatService
{
	public class PsaAndProComponentBase : ComponentBase
	{
		[Parameter]
		public bool IsPrinterFriendly { get; set; }

		[Parameter]
		public SectionEnum SectionEnum { get; set; }

		protected string _collapse;
		protected Section sec;
		protected string BadgeColor = "badge-warning";

		protected override Task OnInitializedAsync()
		{
			sec = Section.FromEnum(SectionEnum);

			if (IsPrinterFriendly)
			{
				_collapse = "";
			}
			else
			{
				_collapse = " collapse";
			}
			return base.OnInitializedAsync();  //protected override void OnInitialized()
		}

	}
}
