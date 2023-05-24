using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor.DropDowns;
using System.Collections.Generic;
using Blazored.Toast.Services;
using Blazored.FluentValidation;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser;

public partial class EntryForm
{
	[Inject] public IService? svc { get; set; }
	[Inject] public IRepository? db { get; set; }
	[Inject] public ILogger<EntryForm>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	private bool ShowEditForm = false;

	public ViewModel VM { get; set; } = new ViewModel();

	private FluentValidationValidator? _fluentValidationValidator;
	public Sukkot.Enums.DateRangeType DateRangeAttendance { get; set; } = Sukkot.Enums.DateRangeType.Attendance;
	public List<RegistrationLookup>? RegistrationLookupList { get; set; }

	private string Title = "";
	private string msg = "";

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(EntryForm) + "!" + nameof(OnInitialized)));

		Title = "Registration Edit ";
		try
		{
			RegistrationLookupList = await db!.PopulateRegistrationLookup();
		}
		catch (Exception)
		{
			Toast!.ShowError("Error getting registration lookup from database, contact your administrator");
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
		Logger!.LogDebug(string.Format("Inside {0}; registrationId:{1}"
			, nameof(EntryForm) + "!" + nameof(GetRegistration), registrationId));
		try
		{
			VM = await svc!.GetById(registrationId);
			if (VM == null)
			{
				msg = $"Registration not found; registrationId:{registrationId}";
				Toast!.ShowWarning(msg);
				Logger!.LogWarning("..." + msg);
			}
			else
			{
				Title = "Registration Edit " + registrationId;
				ShowEditForm = true;
			}
		}
		catch (Exception ex)
		{
			Toast!.ShowError("Error reading database");
			Logger!.LogError(ex, $"...Error reading database");
		}
		StateHasChanged();  // https://stackoverflow.com/questions/56436577/blazor-form-submit-needs-two-clicks-to-refresh-view
	}

	#endregion


	protected async Task HandleValidSubmit()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(EntryForm) + "!" + nameof(HandleValidSubmit)));
		try
		{
			var sprocTuple = await svc!.Update(VM);
			if (sprocTuple.RowsAffected != 0)
			{
				msg = $"{sprocTuple.ReturnMsg}";
				VM = await svc.GetById(VM.Id); //ToDo: do I need to refresh the data?
				Toast!.ShowInfo(sprocTuple.ReturnMsg);
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
			Toast!.ShowError("An invalid operation occurred during Registration Editing, contact your administrator");
		}
	}

} 
