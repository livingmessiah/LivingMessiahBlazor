using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Blazored.Toast.Services;

using LivingMessiah.Web.Pages.Contacts.Data;
using Page = LivingMessiah.Web.Links.Contact;

namespace LivingMessiah.Web.Pages.Contacts;

public partial class ReportGrid
{
	readonly string inside = $"page {Page.Index}; class: {nameof(ReportGrid)}";

	[Inject] public ILogger<ReportGrid>? Logger { get; set; }
	[Inject] public IContactRepository? db { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	public IEnumerable<ContactVM>? Contacts { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("...Inside {0}; {1}", inside, nameof(OnInitializedAsync)));
		try
		{
			Contacts = await db!.GetAll();
			if (Contacts == null)
			{
				Toast!.ShowWarning("Contacts NOT FOUND");
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, string.Format("...Inside catch of {0}", inside + "!" + nameof(OnInitializedAsync)));
			Toast!.ShowError($"{Global.ToastShowError}; inside: {inside}!{nameof(OnInitializedAsync)}");
		}
		StateHasChanged();
	}


}
