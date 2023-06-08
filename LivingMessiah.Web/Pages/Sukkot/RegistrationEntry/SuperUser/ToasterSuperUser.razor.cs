using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser;

public partial class ToasterSuperUser
{
	[Inject] public IToastService? Toast { get; set; }
	protected override void OnInitialized()
	{
		/*
		** To Much Chatter **
		SubscribeToAction<Get_List_Success_Action>(Get_List_Success_Toast); 
		SubscribeToAction<Set_PageHeader_For_Detail_Action>(Get_PageHeader_For_Detail_Toast);
		*/

		SubscribeToAction<Get_List_Warning_Action>(Get_List_Warning_Toast);
		SubscribeToAction<Get_List_Failure_Action>(Get_List_Failure_Toast);

		SubscribeToAction<Get_Item_Success_Action>(Get_Item_Success_Toast);
		SubscribeToAction<Get_Item_Warning_Action>(Get_Item_Warning_Toast);
		SubscribeToAction<Get_Item_Failure_Action>(Get_Item_Failure_Toast);

		SubscribeToAction<Get_DisplayItem_Success_Action>(Get_DisplayItem_Success_Toast);
		SubscribeToAction<Get_DisplayItem_Warning_Action>(Get_DisplayItem_Warning_Toast);
		SubscribeToAction<Get_DisplayItem_Failure_Action>(Get_DisplayItem_Failure_Toast);

		SubscribeToAction<Submitted_Response_Success_Action>(Submitted_Response_Success_Toast);

		SubscribeToAction<Response_Message_Action>(Response_Message_Toast);

		base.OnInitialized();
	}


	/*
	** To Much Chatter **
	private void Get_List_Success_Toast(Get_List_Success_Action action) => Toast!.ShowInfo($"Got list of {action.vwRegistrations.Count} records");
	private void Get_PageHeader_For_Detail_Toast(Set_PageHeader_For_Detail_Action action) => Toast!.ShowInfo($"PageHeader Members {action.ToString()} ");
	*/

	private void Get_List_Warning_Toast(Get_List_Warning_Action action) => Toast!.ShowWarning($"No records found");
	private void Get_List_Failure_Toast(Get_List_Failure_Action action) => Toast!.ShowError($"{action.ErrorMessage}");

	private void Get_Item_Success_Toast(Get_Item_Success_Action action) => Toast!.ShowInfo($"Got {action.FormVM!.FamilyName!}");
	private void Get_Item_Warning_Toast(Get_Item_Warning_Action action) => Toast!.ShowWarning($"{action.WarningMessage}");
	private void Get_Item_Failure_Toast(Get_Item_Failure_Action action) => Toast!.ShowError($"{action.ErrorMessage}");


	private void Get_DisplayItem_Success_Toast(Get_DisplayItem_Success_Action action) => Toast!.ShowInfo($"Got {action.DisplayVM!.FullName(false)}");
	private void Get_DisplayItem_Warning_Toast(Get_DisplayItem_Warning_Action action) => Toast!.ShowWarning($"{action.WarningMessage}");
	private void Get_DisplayItem_Failure_Toast(Get_DisplayItem_Failure_Action action) => Toast!.ShowError($"{action.ErrorMessage}");

	private void Submitted_Response_Success_Toast(Submitted_Response_Success_Action action) 
		=> Toast!.ShowSuccess($"Submit {action.SuccessMessage}");

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

			//default:
			//	break;
		}
	}

}


