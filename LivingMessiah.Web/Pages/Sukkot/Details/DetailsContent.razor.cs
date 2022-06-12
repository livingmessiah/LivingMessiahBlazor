using Microsoft.AspNetCore.Components;
using SukkotApi.Domain;
using SukkotApi.Domain.Enums;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.Details;

public partial class DetailsContent
{
		[Parameter]
		public vwRegistration vwRegistration { get; set; }

		public DateRangeLocal DateRangeAttendance { get; set; } = DateRangeLocal.FromEnum(DateRangeEnum.AttendanceDays);
}
