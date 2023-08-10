namespace LivingMessiah.Web.Pages.Admin.Video.BibleChapterCascadingDDL;

public class State
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public int CountryId { get; set; }
	public Country? Country { get; set; }  // a property of navigation, this is need for EF (I think)
}
