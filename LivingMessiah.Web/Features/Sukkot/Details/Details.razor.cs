using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Features.Sukkot.Domain;
using LivingMessiah.Web.Features.Sukkot.Enums;

namespace LivingMessiah.Web.Features.Sukkot.Details;

public partial class Details
{
	[Parameter] public vwRegistration? vwRegistration { get; set; }

	public DateRangeType DateRangeAttendance { get; set; } = DateRangeType.Attendance;
}

