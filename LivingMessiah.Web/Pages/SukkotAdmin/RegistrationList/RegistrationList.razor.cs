using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Services;
using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SukkotApi.Domain;
using SukkotApi.Domain.Enums;
using Microsoft.AspNetCore.Components;
using System.Linq;
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

	public RegistrationSortEnum Sort { get; private set; }
	public List<vwRegistration> Registrations { get; set; }

	public RegistrationSortEnum RegistrationSort { get; set; } = RegistrationSortEnum.LastName;

	public int RecordCount { get; set; } = 0;

	protected override async Task OnInitializedAsync()
	{
		try
		{
			Logger.LogDebug(string.Format("Inside {0} , RegistrationSort:{1}"
					, nameof(RegistrationList) + "!" + nameof(OnInitializedAsync), RegistrationSort));
			Registrations = await svc.GetAll(RegistrationSort);
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
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error reading database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}
	}

	async Task Sort_ButtonClick(RegistrationSortEnum sort)
	{
		Logger.LogDebug(string.Format("Inside {0} , sort:{1}"
		, nameof(RegistrationList) + "!" + nameof(Sort_ButtonClick), sort));

		RegistrationSort = sort;
		RecordCount = 0;
		try
		{
			Registrations = await svc.GetAll(RegistrationSort);
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
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error reading database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}
		StateHasChanged();
	}

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

	void DetailsMealTicket_ButtonClick(int id)
	{
		NavManager.NavigateTo(Links.Sukkot.Meals.Index + "/" + id);
	}

	void EditMeals_ButtonClick(int id)
	{
		NavManager.NavigateTo(Links.Sukkot.Meals.Index + "/" + id);
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
