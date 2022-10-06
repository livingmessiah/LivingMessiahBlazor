using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;
using Blazored.Toast.Services;
using LivingMessiah.Web.Pages.Sukkot.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.HouseRulesAgreement;

public partial class AddHraForm
{
	[Parameter] public EventCallback<bool> RecordAdded { get; set; }

	[Inject]
	public ISukkotService svc { get; set; }

	[Inject]
	public ILogger<AddHraForm> Logger { get; set; }

	[Inject]
	public IToastService Toast { get; set; }

	public HouseRulesAgreementVM VM { get; set; } = new HouseRulesAgreementVM();

	protected override async Task OnInitializedAsync()
	{
		//AppState.StateChanged += async (Source, Property) => await AppState_StateChanged(Source, Property);
		//AppState.UpdatRefreshHraNotRegistered(this, false);
	}

	private async Task Add_ButtonClick()
	{
		Logger.LogDebug(string.Format("Event: {0} clicked"
		, nameof(AddHraForm) + "!" + nameof(Add_ButtonClick)));
		int id = 0;
		string msg = "";
		try
		{
			msg = $"House Rules Agreement as been recorded for {VM.EMail} on {GetLocalTimeZone()} Local Time, id: {id}";
			id = await svc.AddHouseRulesAgreementRecord(VM.EMail, GetLocalTimeZone());

			//RecordAdded.InvokeAsync(true);

			Toast.ShowInfo(msg);
			Logger.LogDebug(string.Format("{0}", msg));
		}
		catch (InvalidOperationException invalidOperationException)
		{
			Toast.ShowError(invalidOperationException.Message);
		}
	}

	private string GetLocalTimeZone()
	{
		return $"Time Zone: {TimeZoneInfo.Local}.";
	}


}




