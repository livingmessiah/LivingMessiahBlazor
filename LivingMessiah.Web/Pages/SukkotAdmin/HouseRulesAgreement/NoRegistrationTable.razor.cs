using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Blazored.Toast.Services;
using LivingMessiah.Web.Pages.Sukkot.Data;
using LivingMessiah.Web.Pages.Sukkot.Domain;
using System.Collections.Generic;
using LivingMessiah.Web.Pages.Sukkot.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.HouseRulesAgreement;

public partial class NoRegistrationTable
{
	//[Parameter, EditorRequired] public bool RefreshHraList { get; set; }
	
	[Parameter] public EventCallback<string> OnClick { get; set; }

	[Inject] public ISukkotRepository? db { get; set; }
	[Inject] public ISukkotService? svc { get; set; }
	[Inject] public ILogger<NoRegistrationTable>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	//public bool ShowAddRegistrationForm { get; set; } = false;

	protected List<vwHouseRulesAgreement>? VM;


	protected override async Task OnInitializedAsync()
	{
		await PopulateHraList();
		//if (RefreshHraList)		{		}
	}

	private async Task PopulateHraList()
	{
		try
		{
			Logger!.LogDebug(string.Format("Inside {0}"
					, nameof(NoRegistrationTable) + "!" + nameof(PopulateHraList)));

			VM = await db!.NoRegistration();

			if (VM is null)
			{
				Toast!.ShowInfo("No House Rules Agreement records found");
			}
		}
		catch (InvalidOperationException invalidOperationException)
		{
			Toast!.ShowError(invalidOperationException.Message);
		}
	}

	private async Task Refresh_ButtonClick()
	{
		Logger!.LogDebug(string.Format("Inside {0}"
		, nameof(NoRegistrationTable) + "!" + nameof(Refresh_ButtonClick)));
		await PopulateHraList();
	}

	//void AddRegistration_ButtonClick(string email)
	//{
	//	Toast.ShowWarning($"email: {email}");
	//}


	private async Task Delete_ButtonClick(string email)
	{
		Logger!.LogDebug(string.Format("Inside {0}, email: {1}"
		, nameof(NoRegistrationTable) + "!" + nameof(Delete_ButtonClick), email));
		int affectedRows = 0;
		string msg = "";
		try
		{
			msg = $"House Rules Agreement record has been DELETED for {email}";
			affectedRows = await svc!.DeleteHouseRulesAgreementRecord(email);
			msg = $"{msg}; affected rows={affectedRows}";
			Toast!.ShowInfo(msg);
			Logger!.LogInformation(string.Format("{0}", msg));
			await PopulateHraList();
		}
		catch (InvalidOperationException invalidOperationException)
		{
			Toast!.ShowError(invalidOperationException.Message);
		}
	}
}

