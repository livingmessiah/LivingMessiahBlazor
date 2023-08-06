using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.Admin.VideoMasterDetail.Enums;
using System.Threading.Tasks;
using System;

namespace LivingMessiah.Web.Pages.Admin.VideoMasterDetail.MasterDetail;

public partial class CrudButtons
{
	[Parameter, EditorRequired] public Crud? ParmCrud { get; set; }

	[Parameter, EditorRequired] public int? Id { get; set; } 
	[Parameter] public string? YouTubeId { get; set; } 
	[Parameter] public string? Title { get; set; }
	[Parameter] public DateTimeOffset PublishDate { get; set; }

	[Parameter] public EventCallback<CrudAndIdArgs> OnCrudActionSelected { get; set; }

	private async Task OnButtonClicked()  //Crud? crud
	{
		CrudAndIdArgs args = new CrudAndIdArgs
		{
			Crud = ParmCrud!, 
			YouTubeId = YouTubeId,
			Id = Id,
			Title = Title ?? "???",
			PublishDate = PublishDate
		};
		await OnCrudActionSelected.InvokeAsync(args);
	}

	private string TitleFormated => YouTubeId is null ? "YouTubeId is null" : $"YouTubeId: {YouTubeId}";
}
