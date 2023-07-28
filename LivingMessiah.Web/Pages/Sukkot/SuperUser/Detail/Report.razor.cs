using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.Detail;

public partial class Report
{
	[Inject] private IState<State>? State { get; set; }
	ReportVM? ReportVM => State!.Value.DetailReportVM;
}
