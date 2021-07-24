using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SukkotApi.Domain;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using static LivingMessiah.Web.Links.Sukkot;


namespace LivingMessiah.Web.Pages.Sukkot
{
	public partial class Step4MealTicket
	{
		[Inject]
		NavigationManager NavManager { get; set; }

		[Parameter]
		public StatusFlagEnum StatusFlagEnum { get; set; }

		[Parameter]
		public vwRegistrationShell vwRegistrationShell { get; set; }
		
		void Edit_ButtonClick(MouseEventArgs e, int id)
		{
			NavManager.NavigateTo(Links.Sukkot.Links2.EditMeals + "/" + id);
		}

		void DetailsMealTicket_ButtonClick(MouseEventArgs e, int id)
		{
			NavManager.NavigateTo(Links.Sukkot.Links2.DetailsMealTicket + "/" + id);
		}

		void KitchenWork_ButtonClick(MouseEventArgs e, int id)
		{
			NavManager.NavigateTo(LivingMessiah.Web.Links.Sukkot.KitchenWork.Index + "/" + id);
		}
	}
}
