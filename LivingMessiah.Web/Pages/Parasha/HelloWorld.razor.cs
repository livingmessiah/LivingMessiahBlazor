using LivingMessiah.Domain;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Parasha
{
	public partial class HelloWorld
	{
		[Parameter]
		public vwCurrentParasha vwCurrentParasha { get; set; }
	}
}
