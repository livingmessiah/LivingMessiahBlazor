using Blazored.Toast.Services;
using Markdig;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Pages.SpecialEvents.Card;

public partial class SpecialEventCard
{
	[Inject] public Data.IRepository? db { get; set; }
	[Inject] public ILogger<SpecialEventCard>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	protected List<Models.SpecialEventVM>? VM;

	private const int DaysPast = -5;  //
	private const int DaysAhead = 100;  //
	private int RowCnt = 0;

	private string UserInterfaceMessage = "";
	private string LogExceptionMessage = "";
	protected bool TurnSpinnerOff = false;

	protected override async Task OnInitializedAsync()
	{
		SetShowVideoButton();
		try
		{
			Logger!.LogDebug(string.Format("Inside {0} i:{1}"
				, nameof(SpecialEventCard) + "!" + nameof(OnInitializedAsync), 0));

			VM = await db!.GetCurrentEvents();  //daysPast: -3
			if (VM is not null)
			{
				RowCnt = VM.Count;
				Logger!.LogDebug(string.Format("...UpcomingEventList.Count:{0}", RowCnt));
			}
			else
			{
				UserInterfaceMessage = $"{nameof(VM)} NOT FOUND";
				Toast!.ShowWarning(UserInterfaceMessage);
			}
		}
		catch (Exception ex)
		{
			UserInterfaceMessage = "An invalid operation occurred, contact your administrator";
			LogExceptionMessage = string.Format("  Inside catch of {0}"
				, nameof(SpecialEventCard) + "!" + nameof(OnInitializedAsync));
			Logger!.LogError(ex, LogExceptionMessage);
			Toast!.ShowError(UserInterfaceMessage);
		}
		finally
		{
			TurnSpinnerOff = true;
		}


	}

	protected string GetDescriptionMdPipeline(string? description)
	{
		MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
		if (description is null)
		{
			return "null";
		}
		else
		{
			return Markdig.Markdown.ToHtml(description, pipeline);
		}
	}

	public bool ShowVideo { get; set; } = false;
	protected string?  OppositeIcon;
	protected string?  OppositeToggleMsg;

	protected void ShowVideo_Button_Click()
	{
		ShowVideo = !ShowVideo;
		SetShowVideoButton();
		StateHasChanged();
	}

	protected void SetShowVideoButton()
	{
		if (ShowVideo)
		{
			OppositeIcon = "<i class='far fa-arrow-alt-circle-up'></i>";
			OppositeToggleMsg = "Hide Video";
		}
		else
		{
			OppositeIcon = "<i class='far fa-arrow-alt-circle-down'></i>";
			OppositeToggleMsg = "Show Video";
		}
	}

	/*
	See note on SpecialEvent.razor

	[Inject] NavigationManager? NavManager { get; set; }

	private void Edit_ButtonClick(int id)
	{
		NavManager!.NavigateTo(Links.UpcomingEventsAdmin.EditMarkdown.Page + "/" + id);
	}
	*/
}
