using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sukkot.Web.Service;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using LivingMessiah.Web.Infrastructure;
using SukkotApi.Data;
using SukkotApi.Domain;
using SukkotApi.Domain.Enums;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LivingMessiah.Web.Pages.Sukkot
{
	[Authorize]
	public partial class CreateEdit
	{
		[Inject]
		public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

		[Inject]
		public ISukkotService svc { get; set; }

		[Inject]
		public ILogger<CreateEdit> Logger { get; set; }

		[Parameter]
		public vwRegistrationShell Model { get; set; }

		public UI UI { get; set; }

		//ToDo what goes here?
		//[BindProperty]
		public Registration Registration { get; set; }

		public string Email { get; set; }
		public bool Verified { get; set; }
		public string Role { get; set; }
		private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();

		[Parameter]
		public int? id { get; set; }

		//protected override async Task OnInitializedAsync()
		//{
		//	base.OnInitialized();
		//	var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		//	var user = authState.User;

		//	if (user.Identity.IsAuthenticated)
		//	{
		//		Verified = true;
		//		_claims = user.Claims;
		//	}
		//	else
		//	{
		//		Verified = false;
		//	}
		//	Email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
		//	Role = user.Claims.FirstOrDefault(c => c.Type == "https://schemas.livingmessiah.com/roles")?.Value;

		//	SetTitle();

		//	int Id2 = id.HasValue ? id.Value : 0; // if id? is null, Id2 is set to 0 and...
		//	UI = (Id2 == 0) ? new UI(SukkotEnums.CRUD.Add) : new UI(SukkotEnums.CRUD.Edit); // ...  an Add is assumed (i.e. SukkotEnums.CRUD.Add)

		//	Logger.LogInformation($"Inside {nameof(CreateEdit)}!{nameof(OnInitializedAsync)}, id2={Id2}, UI.CRUD={UI.CRUD}");

		//	try
		//	{
		//		if (UI.CRUD == SukkotEnums.CRUD.Add)
		//		{
		//			Registration = new Registration
		//			{
		//				Id = 0,
		//				//HouseRulesAgreement = DateTime.UtcNow, // Task 687: Persist the moment House Rules were agreed to database
		//				//EMail = User.GetUserEmail()
		//			};
		//		}
		//		else
		//		{
		//			//Registration = await svc.Update(Id2, User);
		//		}
		//	}
		//	catch (RegistratationException e)
		//	{
		//		//return RedirectToPage(Anchors.Sukkot.Registration, new { simpleAlertMsg = e.Message ?? "--" });
		//	}

		//	catch (Exception)
		//	{
		//		//ExceptionMessage = svc.ExceptionMessage;
		//		//return RedirectToPage(Anchors.Error.RedirectPageName);
		//	}

		//	Logger.LogInformation($"Finishing {nameof(CreateEdit)}!{nameof(OnInitializedAsync)}, return Page()");
		//	//return Page();
		//}

		protected string Title = "";
		protected string Title2 = "";

		private void SetTitle()
		{
			Title = UI.Title + " - Registration";

			if (Registration != null)
			{
				if (UI.EditMode)
				{
					Title2 = $"{Registration.EMail ?? "NO EMAIL!"} - #{Registration.Id}";
				}
				else
				{
					Title2 = $"{Registration.EMail ?? "NO EMAIL!"}";
				}
			}
			else
			{
				Title2 = "Model.Registration is null";
			}
		}

	}
}
