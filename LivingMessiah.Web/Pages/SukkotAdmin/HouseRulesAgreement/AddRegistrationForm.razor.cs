using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.HouseRulesAgreement;

public partial class AddRegistrationForm
{
	[Inject] public IService? svc { get; set; }
	[Inject] public ILogger<AddRegistrationForm>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	[Parameter, EditorRequired] public string? ChosenEmail { get; set; }

	protected string? Email;

	public ViewModel VM { get; set; } = new ViewModel();
	public Sukkot.Enums.DateRangeType DateRangeAttendance { get; set; } = Sukkot.Enums.DateRangeType.Attendance;
	public List<RegistrationLookup>? RegistrationLookupList { get; set; }

	protected override void OnInitialized()
	{
		Logger!.LogDebug(string.Format("Inside {0}, ChosenEmail: {1}",
			nameof(AddRegistrationForm) + "!" + nameof(OnInitialized), ChosenEmail));

		if (!string.IsNullOrEmpty(ChosenEmail))
		{
			ChosenEmail = Email;
			VM.EMail = Email;
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
			VM.Id = 0;
			VM.Status = Status.StartRegistraion;
			//RegistrationVM.EMail = Email; HACK: data gotten from the form; cant figure out how to populate via EventCallbacks

			var sprocTuple = await svc!.Create(VM);
			if (sprocTuple.NewId != 0)
			{
				Toast!.ShowInfo(sprocTuple.ReturnMsg);
				VM = new ViewModel();
			}
			else
			{
				if (sprocTuple.SprocReturnValue == 2601) // Unique Index Violation
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
