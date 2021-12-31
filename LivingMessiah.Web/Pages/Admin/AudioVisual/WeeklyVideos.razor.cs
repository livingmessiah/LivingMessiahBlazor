using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using LivingMessiah.Web.Services;
using LivingMessiah.Domain;
using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;
//using Microsoft.AspNetCore.Components.Forms;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual
{
	[Authorize(Roles = Roles.AdminOrAudiovisual)]
	public partial class WeeklyVideos
	{
		const bool IsWorkInProgress = false;

		[Inject]
		public IShabbatWeekService svc { get; set; }

		[Inject]
		public ILogger<WeeklyVideos> Logger { get; set; }

		[Inject]
		NavigationManager NavManager { get; set; }

		protected IReadOnlyList<WeeklyVideoIndex> WeeklyVideoIndex;

		protected bool MakeModalVisible = false;
		protected string CrudOperation = CRUD.Read;
		protected bool CrudOperationFailed = false;

		WeeklyVideoModel WeeklyVideoModel = new WeeklyVideoModel();
		protected int NewId;
		protected int Affectedrows;

		protected override async Task OnInitializedAsync()
		{
			await Read();
		}

		protected async Task HandleValidSubmit()
		{
			//Debug(nameof(HandleValidSubmit) + "-Beg");

			if (IsWorkInProgress != true)
			{

				if (CrudOperation == CRUD.Add)
				{
					await Add();
				}
				else
				{
					if (CrudOperation == CRUD.Edit)
					{
						await Update();
					}
					else
					{
						if (CrudOperation == CRUD.Delete)
						{
							await Delete();
						}
						else
						{
							//Debug(nameof(HandleValidSubmit) + "** Unexpected CrudOperation **");
						}
					}
				}

			}

			await Read();
			MakeModalVisible = false;
			//Debug(nameof(HandleValidSubmit) + "-End");
		}

		// This crap method exists because I can't controll the invalid form scenarios
		private bool IsFormValid(string youTubeId)
		{
			if (!String.IsNullOrEmpty(youTubeId))
			{
				if (youTubeId.Length <= 50)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		#region Service CRUD Calls

		protected async Task Read()
		{
			CrudOperationFailed = false;
			//Debug(nameof(Read) + "-Beg");

			try
			{
				WeeklyVideoIndex = await svc.GetTopWeeklyVideos(3);
			}

			catch (System.Exception ex)
			{
				CrudOperationFailed = true;
				Logger.LogError(ex, $"<br /><br /> {nameof(Read)}");
				NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
			}
			//Debug(nameof(Read) + "-End");
		}

		protected async Task Add()
		{
			if (IsFormValid(WeeklyVideoModel.YouTubeId))
			{
				try
				{
					NewId = 0;
					CrudOperationFailed = false;
					NewId = await svc.WeeklyVideoAdd(WeeklyVideoModel);
				}
				catch (System.Exception ex)
				{
					CrudOperationFailed = true;
					Logger.LogWarning(ex, $"Calling {nameof(svc.WeeklyVideoAdd)}");
					NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
				}
			}
			else
			{
				//Debug(nameof(Add) + "IsFormValid is NOT Valid");
			}


		}

		protected async Task Update()
		{
			if (IsFormValid(WeeklyVideoModel.YouTubeId))
			{
				try
				{
					Affectedrows = 0;
					CrudOperationFailed = false;
					Affectedrows = await svc.WeeklyVideoUpdate(WeeklyVideoModel);
				}
				catch (System.Exception ex)
				{
					CrudOperationFailed = true;
					Logger.LogWarning(ex, $"Calling {nameof(svc.WeeklyVideoUpdate)}");
					NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
				}
			}
			else
			{
				//Debug(nameof(Update) + "IsFormValid is NOT Valid");
			}

		}

		protected async Task Delete()
		{
			try
			{
				Affectedrows = 0;
				CrudOperationFailed = false;
				Affectedrows = await svc.WeeklyVideoDelete(WeeklyVideoModel.Id);
			}
			catch (System.Exception ex)
			{
				CrudOperationFailed = true;
				Logger.LogWarning(ex, $"Calling {nameof(svc.WeeklyVideoDelete)}");
				NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
			}
		}

		#endregion

		void ShowModal_ButtonClick(string buttonName, WeeklyVideoIndex weeklyVideoIndex)
		{
			CrudOperation = buttonName;
			MakeModalVisible = true;

			if (CrudOperation == CRUD.Add)
			{
				WeeklyVideoModel.Id = 0;
			}
			else
			{
				WeeklyVideoModel.Id = (int)weeklyVideoIndex.WeeklyVideoId;
			}

			WeeklyVideoModel.WeeklyVideoTypeId = weeklyVideoIndex.TypeId;
			WeeklyVideoModel.ShabbatWeekId = weeklyVideoIndex.ShabbatWeekId;
			WeeklyVideoModel.YouTubeId = weeklyVideoIndex.YouTubeId;
			WeeklyVideoModel.Title = weeklyVideoIndex.Title;
			WeeklyVideoModel.GraphicFileRoot = weeklyVideoIndex.GraphicFile;
			WeeklyVideoModel.NotesFileRoot = weeklyVideoIndex.NotesFile;
			WeeklyVideoModel.Book = weeklyVideoIndex.Book;  //(int)
			WeeklyVideoModel.Chapter = weeklyVideoIndex.Chapter;
			//Debug(nameof(ShowModal_ButtonClick));
		}

		#region Save and Cancel Button Click
		async Task SaveModal_ButtonClick()
		{
			await HandleValidSubmit();
			MakeModalVisible = false;
			//Debug(nameof(SaveModal_ButtonClick));
		}

		void CancelModal_ButtonClick()
		{
			MakeModalVisible = false;
			CrudOperation = CRUD.Read;
			//Debug(nameof(CancelModal_ButtonClick));
		}
		#endregion

		/*
		const bool IsDebug = false;
		private void Debug(string inside, bool showWeeklyVideoModel = false)
		{
			if (IsDebug)
			{
				Console.WriteLine($@" Inside: {inside};  {nameof(CrudOperation)}: {CrudOperation}; {nameof(MakeModalVisible)}: {MakeModalVisible}.");
				if (WeeklyVideoModel != null && showWeeklyVideoModel)
				{
					Console.WriteLine($@"       ... {nameof(WeeklyVideoModel)}: {WeeklyVideoModel}");
				}
			}
		}
		*/
	}
}

/*
 Need to hook this up with Components\TableTemplate.razor.cs ! [Parameter]	public RenderFragment<TItem> RowTemplate { get; set; }

### Index.razor
<button @onclick="@(() => Edit(WeeklyVideoIndex))" type="button" class="btn btn-warning btn-sm">
	<i class="@Anchors.EditIcon"></i> Edit
</button>

<button @onclick="@(() => Remove(WeeklyVideoIndex))" type="button" class="btn btn-danger btn-sm">
	<i class="@Anchors.DeleteIcon"></i> Delete
</button>

### Index.razor.cs	
void Add(WeeklyVideoIndex item) => Console.WriteLine($"Add ==> Type Id: {item.TypeId}, WeeklyVideoIndexId: {item.WeeklyVideoIndexId}");
void Edit(WeeklyVideoIndex item) => Console.WriteLine($"Edit ==> Type Id: {item.TypeId}, WeeklyVideoIndexId: {item.WeeklyVideoIndexId}, YouTubeId: {item.YouTubeId}");
void Remove(WeeklyVideoIndex item) => Console.WriteLine($"Remove ==>  item: {item.TypeId}, WeeklyVideoIndexId: {item.WeeklyVideoIndexId}, YouTubeId: {item.YouTubeId}");
*/