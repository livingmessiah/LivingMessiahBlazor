using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using LivingMessiah.Web.Services;
using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry;
using Blazored.Toast.Services;


namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class AgreementButtons
{
	[Inject] public ILogger<AgreementButtons>? Logger { get; set; }
	[Inject] AppState? AppState { get; set; }
	[Inject] public IToastService? Toast { get; set; }
	[Inject] public IRepository? db { get; set; }

	[Parameter, EditorRequired] public string? EmailParm { get; set; }

	void DoNotAgree_ButtonClick()
	{
		Logger!.LogDebug(string.Format("Event: {0} clicked"
			, nameof(AgreementButtons) + "!" + nameof(DoNotAgree_ButtonClick)));
		AppState!.UpdateMessage(this, "Not agreeing to the House Rules Agreement terminates the Registration Process");
		Toast!.ShowWarning($"Not agreeing to the House Rules Agreement terminates the Registration Process");
	}

	private async Task Agree_ButtonClick()
	{
		await Task.Delay(0);
		Logger!.LogDebug(string.Format("Event: {0} clicked"
			, nameof(AgreementButtons) + "!" + nameof(Agree_ButtonClick)));
		int id = 0;
		try
		{
			id = await db!.InsertHouseRulesAgreement(EmailParm!, GetLocalTimeZone());
			Logger!.LogDebug(string.Format("...returned id: {0}", id));
			Toast!.ShowInfo($"Record updated for House Rules Agreement");
						AppState!.UpdateMessage(this, "Record updated for House Rules Agreement");
		}
		catch (InvalidOperationException invalidOperationException)
		{
			AppState!.UpdateMessage(this, invalidOperationException.Message);
			Toast!.ShowError($"{invalidOperationException.Message}");
		}
	}

	private string GetLocalTimeZone()
	{
		return $"Time Zone: {TimeZoneInfo.Local}.";
	}
}
