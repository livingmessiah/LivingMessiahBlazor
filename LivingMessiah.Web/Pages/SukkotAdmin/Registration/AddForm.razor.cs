using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Services; // using service, don't need .Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Enums;
using static LivingMessiah.Web.Pages.Sukkot.Constants.SqlServer;

using Syncfusion.Blazor.DropDowns;
using System.Collections.Generic;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration;

public partial class AddForm
{
	[Inject]
	public IRegistrationService svc { get; set; }

	[Inject]
	public IRegistrationRepository db { get; set; }

	[Inject]
	public ILogger<AddForm> Logger { get; set; }

	public RegistrationVM RegistrationVM { get; set; } = new RegistrationVM();

	//ToDo this should come from the Sukkot.Constants and saved in cache		
	public Sukkot.DateRangeLocal DateRangeAttendance { get; set; } = Sukkot.DateRangeLocal.FromEnum(Sukkot.DateRangeEnum.AttendanceDays);

	protected bool LoadFailed;
	//protected bool CanEditCampType => Registration.LocationEnum == LocationEnum.WildernessRanch;
	protected bool CanEditCampType = false;

	[Parameter]
	public bool IsEdit { get; set; } = false;

	private bool ShowEditForm = false;

	public List<RegistrationLookup> RegistrationLookupList { get; set; }

	private string Title = "";

	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug($"Inside {nameof(AddForm)}!{nameof(OnInitializedAsync)}; {nameof(IsEdit)}: {IsEdit}");
		InitializeErrorHandling();

		if (!IsEdit)
		{
			Title = "Registration Add";
			RegistrationVM = new RegistrationVM();
		}
		else
		{
			Title = "Registration Edit ";
			try
			{
				RegistrationLookupList = await db.PopulateRegistrationLookup();
			}
			catch (Exception)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error getting registration lookup from database";
			}
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
		Logger.LogDebug($"Inside {nameof(AddForm)}!{nameof(GetRegistration)}; registrationId:{registrationId}");
		try
		{
			DatabaseWarning = false;
			DatabaseWarningMsg = "";
			RegistrationVM = await svc.GetById(registrationId);
			if (RegistrationVM == null)
			{
				DatabaseWarning = true;
				DatabaseWarningMsg = $"Registration NOT FOUND; registrationId:{registrationId}";
			}
			else
			{
				Title = "Registration Edit " + registrationId;
				ShowEditForm = true;
			}
		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error reading database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}
		StateHasChanged();  // https://stackoverflow.com/questions/56436577/blazor-form-submit-needs-two-clicks-to-refresh-view
	}

	#endregion

	#region Submit
	private const string CreateErrMsg = "Error updating to database";
	private const string UpdateErrMsg = "Error adding to database";

	protected async Task HandleValidSubmit()
	{
		InitializeErrorHandling();
		Logger.LogDebug($"Inside {nameof(HandleValidSubmit)}, {nameof(IsEdit)}: {IsEdit}");

		if (IsEdit)
		{
			await Update();
		}
		else
		{
			await Create();
		}

	}

	private async Task Create()
	{
		//RegistrationVM.StatusSmartEnum = BaseStatusSmartEnum.EmailConfirmation;

		try
		{
			RegistrationVM.Id = 0;
			RegistrationVM.StatusSmartEnum = BaseStatusSmartEnum.EmailConfirmation;

			var sprocTuple = await svc.Create(RegistrationVM);
			if (sprocTuple.Item1 != 0)
			{
				DatabaseInformation = true;
				DatabaseInformationMsg = $"{sprocTuple.Item3}";
				RegistrationVM = new RegistrationVM();
			}
			else
			{
				if (sprocTuple.Item2 == ReturnValueViolationInUniqueIndex)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = sprocTuple.Item3;
				}
				else
				{
					DatabaseError = true;
					DatabaseErrorMsg = sprocTuple.Item3;
				}
			}

		}
		catch (Exception)
		{
			DatabaseError = true;
			DatabaseErrorMsg = CreateErrMsg;
		}

	}

	private async Task Update()
	{
		//RegistrationVM.StatusSmartEnum = BaseStatusSmartEnum.EmailConfirmation;

		try
		{
			//var UpdateSprocTuple = new Tuple<int, int, string>(RowsAffected, ReturnValue, ReturnMsg);


			/*	*/
			var sprocTuple = await svc.Update(RegistrationVM);
			if (sprocTuple.Item1 != 0)
			{
				DatabaseInformation = true;
				DatabaseInformationMsg = $"{sprocTuple.Item3}";
				RegistrationVM = await svc.GetById(RegistrationVM.Id); //ToDo: do I need to refresh the data?
			}
			else
			{
				if (sprocTuple.Item2 == ReturnValueViolationInUniqueIndex)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = sprocTuple.Item3;
				}
				else
				{
					DatabaseError = true;
					DatabaseErrorMsg = sprocTuple.Item3;
				}
			}


		}
		catch (Exception)
		{
			DatabaseError = true;
			DatabaseErrorMsg = UpdateErrMsg;
		}
	}
	#endregion

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


} // class 
