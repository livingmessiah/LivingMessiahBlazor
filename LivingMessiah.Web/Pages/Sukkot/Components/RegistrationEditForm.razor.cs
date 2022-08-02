using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.Sukkot.Services;
using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using LivingMessiah.Web.Services;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Services;

namespace LivingMessiah.Web.Pages.Sukkot.Components;

public partial class RegistrationEditForm
{
	[Inject]
	public IRegistrationService svc { get; set; }

	[Inject]
	public ILogger<RegistrationEditForm> Logger { get; set; }

	[Inject]
	AppState AppState { get; set; }

	[Parameter, EditorRequired]
	public int? Id { get; set; }

	[Parameter, EditorRequired]
	public string Email { get; set; }

	//public RegistrationVM VM { get; set; }
	public RegistrationVM VM { get; set; } = new RegistrationVM();

	public DateRangeLocal DateRangeAttendance { get; set; } = DateRangeLocal.FromEnum(DateRangeEnum.AttendanceDays);

	private FluentValidationValidator? _fluentValidationValidator;


	//private async Task GetUserAndClaims()
	//{
	//	var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
	//	User = authState.User;
	//}

	private int Id2;
	protected override async Task OnInitializedAsync()
	{
		Id2 = Id ?? 0;
		Logger.LogInformation(string.Format("Inside {0}; Id2:{1}", nameof(RegistrationEditForm) + "!" + nameof(OnInitializedAsync), Id2));

		try
		{
			if (Id2 == 0)
			{
				//VM = new RegistrationVM { Id = 0, EMail = Email };
				VM.EMail = Email;
			}
			else
			{
				VM = await svc.GetByIdVer2(Id2);
				AppState.UpdateMessage(this, GetNotificationMessage());
			}

			SetUiForAddOrEdit();

		}
		catch (RegistratationException registratationException)
		{
			AppState.UpdateMessage(this, registratationException.Message);  
		}

		catch (InvalidOperationException invalidOperationException)
		{
			AppState.UpdateMessage(this, invalidOperationException.Message); 
		}

		Logger.LogInformation(string.Format("...finished {0}", nameof(RegistrationEditForm) + "!" + nameof(OnInitializedAsync)));
	}

	protected string Title = "";
	protected string Title2 = "";
	protected string SubmitButtonText = "";

	private void SetUiForAddOrEdit()
	{
		if (Id2 == 0)
		{
			Title = "Add - Registration";
			Title2 = $"{Email}";
			SubmitButtonText = "Save";
		}
		else
		{
			Title = "Edit - Registration";
			Title2 = $"{Email} - #{VM.Id}";
			SubmitButtonText = "Update";
		}
	}

	private string GetNotificationMessage()
	{
		if (Id2 != 0)
		{
			return $"Registration record for {Email} - #{VM.Id} received from database";
		}
		else
		{
			return String.Empty; // shouldn't happen
		}
	}


	protected async Task SubmitValidForm()
	{
		Logger.LogDebug(string.Format("Inside {0} Id2:{1}"
		, nameof(RegistrationEditForm) + "!" + nameof(SubmitValidForm), nameof(Id2)));

		if (Id2 == 0)  // Add
		{
			try
			{
				var sprocTuple = await svc.CreateVer2(VM);
				Logger.LogInformation(string.Format("...Registration created! newId={0}", Id2));
				AppState.UpdateMessage(this, "Registration Added!");
			}
			catch (InvalidOperationException invalidOperationException)
			{
				AppState.UpdateMessage(this, invalidOperationException.Message);
			}
		}
		else  // Edit
		{
			try
			{
				var sprocTuple = await svc.UpdateVer2(VM);
				Logger.LogInformation("...Registration Updated!");
				AppState.UpdateMessage(this, "Registration Updated!");
			}

			catch (InvalidOperationException invalidOperationException)
			{
				AppState.UpdateMessage(this, invalidOperationException.Message);
			}
		}
	}

}
