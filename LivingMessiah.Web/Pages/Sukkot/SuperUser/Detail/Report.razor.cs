using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.Detail;

public partial class Report
{
	[Inject] private IState<State>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	ReportVM? ReportVM => State!.Value.DetailReportVM;

	void CancelActionHandler()
	{
		Dispatcher!.Dispatch(new Set_PageHeader_For_Index_Action(SuperUser.Constants.GetPageHeaderForIndexVM()));
	}
}
