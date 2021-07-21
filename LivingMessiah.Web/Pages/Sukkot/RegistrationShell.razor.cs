using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sukkot.Web.Service;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEnums;
using LivingMessiah.Web.Infrastructure;
using SukkotApi.Data;
using SukkotApi.Domain;
using SukkotApi.Domain.Enums;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

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


		public StatusFlagEnum StatusFlagEnum { get; set; }
		public int sf { get; set; }
		public int Id { get; set; }
		public int StatusId { get; set; }

		public ClaimsPrincipal User { get; set; }
		public string EmailAddress { get; set; }
		public string UserName { get; set; }
		//public bool Verified { get; set; }
		//public string Role { get; set; }

		public string Title { get; set; }
		

		public bool IsMealsAvailable { get; set; } = false;

		public int MealCount { get; set; }
		public decimal MealCost { get; set; }
		public decimal CampCost { get; set; }
		public decimal TotalCost { get; set; }
		public decimal RemainingCost { get; set; }
		public decimal TotalDonation { get; set; }

		
		private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();

		protected override async Task OnInitializedAsync()
		{
			base.OnInitialized();
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			User = authState.User;

			//if (User.Identity.IsAuthenticated)
			//{
			//	Verified = true;
			//	_claims = User.Claims;
			//}
			//else
			//{
			//	Verified = false;
			//}

			//Name = User.Identity.Name;
			//EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
			//Role = User.Claims.FirstOrDefault(c => c.Type == "https://schemas.livingmessiah.com/roles")?.Value;

			LoadUserData();

			if (StatusFlagEnum.HasFlag(StatusFlagEnum.EmailConfirmation))
			{
				vwRegistrationShell vw;
				try
				{
					Logger.LogDebug($"Inside {nameof(RegistrationShell)}!{nameof(OnInitializedAsync)}, calling {nameof(db.ByEmail)}");
					vw = await db.ByEmail(EmailAddress);

					if (vw != null)
					{
						Id = vw.Id;
						StatusId = vw.StatusId;
						MealCount = vw.MealCount;
						MealCost = vw.MealCost;
						CampCost = vw.CampCost;
						TotalCost = vw.TotalCost;
						RemainingCost = vw.RemainingCost;
						TotalDonation = vw.TotalDonation;
						FinalizeStatusFlag(vw.StatusId);
					}
				}
				catch (Exception ex)
				{
					Logger.LogError(ex, $"EmailAddress={EmailAddress}");
				}
			}

		}

		private void LoadUserData()
		{
			if (User.Verified()) //Verified
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
			if (MealCount > 0)
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
			sf = (int)StatusFlagEnum;
		}

	}
}
