using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using LivingMessiah.Web.Pages.Sukkot.SuperUser;
using Microsoft.Extensions.Logging;
using System;


namespace LivingMessiah.Web.Pages.Sukkot.HouseRulesAgreement;

public partial class SuperUserAgreementForm
{
	[Inject] public ILogger<SuperUserAgreementForm>? Logger { get; set; }
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	private FluentValidationValidator? _fluentValidationValidator;

	protected void HandleValidSubmit()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(SuperUserAgreementForm) + "!" + nameof(HandleValidSubmit)));
		Dispatcher!.Dispatch(new Add_HRA_Action(State!.Value.HRA_FormVM!, GetLocalTimeZone()));
		Dispatcher!.Dispatch(new ReSet_HRA_Action(new HouseRulesAgreement.FormVM(), HRA_FormState.Start));
		Dispatcher!.Dispatch(new Get_List_Action());
	}

	private string GetLocalTimeZone()
	{
		return $"Time Zone: {TimeZoneInfo.Local}.";
	}

}
