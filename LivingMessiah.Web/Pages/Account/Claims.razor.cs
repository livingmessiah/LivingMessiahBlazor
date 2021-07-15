using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using LivingMessiah.Web.Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.Account
{
	public partial class Claims
	{
    [Inject] 
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    public string EmailAddress { get; set; }
    public string Name { get; set; }
    public string ProfileImage { get; set; }
    public string Country { get; set; }
    public bool Verified { get; set; }
    public string Role { get; set; }

    private string _authMessage;
    private string _surnameMessage;
    private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();

    protected override async Task OnInitializedAsync()
    {
      base.OnInitialized();
      var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
      var user = authState.User;

      Name = user.Identity.Name;
      EmailAddress = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
      ProfileImage = user.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;
      Country = user.Claims.FirstOrDefault(c => c.Type == "country")?.Value;
      //Verified = user.Verified();
      //Role = user.GetRoles();
    }

    private async Task GetClaimsPrincipalData()
    {
      var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
      var user = authState.User;

      if (user.Identity.IsAuthenticated)
      {
        _authMessage = $"{user.Identity.Name} is authenticated.";
        _claims = user.Claims;
        _surnameMessage =
            $"Surname: {user.FindFirst(c => c.Type == ClaimTypes.Surname)?.Value}";
      }
      else
      {
        _authMessage = "The user is NOT authenticated.";
      }
    }

    public string BlueCheck
    {
      get
      {
        if (Verified)
        {
          return "<span class='text-primary'><i class='fas fa-check'></i></span>";
        }
        else
        {
          return "<span class='text-danger'>Unverified<i class='fas fa-question'></i></span>";
        }

      }
    }

    /*
    // https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-5.0#attribute-splatting-and-arbitrary-parameters
    private Dictionary<string, object> TableAttributes { get; set; } =
     new()
     {
       //{ "maxlength", "10" },
       { "class", "table table-striped table-sm" }
     };



     */
  }
}
