namespace LivingMessiah.Web.Features.SpecialEvents.Data;

public static class SqlServer
{
	public const int ReturnValueOk = 0;
	public const int ReturnValueViolationInUniqueIndex = 2601;
	public const string ReturnValueName = "ReturnValue";
	public const string ReturnValueParm = "@ReturnValue";
}

// Ignore Spelling: Parm

/*
use master
select * from sysmessages
*/