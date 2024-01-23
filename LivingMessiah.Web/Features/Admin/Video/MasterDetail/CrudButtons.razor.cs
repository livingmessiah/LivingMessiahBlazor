using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Features.Admin.Video.Enums;
using System.Threading.Tasks;
using System;
using LivingMessiah.Web.Features.Admin.Video.Models;

namespace LivingMessiah.Web.Features.Admin.Video.MasterDetail;

public partial class CrudButtons
{
	[Parameter, EditorRequired] public Crud? CurrentCrudButton { get; set; }
	[Parameter, EditorRequired] public YouTubeFeed? YouTubeFeed { get; set; }

	[Parameter] public EventCallback<CrudAndIdArgs> OnCrudActionSelected { get; set; }


	private string GetDisabled()
	{
		if (CurrentCrudButton!.IsAddMode )
		{
			if (YouTubeFeed!.IsAddMode)  //public bool IsAddMode => Id is null ? true : false;
			{
				return " ";
			}
			else
			{
				return " disabled";
			}
		}
		else  // Not Add, i.e. Edit or Delete button
		{
			if (YouTubeFeed!.IsAddMode)  
			{
				return " disabled";
			}
			else
			{
				return " ";
			}
		}

	}

	private async Task OnButtonClicked()
	{
		CrudAndIdArgs args = new CrudAndIdArgs
		{
			Crud = CurrentCrudButton!,
			YouTubeFeed = YouTubeFeed
		};
		await OnCrudActionSelected.InvokeAsync(args);
	}

	private string TitleFormated => YouTubeFeed!.YouTubeId is null ? "YouTubeId is null" : $"YouTubeId: {YouTubeFeed!.YouTubeId}";
}
