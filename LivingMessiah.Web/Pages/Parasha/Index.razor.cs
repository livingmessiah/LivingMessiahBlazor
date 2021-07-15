namespace LivingMessiah.Web.Pages.Parasha
{
	public partial class Index
	{
		protected override void OnInitialized()
		{
			SetShowTableButton();
		}

		public bool ShowTable { get; set; } = false;
		protected string OppositeIcon;
		protected string OppositeToggleMsg;

		protected void ShowTable_Button_Click()
		{
			ShowTable = !ShowTable; // Toggle ShowTable
			SetShowTableButton();
			StateHasChanged();
		}

		protected void SetShowTableButton()
		{
			if (ShowTable)
			{
				OppositeIcon = "<i class='far fa-arrow-alt-circle-up'></i>";
				OppositeToggleMsg = "Hide Weekly Torah Study Table";
			}
			else
			{
				OppositeIcon = "<i class='far fa-arrow-alt-circle-down'></i>";
				OppositeToggleMsg = "Show Weekly Torah Study Table";
			}

		}
	}
}
