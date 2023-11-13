using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.Shavuot;

public class OmerCountUI
{
	public bool ShowOmer { get; set; }
	public string? faX { get; set; }
	public string? HebrewFont { get; set; }
	public int OmerCnt { get; set; }
}

public partial class OmerCount
{
	[Parameter] public bool IsXsOrSm { get; set; }
	[Parameter] public int OverrideOmerCount { get; set; } = 0;
	[Parameter, EditorRequired] public int YearId { get; set; }

	protected OmerCountUI? UI;

	protected override Task OnInitializedAsync()
	{
		UI = new OmerCountUI
		{
			ShowOmer = false,
			faX = IsXsOrSm ? "fa-5x" : "fa-3x ",
			HebrewFont = IsXsOrSm ? "hebrew62" : "hebrew44"
		};

		if (OverrideOmerCount != 0)
		{
			UI.OmerCnt = OverrideOmerCount;
		}
		else
		{
			UI.OmerCnt = Omer.CountInDays();
		}

		if (UI.OmerCnt > 0 && UI.OmerCnt <= 50)
		{
			UI.ShowOmer = true;
		}

		return base.OnInitializedAsync();
	}

}

// Ignore Spelling: Cnt