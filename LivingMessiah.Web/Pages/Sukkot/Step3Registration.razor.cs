using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using Microsoft.AspNetCore.Components;
using SukkotApi.Domain;

namespace LivingMessiah.Web.Pages.Sukkot
{
	public partial class Step3Registration
	{
		[Parameter]
		public bool IsXs { get; set; }

		//[Parameter]
		//public string ModalId { get; set; }

		[Parameter]
		public StatusFlagEnum StatusFlagEnum { get; set; }

		[Parameter]
		public int RegistrationId { get; set; }

		//[Parameter]
		//public vwRegistrationShell Model { get; set; }
	}
}
