using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using static LivingMessiah.Web.Pages.Sukkot.Constants.Other;

namespace LivingMessiah.Web.Pages.Sukkot
{
	public partial class HouseRulesConfirmationModal
	{
		[Parameter]
		public bool IsXs { get; set; }
		//bool IsXs2 = (ViewData[VDD.IsXs2] == null) ? false : (bool)ViewData[VDD.IsXs2];

		protected string ModalId;
		protected string FormatSize;
		const string Year = "2021";

		protected override async Task OnInitializedAsync()
		{
			base.OnInitialized();
			await Task.Delay(0);

			ModalId = IsXs ? ModalIdHouseRulesXs : ModalIdHouseRules;  //IsXs2 ?
			FormatSize = IsXs ? "" : "lead"; // IsXs2 ?
			

		}
	}
}
