using Microsoft.AspNetCore.Components;
using System;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;

namespace LivingMessiah.Web.Pages.SukkotAdmin.LegalAgreementVerbiage;

[Authorize(Roles = Roles.AdminOrSukkot)]
public partial class AgreementButtonsMockup
{
	[Parameter, EditorRequired]
	public string EmailParm { get; set; }

	[Inject]
	public IToastService Toast { get; set; }

	void DoNotAgree_ButtonClick()
	{
		Toast.ShowWarning("Not agreeing to the House Rules Agreement terminates the Registration Process");
	}

	private  void Agree_ButtonClick()
	{
		Toast.ShowInfo($"House Rules Agreement as been recorded for {EmailParm} on {GetLocalTimeZone()} Local Time");
		//int id = 0;
		//id = await svc.AddHouseRulesAgreementRecord(EmailParm, GetLocalTimeZone());
		//AppState.UpdateMessage(this, "Record updated for House Rules Agreement");
	}
	
	private string GetLocalTimeZone()
	{
		return $"Time Zone: {TimeZoneInfo.Local}.";
	}

}
