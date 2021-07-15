using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace LivingMessiah.Web.Shared
{
	public partial class TableTemplate<TItem>
	{
		[Parameter]
		public RenderFragment TableHeader { get; set; }

		[Parameter]
		public RenderFragment<TItem> RowTemplate { get; set; }
		//public RenderFragment<TItem> ChildContent { get; set; }

		[Parameter]
		public IReadOnlyList<TItem> Items { get; set; }
	}
}
