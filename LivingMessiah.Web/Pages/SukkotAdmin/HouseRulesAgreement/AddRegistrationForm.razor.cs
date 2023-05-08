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
	[Inject] public IRegistrationAdminService? svc { get; set; }
	[Inject] public ILogger<AddRegistrationForm>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	[Parameter, EditorRequired] public string? ChosenEmail { get; set; }

	protected string? Email;

	public RegistrationVM RegistrationVM { get; set; } = new RegistrationVM();

	public Sukkot.Enums.DateRangeType DateRangeAttendance { get; set; } = Sukkot.Enums.DateRangeType.Attendance;

	public List<RegistrationLookup>? RegistrationLookupList { get; set; }

	protected override void OnInitialized()
	{
		Logger!.LogDebug(string.Format("Inside {0}, ChosenEmail: {1}",
			nameof(AddRegistrationForm) + "!" + nameof(OnInitialized), ChosenEmail));
		RegistrationVM = new RegistrationVM();
		if (!string.IsNullOrEmpty(ChosenEmail))
		{
			ChosenEmail = Email;
			RegistrationVM.EMail = Email;
			Toast!.ShowInfo($"Email: {Email}");
		}
		else
		{
			//Toast.ShowWarning("ChosenEmail is Empty!");  HACK: data gotten from the form; cant figure out how to populate via EventCallbacks
		}
	}

	protected async Task HandleValidSubmit()
	{
		Logger!.LogDebug($"Inside {nameof(HandleValidSubmit)}");

		try
		{
			RegistrationVM.Id = 0;
			RegistrationVM.Status = Status.StartRegistraion;
			//RegistrationVM.EMail = Email; HACK: data gotten from the form; cant figure out how to populate via EventCallbacks

			var sprocTuple = await svc!.Create(RegistrationVM);
			if (sprocTuple.NewId != 0)
			{
				Toast!.ShowInfo(sprocTuple.ReturnMsg);
				RegistrationVM = new RegistrationVM();
			}
			else
			{
				if (sprocTuple.SprocReturnValue == ReturnValueViolationInUniqueIndex)
				{
					Toast!.ShowWarning(sprocTuple.ReturnMsg);
				}
				else
				{
					Toast!.ShowError(sprocTuple.ReturnMsg);
				}
			}

		}
		catch (Exception)
		{
			Toast!.ShowError("An invalid operation occurred during Registration Creation, contact your administrator");
		}


	}

}
