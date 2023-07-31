using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using Microsoft.Extensions.Logging;
using ParentState = LivingMessiah.Web.Pages.Sukkot.SuperUser.Index;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.Registrant;

public partial class Form
{
	[Inject] public ILogger<Form>? Logger { get; set; }
	[Inject] private IState<RegistrantState>? State { get; set; }
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

	public Sukkot.Enums.DateRangeType DateRangeAttendance { get; set; } = Sukkot.Enums.DateRangeType.Attendance;

	private FluentValidationValidator? _fluentValidationValidator;

	protected void HandleValidSubmit()
	{
		Logger!.LogDebug(string.Format("Inside {0}, FormMode: {1}"
			, nameof(Form) + "!" + nameof(HandleValidSubmit), State!.Value.FormMode!.Name));
		Dispatcher!.Dispatch(new AddOrEdit_Action(State!.Value.FormVM!, State!.Value.FormMode!));
		Dispatcher!.Dispatch(new MasterDetail.GetAll_Action());
		Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Index_Action(SuperUser.Constants.GetPageHeaderForIndexVM()));
	}

	void CancelActionHandler()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(CancelActionHandler)));
		Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Index_Action(SuperUser.Constants.GetPageHeaderForIndexVM()));
	}
}

