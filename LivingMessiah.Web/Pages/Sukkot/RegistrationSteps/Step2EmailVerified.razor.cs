﻿using Microsoft.AspNetCore.Components;
using Link = LivingMessiah.Web.Links.Account;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class Step2EmailVerified
{
	[Parameter, EditorRequired]
	public Enums.StatusFlag StatusFlag { get; set; }

	[Inject]
	NavigationManager NavigationManager { get; set; }

	void RedirectToLoginClick(string returnUrl)
	{
		NavigationManager.NavigateTo($"{Link.Login}?returnUrl={returnUrl}", true);
	}

}