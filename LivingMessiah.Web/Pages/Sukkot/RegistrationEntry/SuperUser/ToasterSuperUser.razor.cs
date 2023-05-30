using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser;

public partial class ToasterSuperUser
{
	[Inject] public IToastService? Toast { get; set; }
	protected override void OnInitialized()
	{
		SubscribeToAction<Get_List_Success_Action>(Get_List_Success_Toast); // Too much chatter
		SubscribeToAction<Get_List_Warning_Action>(Get_List_Warning_Toast);
		SubscribeToAction<Get_List_Failure_Action>(Get_List_Failure_Toast);

		SubscribeToAction<Get_Item_Success_Action>(Get_Item_Success_Toast);
		SubscribeToAction<Get_Item_Warning_Action>(Get_Item_Warning_Toast);
		SubscribeToAction<Get_Item_Failure_Action>(Get_Item_Failure_Toast);

		SubscribeToAction<Get_DisplayItem_Success_Action>(Get_DisplayItem_Success_Toast);
		SubscribeToAction<Get_DisplayItem_Warning_Action>(Get_DisplayItem_Warning_Toast);
		SubscribeToAction<Get_DisplayItem_Failure_Action>(Get_DisplayItem_Failure_Toast);

		base.OnInitialized();
	}

	//private void SubscribeToAction<T>(Action<T> get_List_Warning_Toast)
	//{
	//	throw new NotImplementedException();
	//}

	private void Get_List_Success_Toast(Get_List_Success_Action action) => Toast!.ShowInfo($"Got list of {action.vwRegistrations.Count} records");
	private void Get_List_Warning_Toast(Get_List_Warning_Action action) => Toast!.ShowWarning($"No records found");
	private void Get_List_Failure_Toast(Get_List_Failure_Action action) => Toast!.ShowError($"{action.ErrorMessage}");

	private void Get_Item_Success_Toast(Get_Item_Success_Action action) => Toast!.ShowInfo($"Got {action.FormVM!.FamilyName!}");
	private void Get_Item_Warning_Toast(Get_Item_Warning_Action action) => Toast!.ShowWarning($"{action.WarningMessage}");
	private void Get_Item_Failure_Toast(Get_Item_Failure_Action action) => Toast!.ShowError($"{action.ErrorMessage}");


	private void Get_DisplayItem_Success_Toast(Get_DisplayItem_Success_Action action) => Toast!.ShowInfo($"Got {action.DisplayVM!.FullName(false)}");
	private void Get_DisplayItem_Warning_Toast(Get_DisplayItem_Warning_Action action) => Toast!.ShowWarning($"{action.WarningMessage}");
	private void Get_DisplayItem_Failure_Toast(Get_DisplayItem_Failure_Action action) => Toast!.ShowError($"{action.ErrorMessage}");

}
