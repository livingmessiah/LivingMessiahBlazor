using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.Sukkot.Domain;
using LivingMessiah.Web.Pages.Sukkot.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.Details;

public partial class Details
{
	[Parameter] public vwRegistration vwRegistration { get; set; }

	public DateRangeType DateRangeAttendance { get; set; } = DateRangeType.Attendance;
}

