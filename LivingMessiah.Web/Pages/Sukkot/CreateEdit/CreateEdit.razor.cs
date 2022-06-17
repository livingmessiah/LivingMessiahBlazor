using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sukkot.Web.Service;
using LivingMessiah.Web.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SukkotApi.Domain.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.CreateEdit;

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

	public RegistrationVM Registration { get; set; }

	public ClaimsPrincipal User { get; set; }


	//ToDo this should come from the Sukkot.Constants and saved in cache		
	public DateRangeLocal DateRangeAttendance { get; set; } = DateRangeLocal.FromEnum(DateRangeEnum.AttendanceDays);

	[Parameter]
	public int? id { get; set; }

	protected override async Task OnInitializedAsync()
	{
		// I want to elevate the logging from Debug to Information because this is the main point of the application
		Logger.LogInformation(string.Format("Inside {0}", nameof(CreateEdit) + "!" + nameof(OnInitializedAsync)));

		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
		User = authState.User;

		int Id2 = id.HasValue ? id.Value : 0; // if id? is null, Id2 is set to 0 and...
		UI = (Id2 == 0) ? new UI(SukkotEnums.CRUD.Add) : new UI(SukkotEnums.CRUD.Edit); // ...  an Add is assumed (i.e. SukkotEnums.CRUD.Add)
		Logger.LogInformation(string.Format("...id2={0} UI.CRUD={1}", nameof(Id2), nameof(UI.CRUD)));

		try
		{
			if (UI.CRUD == SukkotEnums.CRUD.Add)
			{
				Registration = new RegistrationVM
				{
					Id = 0,
					Status = Status.EmailConfirmation,
					EMail = User.GetUserEmail()
				};
			}
			else
			{
				Registration = await svc.Update(Id2, User);
			}

			SetTitle();

		}
		catch (RegistratationException registratationException)
		{
			DatabaseWarning = true;
			DatabaseWarningMsg = registratationException.Message; 
		}
		
		catch (InvalidOperationException invalidOperationException)
		{
			DatabaseError = true;
			DatabaseErrorMsg = invalidOperationException.Message;
		}
		Logger.LogInformation(string.Format("Finished {0}", nameof(CreateEdit) + "!" + nameof(OnInitializedAsync)));
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

	protected async Task HandleValidSubmit()
	{
		Logger.LogDebug(string.Format("Inside {0} UI.EditMode::{1}"
			, nameof(CreateEdit) + "!" + nameof(HandleValidSubmit), nameof(UI.EditMode) ));
		if (UI.EditMode)
		{
			UI = new UI(SukkotEnums.CRUD.Edit);
			int count = 0;
			try
			{
				count = await svc.Edit(Registration, User);
			}

			catch (InvalidOperationException invalidOperationException)
			{
				DatabaseError = true;
				DatabaseErrorMsg = invalidOperationException.Message;
			}

			Logger.LogInformation("Registration Updated!");
			NavManager.NavigateTo(LivingMessiah.Web.Links.Sukkot.RegistrationShell);

		}
		else
		{
			int newId = 0;
			try
			{
				newId = await svc.Create(Registration, User);
			}
			catch (InvalidOperationException invalidOperationException)
			{
				DatabaseError = true;
				DatabaseErrorMsg = invalidOperationException.Message;
			}
			Logger.LogInformation(string.Format("Registration created! newId={0}", newId ));
			NavManager.NavigateTo(LivingMessiah.Web.Links.Sukkot.RegistrationShell);

		}
	}


	#region ErrorHandling

	private void InitializeErrorHandling()
	{
		DatabaseInformationMsg = "";
		DatabaseInformation = false;
		DatabaseWarningMsg = "";
		DatabaseWarning = false;
		DatabaseErrorMsg = "";
		DatabaseError = false;
	}

	protected bool DatabaseInformation = false;
	protected string DatabaseInformationMsg { get; set; }
	protected bool DatabaseWarning = false;
	protected string DatabaseWarningMsg { get; set; }
	protected bool DatabaseError { get; set; } // = false; handled by InitializeErrorHandling
	protected string DatabaseErrorMsg { get; set; }
	#endregion


} 
