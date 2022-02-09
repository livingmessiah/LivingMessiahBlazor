using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using LivingMessiah.Web.Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace LivingMessiah.Web.Pages.Account;

[Authorize]
public partial class Claims
{
		[Inject]
		public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

		public string EmailAddress { get; set; }
		public string Name { get; set; }
		public string ProfileImage { get; set; }
		public string Country { get; set; }
		public bool IsAdmin { get; set; }
		public string Role { get; set; }

		private string _authMessage;
		private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();

		protected List<Claim> ClaimList;

		protected override async Task OnInitializedAsync()
		{
				base.OnInitialized();
				var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
				var user = authState.User;

				Name = user.Identity.Name;
				EmailAddress = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
				ProfileImage = user.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;
				Country = user.Claims.FirstOrDefault(c => c.Type == "country")?.Value;
				Role = user.Claims.FirstOrDefault(c => c.Type == "https://schemas.livingmessiah.com/roles")?.Value;
		}

		private async Task GetClaimsPrincipalData()
		{
				var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
				var user = authState.User;

				if (user.Identity.IsAuthenticated)
				{
						_authMessage = $"{user.Identity.Name} is authenticated.";
						_claims = user.Claims;
						//_surnameMessage = $"Surname: {user.FindFirst(c => c.Type == ClaimTypes.Surname)?.Value}";
						ClaimList = _claims.ToList();
				}
				else
				{
						_authMessage = "The user is NOT authenticated.";
				}


		}


}
