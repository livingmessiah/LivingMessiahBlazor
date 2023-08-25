using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace LivingMessiah.Web.Features.Admin.Database;

public partial class Table
{
	[Parameter, EditorRequired] public List<zvwErrorLog>? ErrorLogs { get; set; }
}
