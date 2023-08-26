using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Web.Pages.Admin.WirecastFolder;

public class ScratchPad
{
	[DataType(DataType.MultilineText)]
	[MaxLength(4000)]
	public string? WireCast { get; set; }
}
