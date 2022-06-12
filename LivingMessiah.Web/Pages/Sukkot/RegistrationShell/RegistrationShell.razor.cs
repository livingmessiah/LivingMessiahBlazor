﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using LivingMessiah.Web.Infrastructure;
using SukkotApi.Data;
using SukkotApi.Domain;
using SukkotApi.Domain.Enums;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationShell;

public partial class RegistrationShell
{
	[Inject]
	public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

	[Inject]
	public ISukkotRepository db { get; set; }

	[Inject]
	public ILogger<RegistrationShell> Logger { get; set; }

	public vwRegistrationShell vwRegistrationShell { get; set; } = new vwRegistrationShell();

	public StatusFlagEnum StatusFlagEnum { get; set; }

	public ClaimsPrincipal User { get; set; }
	public string EmailAddress { get; set; }
	public string UserName { get; set; }

	public string Title { get; set; }

	private vwRegistrationShell GetDefaultModel()
	{
		return new vwRegistrationShell
		{
			StatusId = (int)StatusFlagEnum.EmailConfirmation,
			Id = 0,
			CampCost = 0,
			FamilyName = "",
			MealCost = 0,
			MealCount = 0,
			TotalDonation = 0
		};
	}

	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(string.Format("Inside {0}"
			, nameof(RegistrationShell) + "!" + nameof(OnInitializedAsync)));
		try
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			User = authState.User;
			LoadUserData();
			if (StatusFlagEnum.HasFlag(StatusFlagEnum.EmailConfirmation))
			{
				await GetRegistrationByEmail();
			}
		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"...Error authenticating user; EmailAddress={EmailAddress}";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}
	}

	private async Task GetRegistrationByEmail() 
	{
		Logger.LogDebug(string.Format("Inside {0}; EmailAddress:{1}"
		, nameof(RegistrationShell) + "!" + nameof(GetRegistrationByEmail), EmailAddress));
		try
		{
			vwRegistrationShell = await db.ByEmail(EmailAddress);
			if (vwRegistrationShell != null)
			{
				FinalizeStatusFlag(vwRegistrationShell.StatusId);
			}
			else
			{
				vwRegistrationShell = GetDefaultModel();
			}

		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"...Error reading database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}

	}

	private void LoadUserData()
	{
		if (User.Verified())
		{
			StatusFlagEnum = StatusFlagEnum | StatusFlagEnum.EmailConfirmation;
		}

		UserName = User.GetUserName();
		EmailAddress = User.GetUserEmail();

		if (string.IsNullOrEmpty(UserName) | string.IsNullOrEmpty(EmailAddress))
		{
			Title = "Registration Steps";
		}
		else
		{
			Title = $"Registration for {EmailAddress}";
		}
	}

	private void FinalizeStatusFlag(int statusId)
	{
		StatusFlagEnum = StatusFlagEnum | StatusFlagEnum.RegistrationFormCompleted;

		if (vwRegistrationShell.MealCount > 0)
		{
			StatusFlagEnum = StatusFlagEnum | StatusFlagEnum.MealsFormCompleted;
		}

		if (statusId == (int)StatusEnum.FullyPaid)
		{
			StatusFlagEnum = StatusFlagEnum | StatusFlagEnum.MealsFormCompleted | StatusFlagEnum.FullyPaid;
		}
		else
		{
			if (statusId == (int)StatusEnum.PartiallyPaid)
			{
				StatusFlagEnum = StatusFlagEnum | StatusFlagEnum.MealsFormCompleted | StatusFlagEnum.PartiallyPaid;
			}
		}

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
