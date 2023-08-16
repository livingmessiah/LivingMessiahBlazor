using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;

using LivingMessiah.Web.Infrastructure;
//using Microsoft.Extensions.Logging;
//using Page = LivingMessiah.Web.Links.Account.Profile;


namespace LivingMessiah.Web.Pages.Account;

public partial class Claims
{
	[Inject] public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
	[Inject] public IToastService? Toast { get; set; }
	//[Inject] public ILogger<Claims>? Logger { get; set; }

	public string? EmailAddress { get; set; }
	public string? Name { get; set; }
	public string? ProfileImage { get; set; }
	public string? Country { get; set; }
	public bool IsAdmin { get; set; }
	public string? Role { get; set; }

	private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();

	protected List<Claim>? ClaimList;

	//readonly string inside = $"page {Page.Index}; class: {nameof(Claims)}; ";

	protected override async Task OnInitializedAsync()
	{
		/*
		Logger!.LogDebug(string.Format("...Inside {0}; {1}", inside, nameof(OnInitializedAsync)));
		try
		{
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}"
				, inside + "!" + nameof(OnInitializedAsync)));
			Toast!.ShowError($"{Global.ToastShowError}");
		}

		*/
		base.OnInitialized();
		var authState = await AuthenticationStateProvider!.GetAuthenticationStateAsync();
		var user = authState.User;

		//Name = user.Identity.Name;  // Note, this does not work, see OnInitializedAsync at LivingMessiah.Web\Pages\Sukkot\Index.razor.cs
		Name = user.GetUserName() ?? "?";

		EmailAddress = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
		ProfileImage = user.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;
		Country = user.Claims.FirstOrDefault(c => c.Type == "country")?.Value;
		Role = user.Claims.FirstOrDefault(c => c.Type == "https://schemas.livingmessiah.com/roles")?.Value;

		if (user.Identity!.IsAuthenticated)
		{
			//Toast!.ShowWarning($"{Name} is authenticated! Role(s): {Role ?? "NONE"} ");
			Toast!.ShowWarning($"{Name} is authenticated!");

			_claims = user.Claims;
			//_surnameMessage = $"Surname: {user.FindFirst(c => c.Type == ClaimTypes.Surname)?.Value}";
			ClaimList = _claims.ToList();
			

		}
		else
		{
			Toast!.ShowWarning("The user is NOT authenticated.");
		}

	}


}
