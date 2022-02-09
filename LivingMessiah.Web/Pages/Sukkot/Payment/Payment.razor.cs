using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Sukkot.Web.Service;
using SukkotApi.Domain;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.Sukkot.Payment;

public partial class Payment
{
		[Inject]
		public ILogger<Payment> Logger { get; set; }

		[Inject]
		public ISukkotService svc { get; set; }

		[Inject]
		NavigationManager NavManager { get; set; }

		[Inject]
		public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

		[Parameter]
		public int Id { get; set; }

		public RegistrationSummary RegistrationSummary { get; set; }

		public ClaimsPrincipal User { get; set; }

		protected string AlertMsg = "";
		protected string ExceptionMessage = "";
		protected bool LoadFailed;

		protected override async Task OnInitializedAsync()
		{
				Logger.LogDebug($"Inside {nameof(Payment)}!{nameof(OnInitializedAsync)}; Id={Id}");
				RegistrationSummary = new RegistrationSummary();
				try
				{
						var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
						User = authState.User;
						RegistrationSummary = await svc.Summary(Id, User);
						LoadFailed = false;
				}
				catch (Exception)
				{
						LoadFailed = true;
						ExceptionMessage = svc.ExceptionMessage;
						NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
				}

		}

}
