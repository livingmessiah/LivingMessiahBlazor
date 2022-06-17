using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Services;
using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SukkotApi.Domain;
using Microsoft.AspNetCore.Components;
using SukkotApi.Domain.Registrations.Enums;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationList;

[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class RegistrationList
{
	[Inject]
	public ILogger<RegistrationList> Logger { get; set; }

	[Inject]
	public ISukkotAdminService svc { get; set; }

	[Inject]
	NavigationManager NavManager { get; set; }

	public List<vwRegistration> Registrations { get; set; }

	private bool IsCurrentSortAscending = true;
	private RegistrationSortEnum CurrentRegistrationSortEnum = RegistrationSortEnum.LastName;


	public int RecordCount { get; set; } = 0;

	protected override async Task OnInitializedAsync()
	{
		await PopulateRegistrationList(RegistrationSortEnum.LastName, isAscending: IsCurrentSortAscending);
	}

	async Task SortClicked(RegistrationSortEnum registrationSortEnum)
	{
		Logger.LogDebug(string.Format("...{0} registrationSortEnum:{1}"
			, nameof(RegistrationList) + "!" + nameof(SortClicked), registrationSortEnum));

		if (registrationSortEnum != CurrentRegistrationSortEnum)
		{
			CurrentRegistrationSortEnum = registrationSortEnum;
			IsCurrentSortAscending = true;
		}
		else  
		{
			IsCurrentSortAscending = !IsCurrentSortAscending;
		}
		await PopulateRegistrationList(CurrentRegistrationSortEnum, isAscending: IsCurrentSortAscending);
	}

	private async Task PopulateRegistrationList(RegistrationSortEnum registrationSortEnum, bool isAscending)
	{
		try
		{
			Logger.LogDebug(string.Format("Inside {0} , registrationSortEnum: {1}, isAscending: {2}"
					, nameof(RegistrationList) + "!" + nameof(PopulateRegistrationList), registrationSortEnum, isAscending));
			
			Registrations = await svc.GetAll(registrationSortEnum, isAscending);
			
			if (Registrations is not null)
			{
				RecordCount = Registrations.Count;
			}
			else
			{
				DatabaseWarning = true;
				DatabaseWarningMsg = $"{nameof(Registrations)} is null";
			}
		}
		catch (InvalidOperationException invalidOperationException)
		{
			DatabaseError = true;
			DatabaseErrorMsg = invalidOperationException.Message;
		}
	}

	private string GetSortStyle(RegistrationSortEnum registrationSortEnum)
	{
		if (CurrentRegistrationSortEnum != registrationSortEnum)
		{
			return string.Empty;
		}
		if (IsCurrentSortAscending)
		{
			return "▲";
		}
		else
		{
			return "▼";
		}
	}

	private string GetTitle()
	{
		return $"Sorted by {CurrentRegistrationSortEnum.ToString()} {(IsCurrentSortAscending ? "" : "(descending)")}";
	}

	#region Navigation Button Clicks
	void Add_ButtonClick()
	{
		NavManager.NavigateTo(Links.Sukkot.CreateEdit + "/");
	}

	void DeleteConfirmation_ButtonClick(int id)
	{
		NavManager.NavigateTo(Links.Sukkot.DeleteConfirmation + "/" + id);
	}

	void Payment_ButtonClick(int id)
	{
		NavManager.NavigateTo(Links.Sukkot.Links2.Payment + "/" + id);
	}

	void Details_ButtonClick(int id)
	{
		NavManager.NavigateTo(Links.Sukkot.Details + "/" + id + "/False");
	}

	void Edit_ButtonClick(int id)
	{
		NavManager.NavigateTo(Links.Sukkot.CreateEdit + "/" + id);
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
}
