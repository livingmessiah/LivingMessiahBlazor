using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser;
using Microsoft.Extensions.Logging;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.HouseRulesAgreement;

public partial class Form
{
	[Inject] public ILogger<Form>? Logger { get; set; }
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	public FormVM VM { get; set; } = new FormVM();

	private FluentValidationValidator? _fluentValidationValidator;

	protected void HandleValidSubmit()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(HandleValidSubmit)));
		Dispatcher!.Dispatch(new Add_HRA_Action(VM.EMail, GetLocalTimeZone()));
		Dispatcher!.Dispatch(new Get_List_Action());

		/*
			Dispatcher!.Dispatch(new Submitting_Request_Action(State!.Value.FormVM!, State!.Value.FormMode!));
			Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(SuperUser.Constants.GetPageHeaderForIndexVM()));
		*/
	}

	void ShowAgreement()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(ShowAgreement)));
		Dispatcher!.Dispatch(new Set_ShowAgreementParagraph_Action(true));
		Dispatcher!.Dispatch(new Set_MessageState_Action(SuperUser.Enums.MessageState.Empty));


		
	}

	private string GetLocalTimeZone()
	{
		return $"Time Zone: {TimeZoneInfo.Local}.";
	}

}
