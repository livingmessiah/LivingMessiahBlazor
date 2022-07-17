using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;
using Link = LivingMessiah.Web.Links.Account;
using Sukkot.Web.Service;
using System.Threading.Tasks;
using System;
using SukkotApi.Domain.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class GridContent
{

	[Inject]
	public ILogger<GridContent> Logger { get; set; }
	[Inject]
	public ISukkotService svc { get; set; }
	protected IndexVM IndexVM { get; set; }
	private string GetLocalTimeZone()
	{
		return $"Time Zone: {TimeZoneInfo.Local}.";
	}


	[Parameter]
	public EventCallback AgreeButtonCallBack { get; set; }

	[Parameter, EditorRequired]
	public bool IsXs { get; set; }

	[Parameter, EditorRequired]
	public Status UsersCurrentStatus { get; set; }

	[Parameter, EditorRequired]
	public Status ComparisonStatus { get; set; }

	[Parameter, EditorRequired]
	public RegistrationStep RegistrationStep { get; set; }

	/*	*/
		private async Task AgreeButtonCallBackHandler()
	{
		string eMail = IndexVM.EmailAddress;
		Logger.LogDebug(string.Format("Event: {0}"
			, nameof(Index) + "!" + nameof(AgreeButtonCallBackHandler)));
		int id = 0;
		try
		{
			id = await svc.AddHouseRulesAgreementRecord(eMail, GetLocalTimeZone());
			DatabaseInformationMsg = "Record updated";
			DatabaseInformation = true;
			//await PopulateVM();
		}
		catch (InvalidOperationException invalidOperationException)
		{
			DatabaseError = true;
			DatabaseErrorMsg = invalidOperationException.Message;
		}
	}

	#region AlertHandling
	private void InitializeAlertHandlingling()
	{
		AttemptingToGetRecord = true;
		GotRecord = false;
		AttemptingToGetRecordMsg = "";
	}
	protected bool GotRecord;
	protected bool AttemptingToGetRecord;
	protected string AttemptingToGetRecordMsg;
	#endregion

	#region ErrorHandling
	protected bool DatabaseInformation = false;
	protected string DatabaseInformationMsg { get; set; }
	protected bool DatabaseWarning = false;
	protected string DatabaseWarningMsg { get; set; }
	protected bool DatabaseError { get; set; } // = false; handled by InitializeErrorHandling
	protected string DatabaseErrorMsg { get; set; }
	#endregion


}
