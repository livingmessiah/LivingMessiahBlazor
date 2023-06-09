﻿using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser;

public partial class ToasterSuperUser
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

			//default:
			//	break;
		}
	}

}


