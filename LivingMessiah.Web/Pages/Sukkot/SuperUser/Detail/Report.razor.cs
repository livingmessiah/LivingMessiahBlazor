﻿using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.Detail;

public partial class Report
{
	[Inject] private IState<DetailState>? State { get; set; }
	ReportVM? ReportVM => State!.Value.ReportVM;
}