using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser;

public partial class AgreementButtons
{
	[Inject] public ILogger<AgreementButtons>? Logger { get; set; }
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	void DoNotAgree_ButtonClick()
	{
		Logger!.LogDebug(string.Format("Event: {0} clicked"
			, nameof(AgreementButtons) + "!" + nameof(DoNotAgree_ButtonClick)));
		Dispatcher!.Dispatch(new Set_ShowAgreementParagraph_Action(false));
		Dispatcher!.Dispatch(new Set_MessageState_Action(Enums.MessageState.HRA_DoNotAgree));
	}

	protected void Agree_ButtonClick()
	{
		Logger!.LogDebug(string.Format("Event: {0} clicked"
			, nameof(AgreementButtons) + "!" + nameof(Agree_ButtonClick)));
		Dispatcher!.Dispatch(new Set_ShowAgreementParagraph_Action(false));
		Dispatcher!.Dispatch(new Set_MessageState_Action(Enums.MessageState.HRA_Agree));
		//int id = 0;

	}



}
