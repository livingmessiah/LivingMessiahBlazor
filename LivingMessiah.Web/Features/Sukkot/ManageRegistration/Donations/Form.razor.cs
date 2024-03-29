﻿using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Services;
using System.Threading.Tasks;
using System;

using ParentState = LivingMessiah.Web.Features.Sukkot.ManageRegistration.Index;

namespace LivingMessiah.Web.Features.Sukkot.ManageRegistration.Donations;

public partial class Form
{
	[Inject] public ILogger<Form>? Logger { get; set; }
	[Inject] private IState<DonationState>? DonationState { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }
	[Inject] public ISecurityClaimsService? SvcClaims { get; set; }

	private FluentValidationValidator? _fluentValidationValidator;

	private FormVM? VM => DonationState!.Value.FormVM;
	protected string? FullName;

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(OnInitialized)));

		string email = await SvcClaims!.GetEmail();
		if (String.IsNullOrEmpty(email)) email = "test@test.com";

		VM!.CreatedBy = email;
		VM!.CreateDate = DateTime.UtcNow;
		VM!.RegistrationId = DonationState!.Value.RegistrationId;
		FullName = DonationState!.Value.FullName;

		await base.OnInitializedAsync();
	}


	protected void HandleValidDonationSubmit()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(HandleValidDonationSubmit)));
		Dispatcher!.Dispatch(new Add_Action(DonationState!.Value.FormVM!));
		Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Index_Action(ManageRegistration.Constants.GetPageHeaderForIndexVM()));
	}

	void CancelActionHandler()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(CancelActionHandler)));
		Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Index_Action(ManageRegistration.Constants.GetPageHeaderForIndexVM()));
	}

}
