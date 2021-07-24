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
using static LivingMessiah.Web.Links.Sukkot;


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

		[Inject]
		NavigationManager NavManager { get; set; }

		public UI UI { get; set; }

		public Registration Registration { get; set; }

		public ClaimsPrincipal User { get; set; }

		[Parameter]
		public int? id { get; set; }

		protected bool LoadFailed;

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(CreateEdit)}!{nameof(OnInitializedAsync)}, id: {id}");
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			User = authState.User;

			int Id2 = id.HasValue ? id.Value : 0; // if id? is null, Id2 is set to 0 and...
			UI = (Id2 == 0) ? new UI(SukkotEnums.CRUD.Add) : new UI(SukkotEnums.CRUD.Edit); // ...  an Add is assumed (i.e. SukkotEnums.CRUD.Add)
																																											
			Logger.LogDebug($"Inside {nameof(CreateEdit)}!{nameof(OnInitializedAsync)}, id2={Id2}, UI.CRUD={UI.CRUD}");

			try
			{
				if (UI.CRUD == SukkotEnums.CRUD.Add)
				{
					Registration = new Registration
					{
						Id = 0,
						//HouseRulesAgreement = DateTime.UtcNow, // Task 687: Persist the moment House Rules were agreed to database
						EMail = User.GetUserEmail()
					};
				}
				else
				{
					Registration = await svc.Update(Id2, User);
				}

				SetTitle();

			}
			catch (RegistratationException e)
			{
				LoadFailed = true;
				Logger.LogWarning(e, $"Failed to load page {nameof(CreateEdit)}");
			}

			catch (Exception ex)
			{
				LoadFailed = true;
				ExceptionMessage = svc.ExceptionMessage;
				NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
			}

			Logger.LogInformation($"Finished {nameof(CreateEdit)}!{nameof(OnInitializedAsync)}");
		}

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

		protected string AlertMsg = "";
		protected string ExceptionMessage = "";

		protected async Task HandleValidSubmit()
		{
			Logger.LogDebug($"Inside {nameof(HandleValidSubmit)}, UI.EditMode: {UI.EditMode} ");

			if (UI.EditMode)
			{
				UI = new UI(SukkotEnums.CRUD.Edit);
				int count = 0;
				try
				{
					count = await svc.Edit(Registration, User);
				}
				catch (Exception)
				{
					ExceptionMessage = svc.ExceptionMessage; // Log is handled in the service
					NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
				}

				AlertMsg = $"Registration Updated!";
				Logger.LogInformation(AlertMsg);
				NavManager.NavigateTo(LivingMessiah.Web.Links.Sukkot.RegistrationShell);

			}
			else
			{
				int newId = 0;
				try
				{
					newId = await svc.Create(Registration, User);
				}
				catch (Exception)
				{
					ExceptionMessage = svc.ExceptionMessage; // Log is handled in the service
					NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
				}
				AlertMsg = $"Registration created! Id={newId}";
				Logger.LogInformation(AlertMsg);
				NavManager.NavigateTo(LivingMessiah.Web.Links.Sukkot.RegistrationShell);

			}
		}

		public SukkotApi.Domain.Enums.CampType CampType { get; set; }
		public SukkotApi.Domain.Enums.Status StatusEnum { get; set; }
		public SukkotApi.Domain.Enums.SukkotAttendanceDays AttendanceDaysEnum { get; set; }

	}
}
