using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Features.InDepthStudy;

public partial class ArchiveTable
{
	[Inject] public ILogger<ArchiveTable>? Logger { get; set; }
	[Inject] public Data.IRepository? db { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	[Parameter, EditorRequired] public bool ShowTable { get; set; }
	protected bool CurrentShowTable;
	protected override void OnParametersSet()
	{
		CurrentShowTable = ShowTable;
	}

	protected bool TurnSpinnerOff = false;
	public IReadOnlyList<Data.InDepthStudyQuery>? InDepthStudyList;
	//public CurrentVideoVM? VM;

	protected override async Task OnInitializedAsync()
	{
		if (CurrentShowTable)
		{
			await PopulateTable();
		}
	}

	/*
	private void ReturnedFilter(CalendarEnums.DateType filter)
	{
		//Logger!.LogDebug(string.Format("Inside {0}; {1}, filter: {2}", inside, nameof(ReturnedFilter), filter.Name));
		CurrentFilter = filter;
		//PopulateTable();
	}
	*/

	private void ReturnedToggle(bool showTable)
	{
		CurrentShowTable = showTable;
	}

	protected async Task PopulateTable()
	{
		try
		{
			Logger!.LogDebug("{Class}!{Method}", nameof(ArchiveTable), nameof(PopulateTable));
			InDepthStudyList = await db!.GetIndepthVideos(100)!;

			if (InDepthStudyList == null)
			{
				Toast!.ShowWarning($"{nameof(InDepthStudyList)} NOT FOUND");
			}
			else
			{
				// VM = new CurrentVideoVM();
				// VM.Date = InDepthStudyQuery.ShabbatDate.ToString(DateFormat.ddd_mm_dd);
				// VM.Title = InDepthStudyQuery.Title;
				// VM.GraphicFile = InDepthStudyQuery.GraphicFile ?? Blobs.DefaultImage();
				// VM.Category = InDepthStudyQuery.Category;
				// VM.SubCategory = InDepthStudyQuery.SubCategory;
				// VM.YouTubeUrl = InDepthStudyQuery.YouTubeUrl;
				// VM.BookChapterLabel = $"{InDepthStudyQuery.BookTitle} {InDepthStudyQuery.Chapter}";
				// VM.BiblicalUrlReference = InDepthStudyQuery.BiblicalUrlReference;
			}
		}
		catch (System.Exception ex)
		{
			Logger!.LogError(ex, "{Class}!{Method}; {Command}", nameof(ArchiveTable), nameof(PopulateTable), nameof(db.GetIndepth));
			Toast!.ShowError("An invalid operation occurred reading database, contact your administrator");
		}
		finally
		{
			TurnSpinnerOff = true;
		}
	}


}