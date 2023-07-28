using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.Sukkot.SuperUser;
using LivingMessiah.Web.Services;
using System.Threading.Tasks;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.Donations;

public partial class Form
{
	[Inject] public ILogger<Form>? Logger { get; set; }
	[Inject] private IState<DonationState>? DonationState { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }
	[Inject] public ISecurityClaimsService? SvcClaims { get; set; }

	private FluentValidationValidator? _fluentValidationValidator;

	private FormVM? VM => DonationState!.Value.DonationFormVM;
	protected string? FullName;

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(OnInitialized)));

		string email = await SvcClaims!.GetEmail();
		if (String.IsNullOrEmpty(email)) email = "test@test.com";

		VM!.CreatedBy = email;
		VM!.CreateDate = DateTime.UtcNow;
		VM!.RegistrationId = DonationState!.Value.RegistrationId;
		FullName = DonationState!.Value.FullName;

		await base.OnInitializedAsync();
	}


	protected void HandleValidDonationSubmit()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(HandleValidDonationSubmit)));
		Dispatcher!.Dispatch(new Add_Action(DonationState!.Value.DonationFormVM!));
		Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(SuperUser.Constants.GetPageHeaderForIndexVM()));
	}

	void CancelActionHandler()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(CancelActionHandler)));
		Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(SuperUser.Constants.GetPageHeaderForIndexVM()));
	}

}
