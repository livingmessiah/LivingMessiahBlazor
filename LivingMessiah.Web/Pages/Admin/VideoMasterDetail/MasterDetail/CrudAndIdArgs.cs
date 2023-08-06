using System;

namespace LivingMessiah.Web.Pages.Admin.VideoMasterDetail.MasterDetail;

public struct CrudAndIdArgs
{
	public Enums.Crud Crud { get; set; }
	public int? Id { get; set; }
	public string? YouTubeId { get; set; }
	public string? Title { get; set; }
	public DateTimeOffset PublishDate { get; set; }
}