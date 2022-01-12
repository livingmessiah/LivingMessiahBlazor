using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Enums;
using LivingMessiah.Web.Services;

using Syncfusion.Blazor.Grids;
using System.Linq;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual
{
	public partial class EditGrid
	{
		[Inject]
		public ILogger<EditGrid> Logger { get; set; }

		[Inject]
		public IWeeklyVideosRepository db { get; set; }

		[Inject]

		ISmartEnumServiceForSfDropDownList svcDDL { get; set; }

		public List<ShabbatWeekLookup> ShabbatWeekLookup { get; set; }   // int Id, DateTime ShabbatDate 

		public List<EditGridVM> GridData { get; set; }

		private DialogSettings DialogParams = new DialogSettings
		{ MinHeight = "400px", Width = "450px" };

		/*
		public async Task<IReadOnlyList<WeeklyVideoIndex>> GetTopWeeklyVideos(int top)
		{
			return await db.GetTopWeeklyVideos(top);
		}
		*/

		private int WeekCount = 3;
		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug(string.Format("Inside {0} WeekCount:{WeekCount}", nameof(EditGrid) + "!" + nameof(OnInitialized), WeekCount));
			GridData = await db.GetTopWeeklyVideos(WeekCount);
			ShabbatWeekLookup = await db.GetShabbatWeekLookup(WeekCount);
		}

		private Boolean Check = false;
		public void ActionCompleteHandler(ActionEventArgs<EditGridVM> args)
		{
			if (args.RequestType.ToString() == "Add")
			{
				Check = true;
			}
			else
			{
				Check = false;
			}
		}

		private SfGrid<EditGridVM> Grid;
		public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
		{
			if (args.Item.Id == SyncFusionToolbarCRUD.Add.ArgId) { await this.Grid.AddRecordAsync(); }
			if (args.Item.Id == SyncFusionToolbarCRUD.Edit.ArgId) { await this.Grid.StartEditAsync(); }
			if (args.Item.Id == SyncFusionToolbarCRUD.Delete.ArgId) { await this.Grid.DeleteRecordAsync(); }
			if (args.Item.Id == SyncFusionToolbarCRUD.Update.ArgId)
			{
				// update the specified row by given values without changing into edited state.
				//(double index, TValue data)
				//await this.Grid.UpdateRowAsync(); 
			}
			if (args.Item.Id == SyncFusionToolbarCRUD.Cancel.ArgId) { await this.Grid.CloseEditAsync(); }
		}

		public async Task ActionBeginHandler(ActionEventArgs<EditGridVM> arg)
		{
			if (arg.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
			{
				await Task.Delay(0);
				/*
				 
						if (arg.Action == "Add")
						{
							arg.Data.ID_CONTACT = CurContact.ID_CONTACT;
							await _db.InsertContactEvent(arg.Data);
							System.Console.WriteLine("Insert.");
						}
						else if (arg.Action == "Edit")
						{
							await _db.UpdateContactEvent(arg.Data);
							System.Console.WriteLine("Update.");
						}
					}
					if (arg.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Delete))
					{
						await _db.DeleteContactEvent(arg.Data);
					}
				*/
			}
		}

		#region Book/Chapter Dropdown

		public string BookChapterSelectedValue;
		public int BookChapterSelectedId;
		public int CurrentLastChapter = 150;
		protected List<DropDownListVM> DataSourceBibleBooks => svcDDL.GetBibleBooksVM().ToList(); 

		public void OnChange(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, DropDownListVM> args)
		{
			int i = int.TryParse(args.ItemData.Value, out i) ? i : 0;
			BookChapterSelectedId = i;
			CurrentLastChapter = BaseBibleBookSmartEnum.FromValue(BookChapterSelectedId).LastChapter;
		}
		
		
		#endregion

		//private WeeklyVideoTypeEnum weeklyVideoTypeEnum; // = WeeklyVideoTypeEnum.InDepthStudy;

	}
}
