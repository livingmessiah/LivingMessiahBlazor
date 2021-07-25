﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using LivingMessiah.Web.Infrastructure;
using SukkotApi.Data;
using SukkotApi.Domain;
using SukkotApi.Domain.Enums;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace LivingMessiah.Web.Pages.Sukkot
{
	public partial class RegistrationShell
	{
		[Inject]
		public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
		
		[Inject]
		public ISukkotRepository db { get; set; }

		[Inject]
		public ILogger<RegistrationShell> Logger { get; set; }

		[Inject]
		NavigationManager NavManager { get; set; }

		public vwRegistrationShell vwRegistrationShell { get; set; } = new vwRegistrationShell();

		public StatusFlagEnum StatusFlagEnum { get; set; }
		public int sf { get; set; }
		public int Id { get; set; }
		public int StatusId { get; set; } = 0;

		public ClaimsPrincipal User { get; set; }
		public string EmailAddress { get; set; }
		public string UserName { get; set; }

		public string Title { get; set; }

		public bool IsMealsAvailable { get; set; } = false;

		protected override async Task OnInitializedAsync()
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			User = authState.User;
			LoadUserData();

			if (StatusFlagEnum.HasFlag(StatusFlagEnum.EmailConfirmation))
			{
				try
				{
					Logger.LogDebug($"Inside {nameof(RegistrationShell)}!{nameof(OnInitializedAsync)} calling {nameof(db.ByEmail)} with EmailAddress:{EmailAddress}");
					vwRegistrationShell = await db.ByEmail(EmailAddress);

					if (vwRegistrationShell != null)
					{
						Id = vwRegistrationShell.Id;
						StatusId = vwRegistrationShell.StatusId;
						FinalizeStatusFlag(vwRegistrationShell.StatusId);
					}
					else
					{
						//Logger.LogDebug($"vwRegistrationShell is null, navigating to {nameof(Links.Sukkot.CreateEdit)}");
						//NavManager.NavigateTo(Links.Sukkot.CreateEdit);
						StatusId = 1;
					}

				}
				catch (Exception ex)
				{
					Logger.LogError(ex, $"EmailAddress={EmailAddress}");
					NavManager.NavigateTo(Links.Home.Error);
				}
			}
			sf = (int)StatusFlagEnum;
		}

		private void LoadUserData()
		{
			if (User.Verified())
			{
				StatusFlagEnum = StatusFlagEnum | StatusFlagEnum.EmailConfirmation;
			}

			UserName = User.GetUserName();
			EmailAddress = User.GetUserEmail();

			if (string.IsNullOrEmpty(UserName) | string.IsNullOrEmpty(EmailAddress))
			{
				Title = "Registration Steps";
			}
			else
			{
				Title = $"Registration for {EmailAddress}";
			}
		}

		private void FinalizeStatusFlag(int statusId)
		{
			StatusFlagEnum = StatusFlagEnum | StatusFlagEnum.RegistrationFormCompleted;
			//if (MealCount > 0)
			if (vwRegistrationShell.MealCount > 0)
			{
				StatusFlagEnum = StatusFlagEnum | StatusFlagEnum.MealsFormCompleted;
			}

			if (statusId == (int)Status.FullyPaid)
			{
				StatusFlagEnum = StatusFlagEnum | StatusFlagEnum.MealsFormCompleted | StatusFlagEnum.FullyPaid;
			}
			else
			{
				if (statusId == (int)Status.PartiallyPaid)
				{
					StatusFlagEnum = StatusFlagEnum | StatusFlagEnum.MealsFormCompleted | StatusFlagEnum.PartiallyPaid;
				}
			}
			
		}

	}
}
