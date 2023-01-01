using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace LivingMessiah.Web.Shared;

public partial class DataList<TItem>
{
	[Parameter] public IEnumerable<TItem> Data { get; set; }
	[Parameter] public LinkTypeEnum Type { get; set; }
	[Parameter] public RenderFragment<TItem> ChildContent { get; set; }

	protected override void OnInitialized()
	{
		base.OnInitialized();
	}
}

public enum LinkTypeEnum
{
	Sitemap = 1,
	HomeSidebar = 2,
	Feast = 3,
	Admin = 4,
	AdminDashboard = 5,
	AdminMarkdown = 6,
}
