using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;

namespace LivingMessiah.Web.Pages.Admin.Dashboard
{
	public partial class Index
	{
		[Inject]
		NavigationManager NavManager { get; set; }

		[Inject]
		public ILogger<Index> Logger { get; set; }

		void ThrowException_ButtonClick()
		{
			/*
			for (int i = 0; i < 10; i++)
			{
				if (i == 5)
				{
					throw new Exception("This is our demo exception");
				}
				else
				{
					Logger.LogDebug($"The value of i is {i}");
				}
			}
			*/
			/**/
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
						Logger.LogDebug($"The value of i is {i}");
					}
				}

			}
			catch (Exception ex)
			{
				Logger.LogError(ex, $"We caught this exception inside {nameof(Index)}!{nameof(ThrowException_ButtonClick)}");
				NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error, forceLoad: true);
			}
			

		}
	}
}
