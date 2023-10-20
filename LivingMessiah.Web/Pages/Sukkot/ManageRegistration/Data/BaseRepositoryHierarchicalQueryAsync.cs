using Dapper;

namespace LivingMessiah.Web.Pages.Sukkot.ManageRegistration.Data;

public abstract class BaseRepositoryHierarchicalQueryAsync
{
	public string? Sql { get; set; }
	public DynamicParameters? Parms { get; set; }  // using Dapper; Note, only place dependent on Dapper


	public string? SqlDump
	{
		get
		{
			string s = "";
			s = Sql ?? "SQL IS NULL";
			if (Parms != null)
			{
				/* See Notes in LivingMessiah.Data!BaseRepositoryAsync*/
			}
			return s;
		}
	}
}
