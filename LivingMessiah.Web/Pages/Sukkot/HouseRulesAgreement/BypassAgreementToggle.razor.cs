using LivingMessiah.Web.Pages.Sukkot.SuperUser;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Pages.Sukkot.HouseRulesAgreement;

public partial class BypassAgreementToggle
{
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }
	[Inject] public ILogger<BypassAgreementToggle>? Logger { get; set; }


	private bool IsBypassAgreement => State!.Value.BypassAgreement;


	string Icon => IsBypassAgreement
		? "fas fa-toggle-on"
		: "fas fa-toggle-off";

	// There the same because the Toggle Icon indicates whether the thing is in use or not
	string Text => IsBypassAgreement
		? "Bypass HRA Process"
		: "Bypass HRA Process";

	string Title => IsBypassAgreement
		? "Currently the option to bypass the House Rules Agreement (HRA) process is ON"
		: "Currently the option to bypass the House Rules Agreement (HRA) process is OFF";

	void Toggle()
	{
		Logger!.LogDebug(string.Format("Inside {0}, BypassAgreement: {1}"
			, nameof(BypassAgreementToggle) + "!" + nameof(Toggle), State!.Value.BypassAgreement) );

		Dispatcher!.Dispatch(new ReSet_HRA_Action(new HouseRulesAgreement.FormVM(), HRA_FormState.Start));
		Dispatcher!.Dispatch(new Set_BypassAgreement_Action(!IsBypassAgreement));
	}

}
