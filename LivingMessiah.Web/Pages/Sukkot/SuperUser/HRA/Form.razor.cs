using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;

using Microsoft.Extensions.Logging;
using System;
using LivingMessiah.Web.Pages.Sukkot.SuperUser.Enums;
using ParentState = LivingMessiah.Web.Pages.Sukkot.SuperUser.Index;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.HRA;

public partial class Form
{
	[Inject] public ILogger<Form>? Logger { get; set; }
	[Inject] private IState<HRA_State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	private FluentValidationValidator? _fluentValidationValidator;

	void DoNotAgree_ButtonClick()
	{
		Logger!.LogDebug(string.Format("Event: {0} clicked", nameof(Form) + "!" + nameof(DoNotAgree_ButtonClick)));
		Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Index_Action(SuperUser.Constants.GetPageHeaderForIndexVM()));
		Dispatcher!.Dispatch(new Response_Message_Action(ResponseMessage.Warning, Constants.HRA.DidNotAgreeButton.Text));
	}

	protected void Agree_ButtonClick()
	{
		Logger!.LogDebug(string.Format("Event: {0} clicked"
			, nameof(Form) + "!" + nameof(Agree_ButtonClick)));
		Dispatcher!.Dispatch(new Add_Action(State!.Value.FormVM!, GetLocalTimeZone()));
		Dispatcher!.Dispatch(new SuperUser.MasterDetail.GetAll_Action());
	}

	
	private string GetLocalTimeZone()
	{
		return $"Time Zone: {TimeZoneInfo.Local}.";
	}
}
