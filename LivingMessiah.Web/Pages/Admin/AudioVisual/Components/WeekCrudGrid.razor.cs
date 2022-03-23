using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.SmartEnums;
using LivingMessiah.Web.Services;

using Syncfusion.Blazor.Grids;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual.Components;

// : ComponentBase
public partial class WeekCrudGrid
{
	[Inject]
	public ILogger<WeekCrudGrid> Logger { get; set; }

	[Inject]
	ISmartEnumServiceForSfDropDownList svcDDL { get; set; }

	[Inject]
	public IWeeklyVideosRepository db { get; set; }


	public string[] InitSearchField = (new string[] { "ShabbatWeekId" });
	public List<string> Toolbar = (new List<string>() { "Search", "Add", "Edit", "Delete", "Update", "Cancel" });

	//public List<string> Tool { get; set; } = (new List<string>() { "Search" });

	[Parameter]
	public int ShabbatWeekId { get; set; }

	[Parameter]
	public DateTime ShabbatDate { get; set; }

	public List<WeekCrudGridVM> GridData { get; set; }

	private SfGrid<WeekCrudGridVM> Grid;

	private DialogSettings DialogParams = new DialogSettings
	{ MinHeight = "400px", Width = "450px" };

	public void OnActionBegin(ActionEventArgs<WeekCrudGridVM> args)
	{
		if (args.RequestType.ToString() == "Add")
		{
			args.Data.ShabbatWeekId = ShabbatWeekId;
		}
	}

	#region Drop-downs

	//Book Chapter
	public string BookChapterSelectedValue;
	public int BookChapterSelectedId;
	public int CurrentLastChapter = 150;
	protected List<DropDownListVM> DataSourceBibleBooks => svcDDL.GetBibleBooksVM().ToList();

	public void OnChangeBookChapterDDL(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, DropDownListVM> args)
	{
		int i = int.TryParse(args.ItemData.Value, out i) ? i : 0;
		BookChapterSelectedId = i;
		CurrentLastChapter = BibleBook.FromValue(BookChapterSelectedId).LastChapter;
	}

	#endregion


	#region ErrorHandling
	private void InitializeErrorHandling()
	{
		DatabaseInformationMsg = "";
		DatabaseInformation = false;
		DatabaseWarningMsg = "";
		DatabaseWarning = false;
		DatabaseErrorMsg = "";
		DatabaseError = false;
	}

	protected bool DatabaseInformation = false;
	protected string DatabaseInformationMsg { get; set; }
	protected bool DatabaseWarning = false;
	protected string DatabaseWarningMsg { get; set; }
	protected bool DatabaseError { get; set; } // = false; handled by InitializeErrorHandling
	protected string DatabaseErrorMsg { get; set; }

	void Failure(FailureEventArgs e)
	{
		DatabaseErrorMsg = $"Error inside {nameof(Failure)}";
		Logger.LogError(string.Format("Inside {0}; e.Error: {1}", nameof(WeekCrudGrid) + "!" + nameof(Failure), e.Error));
		DatabaseError = true;
	}
	#endregion

	//public List<WeekCrudGridVM> WeekCrudGridVMList { get; set; }
	protected override async Task OnInitializedAsync()
	{
		await Task.Delay(0);
		/*
		WeekCrudGridVMList = await db.GetTopWeeklyVideos(2);
		GridData = WeekCrudGridVMList.Where(x => x.ShabbatWeekId == ShabbatWeekId).ToList();
		*/
		Logger.LogDebug(string.Format("Inside {0} ShabbatWeekId:{1}", nameof(WeekCrudGrid) + "!" + nameof(OnInitialized), ShabbatWeekId));
	}

	/*
https://blazor.syncfusion.com/documentation/datagrid/searching
	<SfGrid DataSource="@Orders" Toolbar=@Tool>
			<GridSearchSettings Operator=Syncfusion.Blazor.Operator.Contains Fields=@InitSearch Key="anton" IgnoreCase="true"></GridSearchSettings>

https://www.syncfusion.com/forums/164059/preset-search-key-when-using-a-custom-dataadaptor
			<GridSearchSettings Operator=Syncfusion.Blazor.Operator.Equal Fields="@(new string[] { "Number" })" Key="@SearchTerm" IgnoreCase="true" />	 

	 */
}


