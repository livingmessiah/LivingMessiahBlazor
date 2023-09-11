using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Features.SpecialEvents;

public partial class ToasterSpecialEvents
{
	[Inject] public IToastService? Toast { get; set; }
	protected override void OnInitialized()
	{
		SubscribeToAction<Response_Message_Action>(Response_Message_Toast);
		base.OnInitialized();
	}

	private void Response_Message_Toast(Response_Message_Action action)
	{
		switch (action.MessageType)
		{
			case Enums.ResponseMessage.Success:
				Toast!.ShowSuccess(action.Message);
				break;

			case Enums.ResponseMessage.Warning:
				Toast!.ShowWarning(action.Message);
				break;

			case Enums.ResponseMessage.Failure:
				Toast!.ShowError(action.Message);
				break;

			case Enums.ResponseMessage.Info:
				Toast!.ShowInfo(action.Message);
				break;
		}
	}
}
