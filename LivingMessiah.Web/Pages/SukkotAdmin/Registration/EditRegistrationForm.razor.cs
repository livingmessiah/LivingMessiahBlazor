using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Services; // using service, don't need .Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Data;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;
using static LivingMessiah.Web.Pages.Sukkot.Constants.SqlServer;

using Syncfusion.Blazor.DropDowns;
using System.Collections.Generic;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration;

public partial class EditRegistrationForm
{
	[Inject] public IRegistrationService svc { get; set; }
	[Inject] public IRegistrationRepository db { get; set; }
	[Inject] public ILogger<EditRegistrationForm> Logger { get; set; }
	[Inject] public IToastService Toast { get; set; }

	private bool ShowEditForm = false;

	public RegistrationVM RegistrationVM { get; set; } = new RegistrationVM();

	//ToDo this should come from the Sukkot.Constants and saved in cache		
	public Sukkot.DateRangeLocal DateRangeAttendance { get; set; } = Sukkot.DateRangeLocal.FromEnum(Sukkot.DateRangeEnum.AttendanceDays);


	public List<RegistrationLookup> RegistrationLookupList { get; set; }

	private string Title = "";
	private string msg = "";

	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(EditRegistrationForm) + "!" + nameof(OnInitialized)));

		Title = "Registration Edit ";
		try
		{
			RegistrationLookupList = await db.PopulateRegistrationLookup();
		}
		catch (Exception)
		{
			Toast.ShowError("Error getting registration lookup from database, contact your administrator");
		}
	}

	#region AutoComplete

	private int SelectedRegistrantId = 0;
	private string SelectedRegistrantName = "";

	private void OnValueChanged(ChangeEventArgs<string, RegistrationLookup> args)
	{
		if (String.IsNullOrEmpty(args.Value))
		{
			SelectedRegistrantId = 0;
			ShowEditForm = false;
		}
	}

	public async Task OnSelect(SelectEventArgs<RegistrationLookup> args)
	{
		SelectedRegistrantId = int.TryParse(args.ItemData.ID, out SelectedRegistrantId) ? SelectedRegistrantId : 0;
		await GetRegistration(SelectedRegistrantId);
	}

	private async Task GetRegistration(int registrationId)
	{
		
		Logger.LogDebug(string.Format("Inside {0}; registrationId:{1}"
			, nameof(EditRegistrationForm) + "!" + nameof(GetRegistration), registrationId));
		try
		{
			RegistrationVM = await svc.GetById(registrationId);
			if (RegistrationVM == null)
			{
				msg = $"Registration not found; registrationId:{registrationId}";
				Toast.ShowWarning(msg);
				Logger.LogWarning("..." + msg);
			}
			else
			{
				Title = "Registration Edit " + registrationId;
				ShowEditForm = true;
			}
		}
		catch (Exception ex)
		{
			Toast.ShowError("Error reading database");
			Logger.LogError(ex, $"...Error reading database");
		}
		StateHasChanged();  // https://stackoverflow.com/questions/56436577/blazor-form-submit-needs-two-clicks-to-refresh-view
	}

	#endregion


	protected async Task HandleValidSubmit()
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(EditRegistrationForm) + "!" + nameof(HandleValidSubmit)));
		try
		{
			var sprocTuple = await svc.Update(RegistrationVM);
			if (sprocTuple.RowsAffected != 0)
			{
				msg = $"{sprocTuple.ReturnMsg}";
				RegistrationVM = await svc.GetById(RegistrationVM.Id); //ToDo: do I need to refresh the data?
				Toast.ShowInfo(sprocTuple.ReturnMsg);
			}
			else
			{
				if (sprocTuple.SprocReturnValue == ReturnValueViolationInUniqueIndex)
				{
					Toast.ShowWarning(sprocTuple.ReturnMsg);
				}
				else
				{
					Toast.ShowError(sprocTuple.ReturnMsg);
				}
			}
		}
		catch (Exception)
		{
			Toast.ShowError("An invalid operation occurred during Registration Editing, contact your administrator");
		}
	}

} 
