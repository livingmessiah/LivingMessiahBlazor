﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

using Blazored.Toast.Services;
using Page = LivingMessiah.Web.Features.Parasha.Enums.ParashaLinks;

using Microsoft.Extensions.Caching.Memory;
using LivingMessiah.Web.Features.Parasha.Services;

namespace LivingMessiah.Web.Features.Parasha;

public partial class Index
{
	[Inject] private IParashaService? Service { get; set; }
	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	protected CurrentParasha? CurrentParasha;

	protected bool TurnSpinnerOff = false;

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside Page: {0}, Class!Method: {1}", Page.Index, nameof(Index) + "!" + nameof(OnInitializedAsync)));

		try
		{
			CurrentParasha = await Service!.GetCurrentParasha();

			if (CurrentParasha is null || !String.IsNullOrEmpty(Service.UserInterfaceMessage))
			{
				Toast!.ShowWarning(Service.UserInterfaceMessage);
			}
		}
		catch (InvalidOperationException invalidOperationException)
		{
			Toast!.ShowError(invalidOperationException.Message);
		}
		finally
		{
			TurnSpinnerOff = true;
		}
	}

}
