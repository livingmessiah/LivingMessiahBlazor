using LivingMessiah.Web.Pages.Admin.Video.Models;

namespace LivingMessiah.Web.Pages.Admin.Video.MasterDetail;

public struct CrudAndIdArgs
{
	public Enums.Crud Crud { get; set; }
	public YouTubeFeed? YouTubeFeed { get; set; }
}
