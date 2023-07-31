using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using LivingMessiah.Web.Pages.Sukkot.SuperUser;
using Microsoft.Extensions.Logging;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.HouseRulesAgreement;

public partial class SuperUserAgreementForm
{
	[Inject] public ILogger<SuperUserAgreementForm>? Logger { get; set; }
	[Inject] private IState<HRA_State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	private FluentValidationValidator? _fluentValidationValidator;

	protected void HandleValidSubmit()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(SuperUserAgreementForm) + "!" + nameof(HandleValidSubmit)));
		Dispatcher!.Dispatch(new Add_Action(State!.Value.FormVM!, GetLocalTimeZone()));
		Dispatcher!.Dispatch(new ReSet_Action(new HouseRulesAgreement.FormVM(), HRA_FormState.Start));
		Dispatcher!.Dispatch(new SuperUser.MasterDetail.GetAll_Action());
	}

	private string GetLocalTimeZone()
	{
		return $"Time Zone: {TimeZoneInfo.Local}.";
	}

}
