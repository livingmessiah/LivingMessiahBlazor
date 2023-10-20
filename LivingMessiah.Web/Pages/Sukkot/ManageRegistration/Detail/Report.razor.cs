using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Sukkot.ManageRegistration.Detail;

public partial class Report
{
	[Inject] private IState<DetailState>? State { get; set; }
	DetailAndDonationsHierarchicalQuery? ReportVM => State!.Value.ReportVM;
}
