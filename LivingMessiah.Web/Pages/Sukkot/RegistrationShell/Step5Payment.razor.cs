using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using Microsoft.AspNetCore.Components;
using SukkotApi.Domain;
using Microsoft.AspNetCore.Components.Web;
using static LivingMessiah.Web.Links.Sukkot;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationShell;

public partial class Step5Payment
{
		[Inject]
		NavigationManager NavManager { get; set; }

		[Parameter]
		public vwRegistrationShell vwRegistrationShell { get; set; }

		[Parameter]
		public StatusFlagEnum StatusFlagEnum { get; set; }

		void Payment_ButtonClick(MouseEventArgs e, int id)
		{
				NavManager.NavigateTo(Links2.Payment + "/" + id);
		}

		void Details_ButtonClick(MouseEventArgs e, int id, bool showPrintMsg)
		{
				NavManager.NavigateTo(LivingMessiah.Web.Links.Sukkot.Details + "/" + id + "/" + showPrintMsg);
		}

}
