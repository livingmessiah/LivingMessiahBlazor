using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static LivingMessiah.Web.Pages.Sukkot.Constants.Other;
using static LivingMessiah.Web.Links.Sukkot;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationShell
{
	public partial class HouseRulesConfirmationModal
	{
		[Inject]
		NavigationManager NavManager { get; set; }

		[Inject]
		public ILogger<HouseRulesConfirmationModal> Logger { get; set; }

		[Parameter]
		public bool IsXs { get; set; }

		protected string ModalId;
		protected string FormatSize;
		const string Year = "2021";

		protected bool MakeModalVisible = false;

		protected override async Task OnInitializedAsync()
		{
			base.OnInitialized();
			await Task.Delay(0);

			ModalId = IsXs ? ModalIdHouseRulesXs : ModalIdHouseRules;  //IsXs2 ?
			FormatSize = IsXs ? "" : "lead"; // IsXs2 ?
		}


		void BeginRegistration_ButtonClick() 
		{
			Logger.LogDebug($"Event: {nameof(BeginRegistration_ButtonClick)} clicked");
			MakeModalVisible = true;
			StateHasChanged();
		}

		void CancelModal_ButtonClick() 
		{
			Logger.LogDebug($"Event: {nameof(CancelModal_ButtonClick)} clicked");
			MakeModalVisible = false;
			StateHasChanged();
		}

		void DoNotAgree_ButtonClick()
		{
			Logger.LogDebug($"Event: {nameof(DoNotAgree_ButtonClick)} clicked");
			NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Index);
		}

		//async Task Agree_ButtonClick()
		void Agree_ButtonClick()
		{
			Logger.LogDebug($"Event: {nameof(Agree_ButtonClick)} clicked; Navigate to CreateEdit");
			NavManager.NavigateTo(LivingMessiah.Web.Links.Sukkot.CreateEdit);
		}


	}
}
