using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.Sukkot.SuperUser;
using LivingMessiah.Web.Services;
using System.Threading.Tasks;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.AddOrEdit;

public partial class DonationForm
{
	[Inject] public ILogger<DonationForm>? Logger { get; set; }
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }
	[Inject] public ISecurityClaimsService? SvcClaims { get; set; }

	private FluentValidationValidator? _fluentValidationValidator;

	private DonationFormVM? VM => State!.Value.DonationFormVM;


	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(DonationForm) + "!" + nameof(OnInitialized)));

		string email = await SvcClaims!.GetEmail();
		if (String.IsNullOrEmpty(email)) email = "test@test.com";

		VM!.CreatedBy = email;
		VM!.CreateDate = DateTime.UtcNow;
		VM!.RegistrationId = State!.Value.RegistrationId;

		await base.OnInitializedAsync();
	}


	protected void HandleValidDonationSubmit()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(DonationForm) + "!" + nameof(HandleValidDonationSubmit)));
		Dispatcher!.Dispatch(new Submitting_Donation_Request_Action(State!.Value.DonationFormVM!));
		Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(SuperUser.Constants.GetPageHeaderForIndexVM()));
	}

	void CancelActionHandler()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(DonationForm) + "!" + nameof(CancelActionHandler)));
		Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(SuperUser.Constants.GetPageHeaderForIndexVM()));
	}

}
