using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SukkotApi.Domain;
using static LivingMessiah.Web.Links.Sukkot;

namespace LivingMessiah.Web.Pages.Sukkot
{
	public partial class DetailsContent
	{
		[Parameter]
		public vwRegistration vwRegistration { get; set; }
	}
}
