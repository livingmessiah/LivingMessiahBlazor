﻿using Blazored.Toast.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using Page = LivingMessiah.Web.Links.Wirecast;

namespace LivingMessiah.Web.Features.Admin.Wirecast;

[AllowAnonymous]
public partial class Display
{
	[Inject] public Data.IRepository? db { get; set; }
	[Inject] public ILogger<Display>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	public WirecastQuery? VM { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside Page: {0}, Class!Method: {1}"
		, Page.Index, nameof(Display) + "!" + nameof(OnInitializedAsync)));

		try
		{
			VM = await db!.GetCurrentWirecast();
			if (VM == null)
			{
				string s = $"Wirecast is null after calling {nameof(db.GetCurrentWirecast)}";
				Logger!.LogWarning(string.Format("...{0}, Sql:{1}", s, db.BaseSqlDump));
				Toast!.ShowWarning($"...{s}");
			}

		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}"
				, nameof(Display) + "!" + nameof(OnInitializedAsync)));
			Toast!.ShowError("An invalid operation occurred reading database, contact your administrator");
		}
	}
}
