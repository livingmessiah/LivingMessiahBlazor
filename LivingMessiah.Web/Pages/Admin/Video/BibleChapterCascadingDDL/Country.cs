using System.Collections.Generic;

namespace LivingMessiah.Web.Pages.Admin.Video.BibleChapterCascadingDDL;

public class Country
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public List<State>? States { get; set; }  // this is need for EF (I think)
}
