﻿using Fluxor;
using LivingMessiah.Web.Pages.Sukkot.SuperUser;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Pages.Sukkot.HouseRulesAgreement;

public partial class AgreementButtons
{
	[Inject] public ILogger<AgreementButtons>? Logger { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	void DoNotAgree_ButtonClick()
	{
		Logger!.LogDebug(string.Format("Event: {0} clicked"
			, nameof(AgreementButtons) + "!" + nameof(DoNotAgree_ButtonClick)));
		Dispatcher!.Dispatch(new Form_Prep_Action(HRA_FormState.DidNotAgree));
	}

	protected void Agree_ButtonClick()
	{
		Logger!.LogDebug(string.Format("Event: {0} clicked"
			, nameof(AgreementButtons) + "!" + nameof(Agree_ButtonClick)));
		Dispatcher!.Dispatch(new Form_Prep_Action(HRA_FormState.Agreed));
	}

}
