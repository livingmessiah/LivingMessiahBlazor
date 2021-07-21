using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Sukkot
{
	public partial class RegistrationBodyXs
	{
		[Parameter]
		public bool IsXs { get; set; }

		[Parameter]
		public string ModalId { get; set; }

		[Parameter]
		public StatusFlagEnum StatusFlagEnum { get; set; }

	}
}
