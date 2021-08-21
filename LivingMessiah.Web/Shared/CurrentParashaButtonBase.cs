using Microsoft.AspNetCore.Components;
using LivingMessiah.Domain;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Shared
{
	public class CurrentParashaButtonBase : ComponentBase
	{
		[Inject]
		public Services.IShabbatWeekCacheService Svc { get; set; }

		[Inject]
		public ILogger<CurrentParashaButtonBase> Logger { get; set; }

		[Parameter]
		public bool IsPrinterFriendly { get; set; }
		protected bool _isPrinterFriendly;
				
		protected LivingMessiah.Domain.Parasha.Queries.Parasha Parasha;
		protected string ParashaAnchor;
		protected string ParashaHref;
		protected bool CurrentParashaFound;
		protected bool LoadFailed; // See Notes below

		// See Notes below
		protected override async Task OnInitializedAsync()
		{
			try
			{
				LoadFailed = false;

				CurrentParashaFound = false;
				Parasha = await Svc.GetCurrentParasha();
				if (Parasha != null)  // if (Parasha is not null C# 9)
				{
					Logger.LogDebug($"Parasha found. Parasha.ToString(): {Parasha}");
					CurrentParashaFound = true;
					_isPrinterFriendly = IsPrinterFriendly;
					ParashaHref = MyHebrewBible.ParashaUrl(Parasha.Id, Parasha.NameUrl);
					ParashaAnchor = GetParashaAnchor(ParashaHref, Parasha.TorahLong);
				}
				else
				{
					LoadFailed = true;
				}
			}
			catch (System.Exception ex)
			{
				LoadFailed = true;
				Logger.LogError(ex, $"Failed to load page {nameof(CurrentParashaButtonBase)}");
			}
		}

		private string GetParashaAnchor(string href, string torah)
		{
			if (!_isPrinterFriendly)
			{
				return $"<br /><a href='{href}' class='btn-primary btn-lg' title='The current weekly reading (parasha)' target='_blank'>	<i class='fas fa-external-link-alt'></i> {torah}</a><br /><br />";
			}
			else
			{
				return $"<h3>{torah}</h3>";
			}
		}

		public static class MyHebrewBible
		{
			private const string baseUrl = "https://myhebrewbible.com/Parasha/Triennial/LivingMessiah";
			public static string ParashaUrl(int id, string slug)
			{
				return $"{baseUrl}/{id}?slug={slug}/";
			}

		}
	}
}

/*

# Notes:
- The link below at **Lifecycle methods** gives an example of how to handle errors
- It uses a `try catch` structure inside a `OnParametersSetAsync` method
- [MS docs](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/handle-errors?view=aspnetcore-5.0&pivots=webassembly)
 */