using LivingMessiah.Web.Features.Admin.Video.Models;

namespace LivingMessiah.Web.Features.Admin.Video.MasterDetail;

public struct CrudAndIdArgs
{
	public Enums.Crud Crud { get; set; }
	public YouTubeFeed? YouTubeFeed { get; set; }
}
