namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

public partial class YouTubeFeed
{
	[Parameter]
	public string Url { get; set; }

	[Parameter]
	public int Take { get; set; } = 9;

	public List<SyndicationItem>? LatestVideoList { get; set; }

	protected override async Task OnInitializedAsync()
	{
		await Task.Delay(0);
		using var reader = XmlReader.Create(Url);

		var feed = SyndicationFeed.Load(reader);

		LatestVideoList = feed.Items
		.OrderByDescending(x => x.PublishDate)
		.Take(Take)
		.ToList();

	}
}
