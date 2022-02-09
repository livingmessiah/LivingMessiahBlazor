using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using static LivingMessiah.Web.Links.Sukkot;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationShell;

public partial class Step3Registration
{
		[Inject]
		NavigationManager NavManager { get; set; }

		[Inject]
		public ILogger<Step3Registration> Logger { get; set; }

		[Parameter]
		public bool IsXs { get; set; }

		[Parameter]
		public StatusFlagEnum StatusFlagEnum { get; set; }

		[Parameter]
		public int RegistrationId { get; set; }

		void Details_ButtonClick(MouseEventArgs e, int id, bool showPrintMsg)
		{
				NavManager.NavigateTo(LivingMessiah.Web.Links.Sukkot.Details + "/" + id + "/" + showPrintMsg);
		}

		void Edit_ButtonClick(MouseEventArgs e, int id)
		{
				//Logger.LogDebug($"Inside {nameof(Step3Registration)}!{nameof(Edit_ButtonClick)}, id:{id}");
				NavManager.NavigateTo(Links.Sukkot.CreateEdit + "/" + id);
		}

		void DeleteConfirmation_ButtonClick(MouseEventArgs e, int id)
		{
				NavManager.NavigateTo(Links.Sukkot.DeleteConfirmation + "/" + id);
		}
}
