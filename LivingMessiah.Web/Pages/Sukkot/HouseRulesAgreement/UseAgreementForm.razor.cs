﻿using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using LivingMessiah.Web.Pages.Sukkot.SuperUser;
using Microsoft.Extensions.Logging;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.HouseRulesAgreement;

public partial class UseAgreementForm
{
	[Inject] public ILogger<UseAgreementForm>? Logger { get; set; }
	[Inject] private IState<HRA_State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	private FluentValidationValidator? _fluentValidationValidator;

	protected void HandleValidSubmit()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(FormVM) + "!" + nameof(HandleValidSubmit)));
		Dispatcher!.Dispatch(new Add_Action(State!.Value.FormVM!, GetLocalTimeZone()));
		Dispatcher!.Dispatch(new ReSet_Action(new HouseRulesAgreement.FormVM(), HRA_FormState.Start));
		Dispatcher!.Dispatch(new SuperUser.MasterDetail.GetAll_Action());
	}

	private string GetLocalTimeZone()
	{
		return $"Time Zone: {TimeZoneInfo.Local}.";
	}

	protected void Reset_ButtonClick()
	{
		Logger!.LogDebug(string.Format("Event: {0} clicked"
			, nameof(UseAgreementForm) + "!" + nameof(Reset_ButtonClick)));
		Dispatcher!.Dispatch(new ReSet_Action(new HouseRulesAgreement.FormVM(), HRA_FormState.Start));
	}

}
