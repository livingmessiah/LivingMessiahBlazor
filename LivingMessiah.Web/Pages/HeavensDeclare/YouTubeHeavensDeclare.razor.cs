using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.HeavensDeclare;

public partial class YouTubeHeavensDeclare
{
		[Parameter]
		public string UrlId { get; set; }

		[Parameter]
		public string Title { get; set; }

		const string BaseUrl = "https://www.youtube.com/embed/";
		protected string Url
		{
				get
				{
						return BaseUrl + UrlId + "?rel=0";
				}
		}

}


/*
Error CS0115 'foo.OnInitialized()': no suitable method found to override
https://stackoverflow.com/questions/59051233/blazor-server-code-behind-pattern-oninitializedasync-no-suitable-method
Solution: Neeed to make the namespace be like...
	namespace LivingMessiah.Web.Pages.HeavensDeclare

 
base.OnInitialized(); // https://stackoverflow.com/questions/67760302/why-return-base-method-with-blazor-oninitialized-method
 
 */
