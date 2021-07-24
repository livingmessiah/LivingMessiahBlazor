using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Sukkot.Web.Service;
using SukkotApi.Domain;
using System.Security.Claims;
using System.Threading.Tasks;
//using static LivingMessiah.Web.Links.Sukkot;

namespace LivingMessiah.Web.Pages.Sukkot
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
			catch (System.Exception)
			{

				throw;
			}

			
		}

		void Edit_ButtonClick(MouseEventArgs e, int id)
		{
			NavManager.NavigateTo(Links.Sukkot.CreateEdit + "/" + id);
		}

	}
}
