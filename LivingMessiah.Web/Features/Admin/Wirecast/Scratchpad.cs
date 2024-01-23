using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Features.Admin.Wirecast;

public class ScratchPad
{
	[DataType(DataType.MultilineText)]
	[MaxLength(4000)]
	public string? WireCast { get; set; }
}
