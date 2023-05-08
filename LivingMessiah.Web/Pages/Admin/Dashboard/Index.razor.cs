using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;
using Blazored.Toast.Services;

namespace LivingMessiah.Web.Pages.Admin.Dashboard;

[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class Index
{
	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	void ThrowException_ButtonClick()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Index) + "!" + nameof(ThrowException_ButtonClick)));
		try
		{
			for (int i = 0; i < 10; i++)
			{
				if (i == 5)
				{
					throw new Exception("This is our demo exception");
				}
				else
				{
					Logger!.LogDebug($"The value of i is {i}");
				}
			}

		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "...Exception thrown");
			Toast!.ShowError("...Exception thrown");
		}
	}


}
