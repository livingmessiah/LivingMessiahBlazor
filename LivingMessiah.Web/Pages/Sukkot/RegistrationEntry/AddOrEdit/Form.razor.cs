using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.Sukkot.SuperUser;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.AddOrEdit;

public partial class Form
{
	[Inject] public ILogger<Form>? Logger { get; set; }
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	private FormVM? VM => State!.Value.FormVM;

	protected override void OnInitialized()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(OnInitialized)));
		
		string? s = State!.Value.HRA_EMail ?? string.Empty;
		if (!string.IsNullOrEmpty(s)) 
		{
			VM!.EMail = s;
			VM.StatusId = RegistrationSteps.Enums.Status.Payment.Value;
		}

		base.OnInitialized();
	}

	public Enums.DateRangeType DateRangeAttendance { get; set; } = Enums.DateRangeType.Attendance;

	// ToDo: See if I can make this dynamic based on if SuperUser or not
	private FluentValidationValidator? _fluentValidationValidator;

	protected void HandleValidSubmit()
	{
		Logger!.LogDebug(string.Format("Inside {0}, FormMode: {1}"
			, nameof(Form) + "!" + nameof(HandleValidSubmit), State!.Value.FormMode!.Name));
		Dispatcher!.Dispatch(new Submitting_Request_Action(State!.Value.FormVM!, State!.Value.FormMode!));
		Dispatcher!.Dispatch(new Get_List_Action());
		Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(SuperUser.Constants.GetPageHeaderForIndexVM()));
	}

	void CancelActionHandler()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(CancelActionHandler)));
		Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(SuperUser.Constants.GetPageHeaderForIndexVM()));
	}
}
