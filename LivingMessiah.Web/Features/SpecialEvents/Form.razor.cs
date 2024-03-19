﻿using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using Microsoft.Extensions.Logging;
using Page = LivingMessiah.Web.Links.SpecialEvents;

namespace LivingMessiah.Web.Features.SpecialEvents;

public partial class Form
{
	[Inject] public ILogger<Form>? Logger { get; set; }
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	private FormVM? VM => State!.Value.FormVM;
	private FluentValidationValidator? _fluentValidationValidator;

	readonly string inside = $"page {Page.Index}; class: {nameof(Form)}";

	protected override void OnInitialized()
	{
		base.OnInitialized();
		if (VM is not null)
		{
			if (VM.EventDate == System.DateTime.MinValue)VM.EventDate = System.DateTime.Now.AddDays(35);
			if (VM.ShowBeginDate is null) VM.ShowBeginDate = System.DateTime.Now.AddDays(25);
			if (VM.ShowEndDate is null)	VM.ShowEndDate = System.DateTime.Now.AddDays(36);
		}
	}

	protected void HandleValidSubmit()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(HandleValidSubmit)));
		Dispatcher!.Dispatch(new Submitting_Request_Action(State!.Value.FormVM!, State!.Value.FormMode!));
		Dispatcher!.Dispatch(new Get_List_Action());
		Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(Constants.GetPageHeaderForIndexVM()));
	}

	void CancelActionHandler()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(CancelActionHandler)));
		Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(Constants.GetPageHeaderForIndexVM()));
	}


}

