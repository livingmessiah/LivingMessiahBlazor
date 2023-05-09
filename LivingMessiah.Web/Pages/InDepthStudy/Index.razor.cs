namespace LivingMessiah.Web.Pages.InDepthStudy;

public partial class Index
{
	protected override void OnInitialized()
	{
		SetShowLatestVideoButton();
	}

	public bool ShowLatestVideo { get; set; } = false;
	protected string? OppositeIcon;
	protected string? OppositeToggleMsg;

	protected void ShowLatestVideo_Button_Click()
	{
		ShowLatestVideo = !ShowLatestVideo; // Toggle ShowTable
		SetShowLatestVideoButton();
		StateHasChanged();
	}

	protected void SetShowLatestVideoButton()
	{
		if (ShowLatestVideo)
		{
			OppositeIcon = "<i class='far fa-arrow-alt-circle-up'></i>";
			OppositeToggleMsg = "Hide Latest Video";
		}
		else
		{
			OppositeIcon = "<i class='far fa-arrow-alt-circle-down'></i>";
			OppositeToggleMsg = "Show Latest Video";
		}

	}

}

