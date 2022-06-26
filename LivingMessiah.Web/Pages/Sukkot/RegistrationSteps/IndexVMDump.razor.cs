using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public partial class IndexVMDump
{
	[Parameter]
	public IndexVM IndexVM { get; set; }
	public List<StatusFlag> AvailableStatusFlags = Enum.GetValues(typeof(StatusFlag)).Cast<StatusFlag>().ToList();
}
