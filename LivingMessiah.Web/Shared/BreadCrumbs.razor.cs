using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Shared;

public partial class BreadCrumbs
{

		[Parameter]
		public string Href { get; set; }

		[Parameter]
		public string ParentTitle { get; set; }

		[Parameter]
		public string Title { get; set; }

		[Parameter]
		public string Icon { get; set; }

		protected bool IsValid;

		protected override Task OnInitializedAsync()
		{
				if (!String.IsNullOrWhiteSpace(Href))
				{
						IsValid = true;
				}
				else
				{
						IsValid = false;
				}

				if (String.IsNullOrWhiteSpace(ParentTitle))
				{
						ParentTitle = "???";
				}

				if (String.IsNullOrWhiteSpace(Title))
				{
						Title = "???";
				}

				return base.OnInitializedAsync();
		}
}
