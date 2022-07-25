using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.Sukkot.Domain;

namespace LivingMessiah.Web.Pages.Sukkot.Details;

public partial class DetailsContent
{
		[Parameter]
		public vwRegistration vwRegistration { get; set; }

		public DateRangeLocal DateRangeAttendance { get; set; } = DateRangeLocal.FromEnum(DateRangeEnum.AttendanceDays);
}
