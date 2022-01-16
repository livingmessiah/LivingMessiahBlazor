using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Enums;
using LivingMessiah.Web.Services;

using Syncfusion.Blazor.Grids;

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

		public List<EditGridVM> GridData { get; set; }

		private DialogSettings DialogParams = new DialogSettings
		{ MinHeight = "400px", Width = "450px" };


		#region Shabbat Week Lookup
		
		private int MinShabbatWeekId;
		private int MaxShabbatWeekId;
		public List<ShabbatWeekLookup> ShabbatWeeks { get; set; }   // int Id, DateTime ShabbatDate 

		private async Task PopulateShabbatWeek() 
		{
			Logger.LogDebug(string.Format("Inside {0} WeekCount:{1}", nameof(EditGrid) + "!" + nameof(OnInitialized), WeekCount));
			Tuple<int, int, List<ShabbatWeekLookup>> ShabbatWeeksTuple;

			try
			{
				ShabbatWeeksTuple = await db.GetShabbatWeekLookup(WeekCount);

				if (ShabbatWeeksTuple is not null)
				{
					MinShabbatWeekId = ShabbatWeeksTuple.Item1;
					MaxShabbatWeekId = ShabbatWeeksTuple.Item2;
					ShabbatWeeks = ShabbatWeeksTuple.Item3;
					Logger.LogDebug(string.Format("...MinShabbatWeekId:{0}/MaxShabbatWeekId:{1}", MinShabbatWeekId, MaxShabbatWeekId));
				}
				else
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = $"{nameof(ShabbatWeeksTuple)} NOT FOUND";
				}

			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
		}
		#endregion

		private int WeekCount = 3;


		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug(string.Format("Inside {0} WeekCount:{1}", nameof(EditGrid) + "!" + nameof(OnInitialized), WeekCount));
			await PopulateShabbatWeek();
		}


		//private bool Check = false;
		public void OnActionBegin(ActionEventArgs<EditGridVM> args)
		{
			if (args.RequestType.ToString() == "Add")
			{
				//Check = true;
				args.Data.ShabbatWeekId = MaxShabbatWeekId;
//				DateTime current = DateTime.Now;
//				args.Data.YearId = current.Year;

			}
			else
			{
				//Check = false;
			}
		}

		private SfGrid<EditGridVM> Grid; 		/* ToDo do I need this?		*/

		#region Dropdowns

		//Book Chapter
		public string BookChapterSelectedValue;
		public int BookChapterSelectedId;
		public int CurrentLastChapter = 150;
		protected List<DropDownListVM> DataSourceBibleBooks => svcDDL.GetBibleBooksVM().ToList();

		public void OnChangeBookChapterDDL(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, DropDownListVM> args)
		{
			int i = int.TryParse(args.ItemData.Value, out i) ? i : 0;
			BookChapterSelectedId = i;
			CurrentLastChapter = BaseBibleBookSmartEnum.FromValue(BookChapterSelectedId).LastChapter;
		}

		//Shabbat Week
		/*
			public string ShabbatWeekSelectedValue;
			public int ShabbatWeekSelectedId;

			public void OnChangeShabbatWeekDDL(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, DropDownListVM> args)
			{
				int i = int.TryParse(args.ItemData.Value, out i) ? i : 0;
				ShabbatWeekSelectedId = i;
			}
	*/
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
			DatabaseErrorMsg = $"Error inside {nameof(Failure)}";  //; e.Error: {e.Error}
			Logger.LogError(string.Format("Inside {0}; e.Error: {1}", nameof(EditGrid) + "!" + nameof(Failure), e.Error));
			DatabaseError = true;
		}
		#endregion

	}
}
