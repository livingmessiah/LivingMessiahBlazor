- LivingMessiah.Web\Pages\Admin


# \AudioVisual\Components\WeeklyVideos\

## UpdateGrid.razor
@using Syncfusion.Blazor.Grids
@using Syncfusion.Blazor.Data
@using Syncfusion.Blazor.Inputs
@using Syncfusion.Blazor.Popups


@using LivingMessiah.Web.Pages.Admin.AudioVisual.Components.YouTubeFeed
@using LivingMessiah.Web.Pages.Admin.AudioVisual.Services

<p class="text-end text-muted mt-5 mb-0">
	<sup>Shabbat Week Id: @ShabbatWeekId</sup>
</p>

<h4 class="mt-0 mb-1"><span class="badge bg-warning">Shabbat Week @ShabbatDate.ToShortDateString()</span></h4>

<div class="col-lg-12 control-section">
	<div class="content-wrapper">
		<div class="row">

			<SfGrid ID="Grid" @ref="Grid" DataSource="@GridData" Toolbar=@Toolbar>
				<GridSearchSettings Fields=@InitSearchField Key="@ShabbatWeekId.ToString()"
														Operator=Syncfusion.Blazor.Operator.Contains IgnoreCase="true" />
				<SfDataManager AdaptorInstance="typeof(IUpdateGridDataAdaptor)" Adaptor="Adaptors.CustomAdaptor" />
				<GridEvents TValue="UpdateGridViewModel"
										OnActionFailure="Failure" OnActionComplete="OnActionComplete" OnActionBegin="OnActionBegin" OnToolbarClick="ToolbarClickHandler" />
				<GridEditSettings AllowAdding="true" AllowEditing="true" AllowDeleting="true" 
													ShowDeleteConfirmDialog="true" Dialog="DialogParams" Mode="@EditMode.Dialog">

					<Template>
						@{
							var VM = (context as UpdateGridViewModel);
						}
						<div>
							<div class="form row">
								<div class="mb-3 col-md-12">
									<SfNumericTextBox ID="Id" @bind-Value="@(VM.Id)" FloatLabelType="FloatLabelType.Always" Enabled="@Check"
																		Placeholder="Id" />
								</div>
							</div>

							<div class="row">
								<div class="mb-3 col-md-12">
									<label for="weeklyVideoTypeEnum" class="control-label">Event Type</label>
									<InputSelectEnum @bind-Value="VM.WeeklyVideoTypeEnum" class="form-control" id="weeklyVideoTypeEnum" />
								</div>
							</div>


@*							<div class="row">
								<div class="mb-3 col-md-12">
									<SfDropDownList TItem="DropDownListVM" TValue="string" PopupHeight="230px"
																	FloatLabelType="FloatLabelType.Always"
																	Placeholder="Select YouTube Id..."
																	@bind-Value="VM.YouTubeId" DataSource="@DataSource">
										<DropDownListEvents TItem="DropDownListVM" TValue="string" ValueChange="OnChange"></DropDownListEvents>
										<DropDownListFieldSettings Text="Text" Value="Value"></DropDownListFieldSettings>
									</SfDropDownList>

								</div>
							</div>
*@
							<div class="form row">
								<div class="mb-3 col-md-12">
									<SfTextBox ID="Title" @bind-Value="@(VM.Title)" FloatLabelType="FloatLabelType.Always"
														 Placeholder="Title"></SfTextBox>
								</div>
							</div>
@*
							@if (VM.WeeklyVideoTypeEnum == WeeklyVideoTypeEnum.InDepthStudy | VM.WeeklyVideoTypeEnum == WeeklyVideoTypeEnum.TorahTuesday)
							{
								<div class="row">
									<div class="mb-3 col-md-6">
										<SfNumericTextBox ID="Book" @bind-Value="@(VM.Book)" TValue="int"
																		Min="1" Max="66"
																		FloatLabelType="FloatLabelType.Always" Placeholder="Book"></SfNumericTextBox>
									</div>

									<div class="mb-3 col-md-6">
										<SfNumericTextBox ID="Chapter" @bind-Value="@(VM.Chapter)" TValue="int"
																		Min="1" Max="@CurrentLastChapter"
																		FloatLabelType="FloatLabelType.Always" Placeholder="Chapter"></SfNumericTextBox>
									</div>
								</div>

							}*@

						</div>
					</Template>
				</GridEditSettings>

				<GridColumns>
					<GridColumn Field=@nameof(UpdateGridViewModel.Id) IsPrimaryKey="true" HeaderText="PK Id" Visible="true" Width="40"
											TextAlign="@TextAlign.Center" HeaderTextAlign="@TextAlign.Center"
											ValidationRules="@(new Syncfusion.Blazor.Grids.ValidationRules{ Required=true, Number=true})" />

					<GridColumn Field=@nameof(UpdateGridViewModel.ShabbatWeekId) HeaderText="Id" Width="40" TextAlign="@TextAlign.Center"
											ValidationRules="@(new Syncfusion.Blazor.Grids.ValidationRules{ Required=true, Number=true})" />

					<GridColumn Field=@nameof(UpdateGridViewModel.WeeklyVideoTypeEnum) HeaderText="WVT Enum" Width="50" TextAlign="@TextAlign.Left"
											ValidationRules="@(new Syncfusion.Blazor.Grids.ValidationRules{ Required=true})" />

					<GridColumn Field=@nameof(UpdateGridViewModel.Title) HeaderText="Title" Width="100"
											ValidationRules="@(new Syncfusion.Blazor.Grids.ValidationRules{ Required=true, MaxLength=150})" />

@*					<GridColumn Field=@nameof(UpdateGridViewModel.YouTubeId) HeaderText="YouTube Id" Width="80" TextAlign="@TextAlign.Center"
											ValidationRules="@(new Syncfusion.Blazor.Grids.ValidationRules{ Required=true})" />*@

					<GridColumn HeaderText="Book/Chapter" Width="50">
						<Template>
							@{
								var bc = (context as UpdateGridViewModel);

								@if (bc.WeeklyVideoTypeEnum == WeeklyVideoTypeEnum.MainServiceEnglish | bc.WeeklyVideoTypeEnum == WeeklyVideoTypeEnum.MainServiceSpanish)
								{
									<p class="text-info"><i>N/A</i></p>

								}
								else
								{
									@if (bc.Book != 0 && bc.Chapter != 0)
									{
										<a href="@bc.MHBUrl()" target="_blank"
								 	title="@bc.MHBUrl()">
											<u>@bc.Book / @bc.Chapter</u>
											<i class="fas fa-external-link-square-alt"></i>
										</a>
									}
									else
									{
										<p class="text-info"><i>Missing</i></p>
									}
								}
							}
						</Template>
					</GridColumn>

				</GridColumns>
			</SfGrid>

		</div>
	</div>
</div>


@if (DatabaseError)
{
	<p class="text-danger"><em>@DatabaseErrorMsg</em></p>
}
else
{
	if (DatabaseWarning)
	{
		<p class="text-warning">@DatabaseWarningMsg</p>
	}
}


								@*

// ToDo: in the Template, test if I can use EditForm, DataAnnotationsValidator and ValidationSummary

									ValidateOnInput="true"
									<SfTextBox ID="YouTubeId" @bind-Value="@(VM.YouTubeId)" FloatLabelType="FloatLabelType.Always"
									Placeholder="YouTube Id"></SfTextBox>

									@bind-Value="@SelectedValue" DataSource="@DataSource">

								*@


## UpdateGrid.razor.cs

namespace LivingMessiah.Web.Pages.Admin.AudioVisual.Components.WeeklyVideos;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.SmartEnums;
//using LivingMessiah.Web.Services;

using LivingMessiah.Web.Pages.Admin.AudioVisual.Services;
using LivingMessiah.Web.Pages.Admin.AudioVisual.Components.YouTubeFeed;

using Syncfusion.Blazor.Grids;

public partial class UpdateGrid
{
	[Inject]
	public ILogger<UpdateGrid> Logger { get; set; }

	//[Inject]	ISmartEnumServiceForSfDropDownList svcDDL { get; set; }

	[Inject]
	public IWeeklyVideosRepository db { get; set; }

	[Inject]
	public IYouTubeFeedService Svc { get; set; }

	[Parameter]
	public int ShabbatWeekId { get; set; }

	[Parameter]
	public DateTime ShabbatDate { get; set; }

	#region Grid Settings
	private SfGrid<UpdateGridViewModel> Grid;

	private string[] InitSearchField = (new string[] { "ShabbatWeekId" });
	private List<string> Toolbar = (new List<string>() { "Search", "Add", "Edit", "Delete", "Update", "Cancel" });  
	private DialogSettings DialogParams = new DialogSettings { MinHeight = "400px", Width = "450px" };
	#endregion

	public List<UpdateGridViewModel> GridData { get; set; }

	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(string.Format("Inside {0} ShabbatWeekId:{1}", nameof(UpdateGrid) + "!" + nameof(OnInitialized), ShabbatWeekId));
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

		Logger.LogDebug(string.Format("...calling  {0} ", nameof(Svc.GetDropDownList) ));
		DataSource = await Svc.GetDropDownList(SocialMedia.YouTube.YouTubeFeed(), 5);

	}

	#region Events
	public void OnActionBegin(ActionEventArgs<UpdateGridViewModel> args)
	{
		Logger.LogDebug(string.Format("... {0} args.RequestType.ToString:{1}", nameof(UpdateGrid) + "!" + nameof(OnActionBegin), args.RequestType.ToString()));
		/*
		
		  Update Events --> Refresh, BeforeBeginEdit, BeginEdit, Save
			Add Events		--> Refresh, Add, Save
			Delete Events --> Delete
		*/

		if (args.RequestType.ToString() == "Add")
		{
			args.Data.ShabbatWeekId = ShabbatWeekId;
		}
	}


	private Boolean Check = false;
	public void OnActionComplete(ActionEventArgs<UpdateGridViewModel> args)
	{
		Logger.LogDebug(string.Format("... {0} args.RequestType.ToString:{1}"
			, nameof(UpdateGrid) + "!" + nameof(OnActionComplete), args.RequestType.ToString()));
		
		if (args.RequestType.ToString() == "Add")
		{
			Check = true;
		}
		else
		{
			Check = false;
		}
	}


	public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
	{
		Logger.LogDebug(string.Format("... {0} args.RequestType.ToString:{1}"
			, nameof(UpdateGrid) + "!" + nameof(ToolbarClickHandler), args.Item.Id.ToString()));
		
		await Task.Delay(0);
		/*
		if (args.Item.Id == SyncFusionToolbarCRUD.Add.ArgId) 
		{ 
			await this.Grid.AddRecordAsync(); 
		}
		if (args.Item.Id == SyncFusionToolbarCRUD.Edit.ArgId) 
			{ 
			await this.Grid.StartEditAsync(); 
			}
		if (args.Item.Id == SyncFusionToolbarCRUD.Delete.ArgId) 
			{ 
				await this.Grid.DeleteRecordAsync(); 
			}
		if (args.Item.Id == SyncFusionToolbarCRUD.Update.ArgId)
		{
		}
		if (args.Item.Id == SyncFusionToolbarCRUD.Cancel.ArgId) 
		{ 
			await this.Grid.CloseEditAsync(); 
		}
		*/
	}


	#endregion

	#region Drop-downs

	////Book Chapter
	//public string BookChapterSelectedValue;
	//public int BookChapterSelectedId;
	public int CurrentLastChapter = 150;
	////protected List<DropDownListVM> DataSourceBibleBooks => svcDDL.GetBibleBooksVM().ToList();

	//public void OnChangeBookChapterDDL(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, DropDownListVM> args)
	//{
	//	int i = int.TryParse(args.ItemData.Value, out i) ? i : 0;
	//	BookChapterSelectedId = i;
	//	CurrentLastChapter = BibleBook.FromValue(BookChapterSelectedId).LastChapter;
	//}


	public List<DropDownListVM> DataSource { get; set; }

	public string SelectedValue;
	public string SelectedId;

	public void OnChange(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, DropDownListVM> args)
	{
		SelectedId = args.ItemData.Value;
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
		Logger.LogError(string.Format("Inside {0}; e.Error: {1}", nameof(UpdateGrid) + "!" + nameof(Failure), e.Error));
		DatabaseError = true;
	}
	#endregion


}

## UpdateGridDataAdaptor.cs
namespace LivingMessiah.Web.Pages.Admin.AudioVisual.Components.WeeklyVideos;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

public interface IUpdateGridDataAdaptor
{
	Task<object> InsertAsync(DataManager dataManager, object data, string key);
	Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null);
	Task<object> RemoveAsync(DataManager dataManager, object primaryKeyValue, string keyField, string key);
	Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key);
}

public class UpdateGridDataAdaptor : DataAdaptor, IUpdateGridDataAdaptor
{
	#region Constructor and DI
	public IWeeklyVideosRepository db;
	public UpdateGridDataAdaptor(IWeeklyVideosRepository weeklyVideosRepository)
	{
		db = weeklyVideosRepository;
	}
	#endregion

	public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
	{
		List<UpdateGridViewModel> recs = await db.GetTopWeeklyVideos();
		int count = recs.Count;
		return dataManagerRequest.RequiresCounts ? new DataResult() { Result = recs, Count = count } : count;
	}

	public override async Task<object> InsertAsync(DataManager dataManager, object data, string key)
	{
		await db.WeeklyVideoAdd(data as UpdateGridViewModel);
		return data;
	}

	public override async Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key)
	{
		await db.WeeklyVideoUpdate(data as UpdateGridViewModel);
		return data;
	}

	public override async Task<object> RemoveAsync(DataManager dataManager, object primaryKeyValue, string keyField, string key)
	{
		await db.WeeklyVideoDelete(Convert.ToInt32(primaryKeyValue));
		return primaryKeyValue;
	}


}



## UpdateGridViewModel.cs
namespace LivingMessiah.Web.Pages.Admin.AudioVisual.Components.WeeklyVideos;

using System;
//using System.ComponentModel.DataAnnotations;

//using global::LivingMessiah.Web.SmartEnums;
using LivingMessiah.Web.SmartEnums;


public class UpdateGridViewModel
{
	public int? Id { get; set; }

	//[Required]
	public WeeklyVideoTypeEnum WeeklyVideoTypeEnum { get; set; }

	//[Required]
	public int ShabbatWeekId { get; set; }

	//public string ShabbatWeekIdString { get; set; }

	//[Required]
	//[Display(Name = "YouTube Id")]
	//[StringLength(11, MinimumLength = 3, ErrorMessage = "length {0} must be between {2} and {1}.")]
	public string YouTubeId { get; set; }

	public string Url()
	{
		if (YouTubeId != null)
		{
			return $"https://www.youtube.com/watch?v={YouTubeId}";
		}
		else
		{
			return "";
		}
	}


	public string MHBUrl()
	{
		if (Book != 0 && Chapter != 0)
		{
			if (BibleBook.TryFromValue(Book, out var se))
			{
				return $"https://myhebrewbible.com/BookChapter/{se.Name}/{Chapter}/Slug";
			}
			else
			{
				return "";
			}
		}
		else
		{
			return "";
		}
	}



	/* 
Unique every week
- 1 MS Eng:  The Spiritual State of Faithlessness
- 2 MS Esp:  El Estado Espiritual De Infidelidad

More static as it derived from the Book/Chapter
- 3 Indepth: SubTitle="<strike>Ralphie</strike> We continues the study in [<b>John</b> (<i>Yochanan</i>]
- 4 ToTu:    SubTitle="<strike>Mark</strike> We continue the study in [<b>Exodus</b> (<i>Shemot</i>] 
	*/
	//[Required]
	//[Display(Name = "YouTube Title")]
	//[StringLength(150, MinimumLength = 3, ErrorMessage = "length of {0} must be between {2} and {1}")]
	public string Title { get; set; }

	////[Required]
	public string GraphicFileRoot { get; set; } // File given by Ralphie

	public string NotesFileRoot { get; set; }   // File given by Mark

	//[Range(1, 66, ErrorMessage = "length of {0} must be between {1} and {2}")]
	public int Book { get; set; }

	//[Range(1, 150, ErrorMessage = "length of {0} must be between {1} and {2}")]
	public int Chapter { get; set; }

	//For TypeId = 3 use...		
	//public BookChapter TorahBookChapter { get; set; }
	//For TypeId = 4 use...		
	//public BookChapter GospelBookChapter { get; set; }


	//public override string ToString()
	//{
	//	return $@"  Id: {Id}; WeeklyVideoTypeEnum: {WeeklyVideoTypeEnum}; ShabbatWeekId: {ShabbatWeekId}; YouTubeId: {YouTubeId}";
	//}

}


# \AudioVisual\Components\YouTubeFeed\

## DropDownList.razor

@using LivingMessiah.Web.Pages.Admin.AudioVisual.Services

<SfDropDownList TItem="DropDownListVM" TValue="string" PopupHeight="230px"
								Placeholder="Select YouTube Id..."
								@bind-Value="@SelectedValue" DataSource="@DataSource">
	<DropDownListEvents TItem="DropDownListVM" TValue="string" ValueChange="OnChange"></DropDownListEvents>
	<DropDownListFieldSettings Text="Text" Value="Value"></DropDownListFieldSettings>
</SfDropDownList>


## DropDownList.razor.cs
namespace LivingMessiah.Web.Pages.Admin.AudioVisual.Components.YouTubeFeed;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.Admin.AudioVisual.Services;
using Microsoft.Extensions.Logging;

public partial class DropDownList
{
	[Inject]
	public IYouTubeFeedService Svc { get; set; }

	[Inject]
	public ILogger<DropDownList> Logger { get; set; }

	public string SelectedValue;
	public int SelectedId;

	public List<DropDownListVM> DataSource { get; set; }

	public void OnChange(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, DropDownListVM> args)
	{
		int i = int.TryParse(args.ItemData.Value, out i) ? i : 0;
		SelectedId = i;
	}

	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(string.Format("Inside {0} ", nameof(DropDownList) + "!" + nameof(OnInitialized)));
		DataSource = await Svc.GetDropDownList(SocialMedia.YouTube.YouTubeFeed(), 5);
	}
}

//try
//{
//	GridData = await db.GetWeeklyVideos();
//	if (GridData != null)
//	{
//		Logger.LogDebug(string.Format("... Data gotten from DATABASE"));
//	}
//	else
//	{
//		DatabaseWarning = true;
//		DatabaseWarningMsg = "Weekly Videos NOT FOUND";
//	}
//}
//catch (Exception ex)
//{
//	DatabaseError = true;
//	DatabaseErrorMsg = $"Error reading database";
//	Logger.LogError(ex, $"...{DatabaseErrorMsg}");
//}


# \AudioVisual\

## WeeklyVideoTypeEnum.cs
namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

public enum WeeklyVideoTypeEnum
{
		MainServiceEnglish = 1,
		MainServiceSpanish = 2,
		InDepthStudy = 3,
		TorahTuesday = 4
}



## WeeklyVideosRepository.cs
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;

// From Base Class
using System.Text;
using System.Data.SqlClient;
using LivingMessiah.Web.Pages.Admin.AudioVisual.Components;
using LivingMessiah.Web.Pages.Admin.AudioVisual.Components.WeeklyVideos;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

public interface IWeeklyVideosRepository
{
	string BaseSqlDump { get; }

	// Query 
	//Task<Tuple<int, DateTime, int, DateTime>> GetShabbatWeekLookup(int top); // ToDo: is this Deprecated
	Task<List<UpdateGridViewModel>> GetTopWeeklyVideos(int top = 3);
	Task<List<UpdateGridViewModel>> GetWeeklyVideos();
	Task<List<ShabbatWeek>> GetShabbatWeekList(int top);
	Task<List<WeeklyVideoTable>> GetWeeklyVideoTableList(int top);

	// Command
	Task<int> WeeklyVideoAdd(UpdateGridViewModel dto);
	Task<int> WeeklyVideoUpdate(UpdateGridViewModel dto);
	Task<int> WeeklyVideoDelete(int id);
}

public class WeeklyVideosRepository : IWeeklyVideosRepository
{
	public WeeklyVideosRepository(IConfiguration config, ILogger<WeeklyVideosRepository> logger)
	{
		this.config = config;
		this.Logger = logger;
		connectionString = config[configationConnectionKey];
	}

	public string BaseSqlDump
	{
		get { return SqlDump; }
	}

	#region BaseClass
	const string configationConnectionKey = "ConnectionStrings:LivingMessiah";
	private readonly IConfiguration config;
	protected readonly ILogger Logger;

	public string Sql { get; set; }
	public DynamicParameters Parms { get; set; }  // using Dapper; Note, only place dependent on Dapper

	string connectionString;

	public string SqlDump
	{
		get
		{
			string s = "";
			s = Sql ?? "SQL IS NULL";
			if (Parms != null)
			{
				string v = "";
				var sb = new StringBuilder();
				foreach (var name in Parms.ParameterNames) // Why is this empty? 
				{
					var pValue = Parms.Get<dynamic>(name);
					v = (pValue != null) ? pValue.ToString() : "null";
					sb.AppendFormat($"name {name}={v}\n");
				}

				s += ", parameter: " + sb.ToString();

			}
			return s;
		}
	}
	#endregion


	#region Query

	public async Task<List<UpdateGridViewModel>> GetWeeklyVideos()
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(WeeklyVideosRepository) + "!" + nameof(GetWeeklyVideos)));
		Sql = $@"
SELECT TOP 20
wv.Id, wv.ShabbatWeekId, wv.WeeklyVideoTypeId AS WeeklyVideoTypeEnum, wv.YouTubeId, wv.Title
, wv.Book, wv.Chapter

FROM  WeeklyVideo wv
	INNER JOIN ShabbatWeek sw
		ON wv.ShabbatWeekId = sw.Id
WHERE sw.ShabbatDate <= dbo.udfGetNextShabbatDate()
ORDER BY sw.ShabbatDate DESC
";

		using var connect = new SqlConnection(connectionString);
		await connect.OpenAsync();
		var rows = await connect.QueryAsync<UpdateGridViewModel>(sql: Sql);
		Logger.LogDebug(string.Format("...Sql {0}", Sql));
		return rows.ToList();
	}

	public async Task<List<WeeklyVideoTable>> GetWeeklyVideoTableList(int top = 9)
	{
		Logger.LogDebug(string.Format("Inside {0}, top={1}", nameof(WeeklyVideosRepository) + "!" + nameof(GetWeeklyVideoTableList), top));
		Parms = new DynamicParameters(new { Top = top });

		Sql = $@"
-- DECLARE @Top int = 3
SELECT 
 wv.Id
, Descr AS WeeklyVideoTypeDescr
, ShabbatDate
, wv.ShabbatWeekId,	wv.WeeklyVideoTypeId
,	wv.YouTubeId
, wv.Title
--, LAG(wv.ShabbatWeekId, 1, 0) OVER (ORDER BY ShabbatDate DESC, tvf.WeeklyVideoTypeId) AS PrevShabbatWeekId
FROM tvfShabbatWeekCrossWeeklyVideoTypeByTop(@Top) tvf
LEFT OUTER JOIN WeeklyVideo wv 
	ON tvf.ShabbatWeekId = wv.ShabbatWeekId AND
	   tvf.WeeklyVideoTypeId = wv.WeeklyVideoTypeId
WHERE wv.Id IS NOT NULL
ORDER BY ShabbatDate DESC, tvf.WeeklyVideoTypeId
";

		using var connect = new SqlConnection(connectionString);
		await connect.OpenAsync();
		var rows = await connect.QueryAsync<WeeklyVideoTable>(sql: Sql, param: Parms);
		//Logger.LogDebug(string.Format("...Sql {0}", Sql));
		return rows.ToList();
	}

	// Deprecated
	public async Task<List<UpdateGridViewModel>> GetTopWeeklyVideos(int top = 2)
	{
		Logger.LogDebug(string.Format("Inside {0}, top={1}", nameof(WeeklyVideosRepository) + "!" + nameof(GetTopWeeklyVideos), top));
		Parms = new DynamicParameters(new { Top = top });

		Sql = $@"
-- DECLARE @Top int = 3
SELECT
	wv.Id
, tvf.WeeklyVideoTypeId AS WeeklyVideoTypeEnum
, Descr
, tvf.ShabbatWeekId
, ROW_NUMBER () OVER (ORDER BY ShabbatDate DESC, tvf.WeeklyVideoTypeId )  AS RowNum
, ShabbatDateYMD
,	ShabbatDate
,	wv.ShabbatWeekId,	wv.WeeklyVideoTypeId
,	wv.YouTubeId
, wv.Title
, CASE 
   WHEN wv.Id IS NULL OR wv.GraphicFile IS NOT NULL
			THEN wv.GraphicFile
			ELSE CAST(tvf.WeeklyVideoTypeId AS varchar(10)) + '-graphic-' +  ShabbatDateYMD -- + '-' + ISNULL(wv.Title, '***title***')
	END AS GraphicFile
, CASE 
   WHEN wv.Id IS NULL OR wv.NotesFile IS NOT NULL
			THEN wv.NotesFile
			ELSE CAST(tvf.WeeklyVideoTypeId AS varchar(10)) + '-notes-' +  ShabbatDateYMD -- + '-' + ISNULL(wv.Title, '***title***')
	END AS NotesFile
, wv.Book
, wv.Chapter
FROM tvfShabbatWeekCrossWeeklyVideoTypeByTop(@Top) tvf
LEFT OUTER JOIN WeeklyVideo wv 
	ON tvf.ShabbatWeekId = wv.ShabbatWeekId AND
	   tvf.WeeklyVideoTypeId = wv.WeeklyVideoTypeId
ORDER BY ShabbatDate DESC, tvf.WeeklyVideoTypeId
";

		using var connect = new SqlConnection(connectionString);
		await connect.OpenAsync();
		var rows = await connect.QueryAsync<UpdateGridViewModel>(sql: Sql, param: Parms);
		//Logger.LogDebug(string.Format("...rows={0}, Sql {1}", rows, Sql));
		Logger.LogDebug(string.Format("...Sql {0}", Sql));
		return rows.ToList();
	}

	/*
	public async Task<Tuple<int, DateTime, int, DateTime>> GetShabbatWeekLookup(int top)
	{
		Logger.LogDebug(string.Format("Inside {0}, top={1}", nameof(WeeklyVideosRepository) + "!" + nameof(GetShabbatWeekLookup), top));
		Parms = new DynamicParameters(new { Top = top });
		Sql = $@"
-- DECLARE @Top int = 3
SELECT TOP (@Top) Id, ShabbatDate
FROM ShabbatWeek 
WHERE ShabbatDate <= dbo.udfGetNextShabbatDate()
ORDER BY ShabbatDate DESC
";
		int MaxId;
		DateTime MaxShabbatDate;
		int MinId;
		DateTime MinShabbatDate;

		using var connect = new SqlConnection(connectionString);
		await connect.OpenAsync();
		var rows = await connect.QueryAsync<ShabbatWeekLookup>(sql: Sql, param: Parms);
		if (rows.Any())
		{
			// Because the query is in DESC order, MaxId=First & MinId=Last
			MaxId = rows.FirstOrDefault().Id;
			MaxShabbatDate = rows.FirstOrDefault().ShabbatDate;
			MinId = rows.LastOrDefault().Id;
			MinShabbatDate = rows.LastOrDefault().ShabbatDate;
			Logger.LogDebug(string.Format("...MinId={0}, MaxId={1}, Sql {2}", MinId, MaxId, Sql));
			return new Tuple<int, DateTime, int, DateTime>(MaxId, MaxShabbatDate, MinId, MinShabbatDate);
		}
		return new Tuple<int, DateTime, int, DateTime>(0, default(DateTime), 0, default(DateTime));
	}
	*/


	public async Task<List<ShabbatWeek>> GetShabbatWeekList(int top = 3)
	{
		Logger.LogDebug(string.Format("Inside {0}, top={1}", nameof(WeeklyVideosRepository) + "!" + nameof(GetShabbatWeekList), top));
		Parms = new DynamicParameters(new { Top = top });
		Sql = $@"
-- DECLARE @Top int = 3
SELECT TOP (@Top) Id, ShabbatDate
FROM ShabbatWeek 
WHERE ShabbatDate <= dbo.udfGetNextShabbatDate()
ORDER BY ShabbatDate DESC
";
		using var connect = new SqlConnection(connectionString);
		await connect.OpenAsync();
		var rows = await connect.QueryAsync<ShabbatWeek>(sql: Sql, param: Parms);
		Logger.LogDebug(string.Format("...Sql {0}", Sql));
		return rows.ToList();
	}


	#endregion

	#region Command

	// Deprecated ?
	public async Task<int> WeeklyVideoAdd(UpdateGridViewModel dto)
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(WeeklyVideosRepository) + "!" + nameof(WeeklyVideoAdd)));
		Parms = new DynamicParameters(new
		{
			dto.ShabbatWeekId,
			dto.WeeklyVideoTypeEnum,
			dto.YouTubeId,
			dto.Title,
			dto.Book,
			dto.Chapter
		});
		//dto.GraphicFileRoot,dto.NotesFileRoot, ... GraphicFile, NotesFile ... , @GraphicFile, @NotesFile
		Sql = $@"
INSERT INTO WeeklyVideo
(ShabbatWeekId, WeeklyVideoTypeId, YouTubeId, Title, Book, Chapter)
VALUES (@ShabbatWeekId, @WeeklyVideoTypeEnum, @YouTubeId, @Title, @Book, @Chapter)
; SELECT CAST(SCOPE_IDENTITY() as int)
";
		int newId;

		using var connect = new SqlConnection(connectionString);
		await connect.OpenAsync();
		newId = await connect.ExecuteScalarAsync<int>(Sql, Parms);
		Logger.LogDebug(string.Format("...newId={0}, Sql {1}", newId, SqlDump));
		return newId;
	}

	public async Task<int> WeeklyVideoUpdate(UpdateGridViewModel dto)
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(WeeklyVideosRepository) + "!" + nameof(WeeklyVideoUpdate)));
		Parms = new DynamicParameters(new
		{
			dto.Id,
			dto.ShabbatWeekId,
			dto.WeeklyVideoTypeEnum,
			dto.YouTubeId,
			dto.Title,
			//dto.GraphicFileRoot,
			//dto.NotesFileRoot,
			dto.Book,
			dto.Chapter

		});
		Sql = $@"
UPDATE WeeklyVideo SET
  ShabbatWeekId = @ShabbatWeekId
, WeeklyVideoTypeId = @WeeklyVideoTypeEnum
, YouTubeId = @YouTubeId
, Title = @Title
--, GraphicFile = @GraphicFile
--, NotesFile = @NotesFile
, Book = @Book
, Chapter = @Chapter
WHERE Id = @Id
";

		using var connect = new SqlConnection(connectionString);
		await connect.OpenAsync();
		var affectedrows = await connect.ExecuteAsync(sql: Sql, param: Parms);
		Logger.LogDebug(string.Format("...affectedrows={0}, SqlDump {1}", affectedrows, SqlDump));
		return affectedrows;
	}

	public async Task<int> WeeklyVideoDelete(int id)
	{
		Sql = "DELETE FROM WeeklyVideo WHERE Id = @id";
		Parms = new DynamicParameters(new { Id = id });
		using var connect = new SqlConnection(connectionString);
		await connect.OpenAsync();
		var affectedrows = await connect.ExecuteAsync(sql: Sql, param: Parms);
		Logger.LogDebug(string.Format("...affectedrows={0}, SqlDump {1}", affectedrows, SqlDump));
		return affectedrows;
	}

	#endregion

}


# \AudioVisual\

## Index.razor
@page "/Admin/AudioVisual/"
@using static LivingMessiah.Web.Links.AudioVisual

@using Page = LivingMessiah.Web.Links.Admin.AudioVisual
@using static LivingMessiah.Web.Services.Auth0
@using LivingMessiah.Web.Pages.Admin.AudioVisual.Components
@using LivingMessiah.Web.Pages.Admin.AudioVisual.Components.WeeklyVideos
@using LivingMessiah.Web.Pages.Admin.AudioVisual.Components.YouTubeFeed

<PageTitle>@Page.Title</PageTitle>

<div class="pb-2 mt-4 mb-2 border-bottom">
	<h2><i class="@Icon"></i> @Title</h2>
</div>

@using Microsoft.AspNetCore.Hosting
@using Microsoft.Extensions.Hosting
@inject IWebHostEnvironment Env
@if (Env.IsDevelopment())
{
	@*<WeeklyVideoAddForm Title="Title-1" YouTubeId="YouTubeId1" />*@
	@*<YouTubeFeed Url="@SocialMedia.YouTube.YouTubeFeed()" Take="5" />*@
	@*<hr class="warning" />*@

	@*<DropDownList></DropDownList>*@

	@*<UpdateGrid ShabbatWeekId="@MinShabbatWeekId" ShabbatDate="@MinShabbatDate" />*@
	<WeeklyVideoAddForm></WeeklyVideoAddForm>
}
else
{
	<AuthorizeView Roles="@Roles.AdminOrAnnouncements">

		<Authorized>
			<p class="text-muted my-1">
				<a href="@SocialMedia.YouTube.YouTubeNormal()"
				 class="btn btn-danger" title="Watch our videos on YouTube">
					<i class="fab fa-youtube" aria-hidden="true"></i>
					YouTube Search=living+messiah
				</a>
			</p>

			<YouTubeFeed Url="@SocialMedia.YouTube.YouTubeFeed()" Take="5" />
			<hr class="warning" />

			@*<UpdateGrid ShabbatWeekId="@MinShabbatWeekId" ShabbatDate="@MinShabbatDate" />*@

		</Authorized>

		<NotAuthorized>

			<div class="card border-warning my-5">
				<div class="card-header">Not Authorized</div>

				<div class="card-body">
					<h5 class="">To view <b>@Page.Title</b> you need to be logged in.</h5>
					<button @onclick="@(() => RedirectToLoginClick(Page.Redirect))"
								type="button" class="btn-primary btn-sm">
						Login <i class='fas fa-sign-in-alt'></i>
					</button>
				</div>
			</div>

		</NotAuthorized>
	</AuthorizeView>
}

<div class="card mt-5" style="width: 25rem;">
	<div class="card-header">Audio Visual Links</div>

	<ul class="list-group list-group-flush">
		<li class="list-group-item">
			<a href="@LivingMessiah.Web.Links.WeeklyVideos.Index"
				 title="@LivingMessiah.Web.Links.WeeklyVideos.Title"
				 class="list-group-item list-group-item-warning">
				<b>@LivingMessiah.Web.Links.WeeklyVideos.Title</b>
				<i class="@LivingMessiah.Web.Links.WeeklyVideos.Icon"></i>
			</a>
		</li>

		<li class="list-group-item">
			<a href="@LivingMessiah.Web.Links.Wirecast.Edit.Page"
				 title="@LivingMessiah.Web.Links.Wirecast.Title"
				 class="list-group-item list-group-item-warning">
				<b>@LivingMessiah.Web.Links.Wirecast.Title</b>
				<i class="@LivingMessiah.Web.Links.Wirecast.Edit.Icon"></i>
			</a>
		</li>

	</ul>
</div>


## Index.razor.cs
- for got to save, mostly commmented out code


## WeeklyVideos.razor

<YouTubeFeed Url="@SocialMedia.YouTube.YouTubeFeed()" Take="5"/>


## YouTubeFeed.razor
<h3>YouTube Feed</h3>

<TableTemplate Items="YouTubeFeedArgList">
	<TableHeader>
		<th>Title</th>
		<th>Id</th>
		<th>Published Date</th>
		@*<th>Add</th>*@
	</TableHeader>
	<RowTemplate>
		<td>@context.Title</td>
		<td>@context.YouTubeId</td>
		<td>@context.PublishDate</td>

@*
	<td>
		<button class="btn btn-primary" 
				@onclick='() => OnClickYouTubeId(context.Id.Replace("yt:video:",""))'>
				<i class="oi oi-pencil"></i>
			</button>


			<button class="btn btn-primary" 
				@onclick='() => OnClickedRow(context.Id.Replace("yt:video:",""), context.Title.Text, context.PublishDate.Date.ToShortDateString())'>
				<i class="oi oi-pencil"></i>
			</button>
		</td>
		
		
		
*@

	</RowTemplate>
</TableTemplate>



@*


AudioVisual.YouTubeFeed' does not have a property matching the name 'ClickedRow.YouTubeId'.
@onclick='() => OnClickedRow(context.Id.Replace("yt:video:","", context.Title.Text, context.PublishDate.Date.ToShortDateString())'>
@onclick="() => OnClickedRow(context.Id.Replace("yt:video:",""), context.Title.Text), context.PublishDate.Date.ToShortDateString()">

@onclick="() => OnClickedRow(
context.Id.Replace("yt:video:","")
, context.Title.Text)
, context.PublishDate.Date.ToShortDateString()">


<button type="button" class="btn btn-danger" @onclick="() => OnConfirmationChange(true)">
    Delete
</button>



*@

## YouTubeFeed.razor.cs
namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

public partial class YouTubeFeed
{
	[Parameter]
	public string Url { get; set; }

	[Parameter]
	public int Take { get; set; } = 9;

	public List<YouTubeFeedArgs> YouTubeFeedArgList { get; set; }

	protected override async Task OnInitializedAsync()
	{
		await Task.Delay(0);
		using var reader = XmlReader.Create(Url);

		var feed = SyndicationFeed.Load(reader);
		List<SyndicationItem> si = new List<SyndicationItem>();
		si = feed.Items
		.OrderByDescending(x => x.PublishDate)
		.Take(Take)
		.ToList();

		if (si.Any())
		{
			YouTubeFeedArgList = new List<YouTubeFeedArgs>();
			foreach (var item in si)
			{
				YouTubeFeedArgList.Add(new YouTubeFeedArgs()
				{
					YouTubeId = item.Id.Replace("yt:video:", ""),
					Title = item.Title.Text,
					PublishDate =	item.PublishDate.Date.ToShortDateString()
				});
			}
		}

	}

	#region ChildComponent

	//[Parameter]	public EventCallback<string> YouTubeIdSelected { get; set; }
	//[Parameter] public string YouTubeId { get; set; }

	//private Task OnClickYouTubeId(string youTubeId)
	//{
	//	//return YouTubeIdSelected.InvokeAsync(youTubeId);
	//	YouTubeId = youTubeId;
	//	return YouTubeIdSelected.InvokeAsync(YouTubeId);
	//}

	/*
	[Parameter]	public EventCallback<YouTubeFeedArgs> ClickedRow { get; set; }

	[Parameter] public string YouTubeId { get; set; }
	[Parameter] public string Title { get; set; }
	[Parameter] public string PublishDate { get; set; }
	*/

	/*
	public YouTubeFeedArgs args { get; set; }
	protected async Task OnClickedRow(string youTubeId, string title, string publishDate)
	{
		args = new YouTubeFeedArgs { YouTubeId = youTubeId, Title = title, PublishDate = publishDate };
		await ClickedRow.InvokeAsync(args);
	}
	*/

	/*
	public YouTubeFeedArgs args { get; set; }
	private async Task OnClickedRow(string youTubeId, string title, string publishDate)
	{
		//YouTubeId = youTubeId;
		//Title = title;
		//PublishDate = publishDate;
		//return TitleChanged.InvokeAsync(Title);
		//await this.InvokeAsync(???);
		args = new YouTubeFeedArgs { YouTubeId = youTubeId, Title = title, PublishDate = publishDate };
		await ClickedRow.InvokeAsync(args);
		
	}
	*/
	#endregion


}


## YouTubeFeedArgs.cs
namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

public class YouTubeFeedArgs
{
	public string YouTubeId { get; set; }
	public string Title { get; set; }
	public string PublishDate { get; set; }
}



---

# Ancillary

## ServiceCollectionExtensions.cs
- \LivingMessiah.Web\ServiceCollectionExtensions.cs

					.AddTransient<IUpdateGridDataAdaptor, UpdateGridDataAdaptor>()

## Constants.cs
- \LivingMessiah.Web\Links\Constants.cs

public static class Admin
{
	public static class AudioVisual
	{
		public const string Index = "/Admin/AudioVisual/";
		public const string Redirect = "/Admin/AudioVisual";
		public const string Title = "Audio Visual";
		public const string Icon = "fab fa-teamspeak";
		//public const string Icon2 = "fas fa-theater-masks";
		//public const string Icon3 = "fas fa-broadcast-tower";

		//public const string Add = "/Admin/AudioVisual/WeeklyVideoAddForm/";
		public static class Add
		{
			public const string Index = "/Admin/AudioVisual/WeeklyVideoAddForm/";
			public const string Title = "Weekly Video Add";
			public const string Icon = "fas fa-plus";
		}
	}
}
