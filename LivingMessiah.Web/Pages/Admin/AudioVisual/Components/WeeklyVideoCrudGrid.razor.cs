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
public partial class WeeklyVideoCrudGrid 
{
	[Inject]
	public ILogger<WeeklyVideoCrudGrid> Logger { get; set; }
	
	[Inject]
	ISmartEnumServiceForSfDropDownList svcDDL { get; set; }

	[Inject]
	public IWeeklyVideosRepository db { get; set; }

	[Parameter]
	public int ShabbatWeekId { get; set; }

	[Parameter]
	public DateTime ShabbatDate { get; set; }

	#region Grid Settings
	private SfGrid<WeeklyVideoCrudGridVM> Grid;

	private string[] InitSearchField = (new string[] { "ShabbatWeekId" });
	private List<string> Toolbar = (new List<string>() { "Search", "Edit", "Delete", "Update", "Cancel" });  // , "Add"
	private DialogSettings DialogParams = new DialogSettings	{ MinHeight = "400px", Width = "450px" };
	#endregion

	public List<WeeklyVideoCrudGridVM> GridData { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(string.Format("Inside {0} ShabbatWeekId:{1}", nameof(WeeklyVideoCrudGrid) + "!" + nameof(OnInitialized), ShabbatWeekId));
		try
		{
			GridData = await db.GetWeeklyVideos();
			if (GridData != null)
			{
				Logger.LogDebug(string.Format("... Data gotten from DATABASE"));
			}
			else
			{
				DatabaseWarning = true;
				DatabaseWarningMsg = "Weekly Videos NOT FOUND";
			}
		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error reading database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}

		
	}

	#region Drop-downs
	public void OnActionBegin(ActionEventArgs<WeeklyVideoCrudGridVM> args)
	{
		/*
		Logger.LogDebug(string.Format("... {0} args.RequestType.ToString:{1}", nameof(WeeklyVideoCrudGrid) + "!" + nameof(OnActionBegin), args.RequestType.ToString()));
		  Update Events --> Refresh, BeforeBeginEdit, BeginEdit, Save
			Add Events		--> Refresh, Add, Save
			Delete Events --> Delete
		*/

		if (args.RequestType.ToString() == "Add")
		{
			args.Data.ShabbatWeekId = ShabbatWeekId;
		}
	}
	#endregion

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
		Logger.LogError(string.Format("Inside {0}; e.Error: {1}", nameof(WeeklyVideoCrudGrid) + "!" + nameof(Failure), e.Error));
		DatabaseError = true;
	}
	#endregion


}
