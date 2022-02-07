using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Sukkot.Web.Service;
using SukkotApi.Domain;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationShell
{
	public partial class DeleteConfirmation
	{
		[Inject]
		public ILogger<DeleteConfirmation> Logger { get; set; }

		[Inject]
		public ISukkotService svc { get; set; }

		[Inject]
		public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

		[Inject]
		NavigationManager NavManager { get; set; }

		public ClaimsPrincipal User { get; set; }

		[Parameter]
		public int Id { get; set; }

		public vwRegistration vwRegistration { get; set; }

		public string ExceptionMessage { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(DeleteConfirmation)}!{nameof(OnInitializedAsync)}; Id={Id}");
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			User = authState.User;

			vwRegistration = new vwRegistration();
			try
			{
				vwRegistration = await svc.DeleteConfirmation(Id, User);
			}
			catch (Exception)
			{
				ExceptionMessage = svc.ExceptionMessage;
				NavManager.NavigateTo(Links.Home.Error);
			}
			
		}

		protected async Task Delete_ButtonClick(int id)
		{
			Logger.LogDebug($"Inside {nameof(Delete_ButtonClick)}, Start Registration Deletion for id:{id} ");
			int count = 0;
			try
			{
				count = await svc.DeleteConfirmed(id);
			}
			catch (Exception)
			{
				ExceptionMessage = svc.ExceptionMessage;
				NavManager.NavigateTo(Links.Home.Error);
			}
			NavManager.NavigateTo(Links.Sukkot.Index);
		}
	}
}
