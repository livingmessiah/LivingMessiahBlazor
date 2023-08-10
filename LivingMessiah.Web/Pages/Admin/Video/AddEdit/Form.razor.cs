using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using Microsoft.Extensions.Logging;

using ParentState = LivingMessiah.Web.Pages.Admin.Video.Index;

namespace LivingMessiah.Web.Pages.Admin.Video.AddEdit;

public partial class Form
{
	[Inject] public ILogger<Form>? Logger { get; set; }
	[Inject] private IState<AddEditState>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	private FormVM? VM => State!.Value.FormVM;

	protected override void OnInitialized()
	{
		Logger!.LogDebug(string.Format("Inside Admin.Video.AddEdit!{0}", nameof(Form) + "!" + nameof(OnInitialized)));
		if (State!.Value.ShabbatWeekList is null)
		{
			Logger!.LogDebug(string.Format("...Call {0} because ShabbatWeekList is null", nameof(DB_Populate_ShabbatWeekList)));
			Dispatcher!.Dispatch(new DB_Populate_ShabbatWeekList());
		}

		base.OnInitialized();
	}

	private FluentValidationValidator? _fluentValidationValidator;

	//protected HandleBookChapterVisibility()

	private void HandleBookChapterVisibility(ChangeEventArgs e)
	{
		string? selectedWVT = e.Value!.ToString();
		Logger!.LogDebug(string.Format("...{0}; selectedWVT {1}", nameof(HandleBookChapterVisibility), selectedWVT ?? "null"));

		// 
		//ShowOtherControl = selectedValue == "option1"; // Set to true for Option 1, false otherwise
	}


	protected void HandleValidSubmit()
	{
		Logger!.LogDebug(string.Format("Inside {0}, FormMode: {1}"
			, nameof(Form) + "!" + nameof(HandleValidSubmit), State!.Value.FormMode!.Name));
		Dispatcher!.Dispatch(new DB_InsertOrUpdate_Action(State!.Value.FormVM!, State!.Value.FormMode!));
		//Dispatcher!.Dispatch(new MasterDetail.GetAll_Action());
		Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Index_Action(Video.Constants.GetPageHeaderForIndexVM()));
	}

	void CancelActionHandler()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(CancelActionHandler)));
		Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Index_Action(Video.Constants.GetPageHeaderForIndexVM()));
	}
}

