using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;

namespace LivingMessiah.Web.Pages.Admin.Dashboard
{
	[Authorize(Roles = Roles.AdminOrSukkot)]
	public partial class Index
	{
		[Inject]
		NavigationManager NavManager { get; set; }

		[Inject]
		public ILogger<Index> Logger { get; set; }

		void ThrowException_ButtonClick()
		{
			Logger.LogDebug(string.Format("Inside {0}", nameof(Index) + "!" + nameof(ThrowException_ButtonClick)));
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
				DatabaseError = true;
				DatabaseErrorMsg = $"We caught this exception inside {nameof(Index)}!{nameof(ThrowException_ButtonClick)}";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
		}

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }

	}
}
