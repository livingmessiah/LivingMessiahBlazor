using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Sukkot.Web.Service;
using SukkotApi.Domain;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
//using static LivingMessiah.Web.Links.Sukkot;

namespace LivingMessiah.Web.Pages.Sukkot.Details
{
	public partial class Details
	{
		[Inject]
		public ISukkotService svc { get; set; }

		[Inject]
		public ILogger<Details> Logger { get; set; }
		
		[Inject]
		public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
		
		[Inject]
		NavigationManager NavManager { get; set; }

		[Parameter]
		public int Id { get; set; }

		[Parameter]
		public bool showPrintInstructionMessage { get; set; } = true;

		public vwRegistration vwRegistration { get; set; }
		public ClaimsPrincipal User { get; set; }

		protected string ExceptionMessage = "";
		//protected bool LoadFailed;  // using <LoadingComponent>

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(Details)}!{nameof(OnInitializedAsync)}, Id: {Id}");
			
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			User = authState.User;

			try
			{
				Logger.LogDebug($"Calling {nameof(svc.Details)}");
				vwRegistration = await svc.Details(Id, User, showPrintInstructionMessage);
			}
			catch (Exception)
			{
				//LoadFailed = true;
				ExceptionMessage = svc.ExceptionMessage;
				NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
			}


		}

		protected bool MakeModalVisible = false;

		void PaymentInstructions_ButtonClick()
		{
			Logger.LogDebug($"Event: {nameof(PaymentInstructions_ButtonClick)} clicked");
			MakeModalVisible = true;
			StateHasChanged();

		}
		void CancelModal_ButtonClick()
		{
			Logger.LogDebug($"Event: {nameof(CancelModal_ButtonClick)} clicked");
			MakeModalVisible = false;
			StateHasChanged();
		}

		void Edit_ButtonClick(MouseEventArgs e, int id)
		{
			NavManager.NavigateTo(Links.Sukkot.CreateEdit + "/" + id);
		}


	}
}
