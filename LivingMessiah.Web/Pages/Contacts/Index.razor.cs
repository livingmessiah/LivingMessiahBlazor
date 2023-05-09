using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Pages.Contacts.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;

using Syncfusion.Blazor.Grids;
using Blazored.Toast.Services;

using Page = LivingMessiah.Web.Links.Contact;

namespace LivingMessiah.Web.Pages.Contacts;

[Authorize(Roles = Roles.AdminOrElder)]
public partial class Index
{
	private const string Message = $"Failed to load page {Page.Index},  Class!Method:{nameof(Index)}!{nameof(OnInitializedAsync)}";

	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] public IContactRepository? db { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	public IEnumerable<Domain.ContactVM>? Contacts { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(Message);
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
			Logger!.LogError(ex, Message);
			Toast!.ShowError($"Error reading database. {Message}");
		}
		StateHasChanged();
	}

}
