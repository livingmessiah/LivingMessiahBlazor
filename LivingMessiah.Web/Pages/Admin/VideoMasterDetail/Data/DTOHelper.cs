
namespace LivingMessiah.Web.Pages.Admin.VideoMasterDetail.Data;

public static class DTOHelper
{
	public static string? Scrub(string? notes)
	{
		if (!string.IsNullOrEmpty(notes))
		{
			return notes.Replace("\"", string.Empty).Replace("'", string.Empty);
		}
		else
		{
			return string.Empty;
		}
	}

}

