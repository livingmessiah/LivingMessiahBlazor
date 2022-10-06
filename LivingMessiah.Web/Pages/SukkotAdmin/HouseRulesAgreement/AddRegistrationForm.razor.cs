using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Services; // using service, don't need .Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;
using static LivingMessiah.Web.Pages.Sukkot.Constants.SqlServer;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.HouseRulesAgreement;

public partial class AddRegistrationForm
{
	[Parameter, EditorRequired] public string ChosenEmail { get; set; }

	protected string Email;

	[Inject]
	public IRegistrationService svc { get; set; }

	[Inject]
	public ILogger<AddRegistrationForm> Logger { get; set; }

	[Inject]
	public IToastService Toast { get; set; }

	public RegistrationVM RegistrationVM { get; set; } = new RegistrationVM();

	//ToDo this should come from the Sukkot.Constants and saved in cache		
	public Sukkot.DateRangeLocal DateRangeAttendance { get; set; } = Sukkot.DateRangeLocal.FromEnum(Sukkot.DateRangeEnum.AttendanceDays);

	public List<RegistrationLookup> RegistrationLookupList { get; set; }

	protected override void OnInitialized()
	{
		Logger.LogDebug(string.Format("Inside {0}, ChosenEmail: {1}",
			nameof(AddRegistrationForm) + "!" + nameof(OnInitialized), ChosenEmail));
		RegistrationVM = new RegistrationVM();
		if (!string.IsNullOrEmpty(ChosenEmail))
		{
			ChosenEmail = Email;
			RegistrationVM.EMail = Email;
			Toast.ShowInfo($"Email: {Email}");
		}
		else
		{
			//Toast.ShowWarning("ChosenEmail is Empty!");  HACK: data gotten from the form; cant figure out how to populate via EventCallbacks
		}
	}

	protected async Task HandleValidSubmit()
	{
		Logger.LogDebug($"Inside {nameof(HandleValidSubmit)}");

		try
		{
			RegistrationVM.Id = 0;
			RegistrationVM.Status = Status.StartRegistraion;
			//RegistrationVM.EMail = Email; HACK: data gotten from the form; cant figure out how to populate via EventCallbacks

			var sprocTuple = await svc.Create(RegistrationVM);
			if (sprocTuple.Item1 != 0)
			{
				Toast.ShowInfo(sprocTuple.Item3);
				RegistrationVM = new RegistrationVM();
			}
			else
			{
				if (sprocTuple.Item2 == ReturnValueViolationInUniqueIndex)
				{
					Toast.ShowWarning(sprocTuple.Item3);
				}
				else
				{
					Toast.ShowError(sprocTuple.Item3);
				}
			}

		}
		catch (Exception)
		{
			Toast.ShowError("An invalid operation occurred during Registration Creation, contact your administrator");
		}


	}

}
