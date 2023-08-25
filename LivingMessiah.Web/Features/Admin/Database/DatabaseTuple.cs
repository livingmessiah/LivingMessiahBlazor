namespace LivingMessiah.Web.Features.Admin.Database;

public class DatabaseTuple
{
	public int AffectedRows { get; set; }
	public int NewId { get; set; }
	public int SprocReturnValue { get; set; }
	public string? ReturnMsg { get; set; }
}

// Ignore Spelling: Sproc