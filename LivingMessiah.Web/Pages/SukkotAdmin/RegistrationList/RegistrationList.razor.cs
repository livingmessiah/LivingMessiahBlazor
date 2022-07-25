using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;

using LivingMessiah.Web.Pages.Sukkot.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationList;

[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class RegistrationList
{
	[Inject]
	public ILogger<RegistrationList> Logger { get; set; }

	[Inject]
	public ISukkotAdminService svc { get; set; }

	public List<vwRegistration> Registrations { get; set; }

	private bool IsCurrentSortAscending = true;
	private EnumsOld.RegistrationSortEnum CurrentRegistrationSortEnum = EnumsOld.RegistrationSortEnum.LastName;

	public int RecordCount { get; set; } = 0;

	protected override async Task OnInitializedAsync()
	{
		await PopulateRegistrationList(EnumsOld.RegistrationSortEnum.LastName, isAscending: IsCurrentSortAscending);
	}

	async Task SortClicked(EnumsOld.RegistrationSortEnum registrationSortEnum)
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

	private async Task PopulateRegistrationList(EnumsOld.RegistrationSortEnum registrationSortEnum, bool isAscending)
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

	private string GetSortStyle(EnumsOld.RegistrationSortEnum registrationSortEnum)
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
