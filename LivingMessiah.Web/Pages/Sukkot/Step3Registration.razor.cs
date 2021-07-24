using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using static LivingMessiah.Web.Links.Sukkot;

namespace LivingMessiah.Web.Pages.Sukkot
{
	public partial class Step3Registration
	{
		[Inject]
		NavigationManager NavManager { get; set; }

		[Parameter]
		public bool IsXs { get; set; }

		[Parameter]
		public StatusFlagEnum StatusFlagEnum { get; set; }

		[Parameter]
		public int RegistrationId { get; set; }

		void Details_ButtonClick(MouseEventArgs e, int id)
		{
			NavManager.NavigateTo(Details + "/" + id);
		}

		void Edit_ButtonClick(MouseEventArgs e, int id)
		{
			NavManager.NavigateTo(Links.Sukkot.CreateEdit + "/" + id);
		}

		void DeleteConfirmation_ButtonClick(MouseEventArgs e, int id)
		{
			NavManager.NavigateTo(DeleteConfirmation + "/" + id);
		}
	}
}
