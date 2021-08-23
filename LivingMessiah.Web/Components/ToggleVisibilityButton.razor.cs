using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components; 
//using Microsoft.AspNetCore.Components.Web;

namespace LivingMessiah.Web.Components
{
	public partial class ToggleVisibilityButton
	{
		[Parameter] public RenderFragment ChildContent { get; set; }

		[Parameter(CaptureUnmatchedValues = true)]
		public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }
			= new Dictionary<string, object>() { 
				{ "badgecolor", "badge-warning" },
				{ "buttoncolor", "btn-primary" },
				{ "buttonsize", "btn-sm" },
				{ "buttonfloat", "float-right" }
			};

		string BadgeColor => AdditionalAttributes
			.TryGetValue("badgecolor", out var value) ? value.ToString() : string.Empty;

		string ButtonColor => AdditionalAttributes
			.TryGetValue("buttoncolor", out var value) ? value.ToString() : string.Empty;

		string ButtonSize => AdditionalAttributes
			.TryGetValue("buttonsize", out var value) ? value.ToString() : string.Empty;

		string ButtonFloat => AdditionalAttributes
			.TryGetValue("buttonfloat", out var value) ? value.ToString() : string.Empty;

		//[Parameter] 	public string Title { get; set; }

		public EventCallback OnToggleClick { get; set; }

		public bool IsCollapsed { get; set; } = true;
		protected void Collapsed_ButtonClick()
		{
			IsCollapsed = !IsCollapsed;
		}
	}
}
