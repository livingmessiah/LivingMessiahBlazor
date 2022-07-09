using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using SukkotApi.Domain.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;
public partial class StatusFlagDump
{
	[Parameter, EditorRequired]
	public Enums.StatusFlag StatusFlag { get; set; }

	[Parameter, EditorRequired]
	public Status Status { get; set; }

	protected List<Enums.StatusFlag> AvailableStatusFlags = Enum.GetValues(typeof(Enums.StatusFlag)).Cast<Enums.StatusFlag>().ToList();


	public bool Has(Enums.StatusFlag statusFlag)
	{
		return StatusFlag.HasFlag(statusFlag);
	}
}
