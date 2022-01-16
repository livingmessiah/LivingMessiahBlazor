
Task<Tuple<int, int, string>> Create(NonKeyDateCrudVM nonKeyDateCrudVM);

# EditGrid.razor


## ToolbarClickHandler Deleted Code
- This logic will be replaced with Adaptor Logic

### EditGrid.razor
```csharp

	<GridEvents OnActionComplete="ActionCompleteHandler" TValue="EditGridVM"
							OnActionBegin="ActionBeginHandler"
							OnActionFailure="Failure"
							OnToolbarClick="ToolbarClickHandler">
	</GridEvents>
```

### EditGrid.razor.cs
```csharp
		
		public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
		{
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
				// update the specified row by given values without changing into edited state.
				//(double index, TValue data)
				//await this.Grid.UpdateRowAsync(); 
			}

			if (args.Item.Id == SyncFusionToolbarCRUD.Cancel.ArgId)
			{
				await this.Grid.CloseEditAsync();
			}
		}

```


- Copied from 


```html
		<Template>
			@{
				var Order = (context as EditGridVM);
			}
			<div>
				<div class="form-row">
					<div class="form-group col-md-6">
						<SfNumericTextBox ID="Id" @bind-Value="@(EditGridVM.Id)" Enabled="@Check"
															FloatLabelType="FloatLabelType.Always" Placeholder="Id">
						</SfNumericTextBox>
					</div>
					<div class="form-group col-md-6">
						<SfAutoComplete TItem="EditGridVM" ID="CustomerID" @bind-Value="@(EditGridVM.CustomerID)"
														TValue="string" DataSource="@GridData"
														FloatLabelType="FloatLabelType.Always" Placeholder="Customer Name">
							<AutoCompleteFieldSettings Value="CustomerID"></AutoCompleteFieldSettings>
						</SfAutoComplete>
					</div>
				</div>

				<div class="form-row">
					<div class="form-group col-md-6">
						<SfNumericTextBox ID="Freight" @bind-Value="@(EditGridVM.Freight)" TValue="double?"
															FloatLabelType="FloatLabelType.Always" Placeholder="Freight"></SfNumericTextBox>
					</div>
					<div class="form-group col-md-6">
						<SfDatePicker ID="OrderDate" @bind-Value="@(EditGridVM.OrderDate)" FloatLabelType="FloatLabelType.Always" Placeholder="Order Date">
						</SfDatePicker>
					</div>
				</div>

				<div class="form-row">
					<div class="form-group col-md-6">
						<SfDropDownList ID="ShipCountry" TItem="EditGridVM" @bind-Value="@(EditGridVM.ShipCountry)"
														TValue="string" DataSource="@GridData" FloatLabelType="FloatLabelType.Always" Placeholder="Ship Country">
							<DropDownListFieldSettings Value="ShipCountry" Text="ShipCountry"></DropDownListFieldSettings>
						</SfDropDownList>
					</div>
					<div class="form-group col-md-6">
						<SfDropDownList ID="ShipCity" TItem="EditGridVM" @bind-Value="@(EditGridVM.ShipCity)"
														TValue="string" DataSource="@GridData" FloatLabelType="FloatLabelType.Always" Placeholder="Ship City">
							<DropDownListFieldSettings Value="ShipCity" Text="ShipCity"></DropDownListFieldSettings>
						</SfDropDownList>
					</div>
				</div>

				<div class="form-row">
					<div class="form-group col-md-12">
						<SfTextBox ID="ShipAddress" @bind-Value="@(EditGridVM.ShipAddress)" FloatLabelType="FloatLabelType.Always"
											 Placeholder="Ship Address"></SfTextBox>
					</div>
				</div>

			</div>
		</Template>

		...


	<GridColumns>
		<GridColumn Field=@nameof(EditGridVM.RowNum) HeaderText="Row #" IsPrimaryKey="true"
								ValidationRules="@(new Syncfusion.Blazor.Grids.ValidationRules{ Required=true, Number=true})"
								TextAlign="@TextAlign.Center" HeaderTextAlign="@TextAlign.Center" Width="140"></GridColumn>

		<GridColumn Field=@nameof(EditGridVM.CustomerID) HeaderText="Customer Name"
								ValidationRules="@(new Syncfusion.Blazor.Grids.ValidationRules{ Required=true})" Width="150"></GridColumn>
		<GridColumn Field=@nameof(EditGridVM.Freight) HeaderText="Freight" Format="C2" Width="140"
								ValidationRules="@(new Syncfusion.Blazor.Grids.ValidationRules{ Required=true, Range = new double[]{1, 1000}})"
								TextAlign="@TextAlign.Right" HeaderTextAlign="@TextAlign.Right"></GridColumn>
		<GridColumn Field=@nameof(EditGridVM.OrderDate) HeaderText="Order Date" Format="d"
								TextAlign="TextAlign.Right" Type="ColumnType.Date" Width="160"></GridColumn>
		<GridColumn Field=@nameof(EditGridVM.ShipCountry) HeaderText="Ship Country" Width="150"></GridColumn>

	</GridColumns>
```

# WeeklyVideos.razor.cs
- LivingMessiah.Web\Pages\Admin\AudioVisual\WeeklyVideos.razor.cs

```csharp
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
```







# EditGrim.cs

- This of was an older version but with out the annotations
- The guts of it was latter replaced with LivingMessiah.Domain/WeeklyVideoModel.cs
-
```csharp
using System;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual
{
	// Was WeeklyVideoIndex
	public class EditGridVM
	{
		public int TypeId { get; set; }
		public string Descr { get; set; }
		public int ShabbatWeekId { get; set; }
		public int RowNum { get; set; }
		public DateTime ShabbatDate { get; set; }
		public int? WeeklyVideoId { get; set; }
		public string YouTubeId { get; set; }
		public string Title { get; set; }
		public string GraphicFile { get; set; }
		public string NotesFile { get; set; }
		public int Book { get; set; }
		public int Chapter { get; set; }

		public override string ToString()
		{
			return $"Type Id: {TypeId}, ShabbatWeekId: {ShabbatWeekId}, YouTubeId: {YouTubeId}"; //, WeeklyVideoType: {WeeklyVideoType}
		}


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

	}
}
```

# WeeklyVideosRepository.cs
- LivingMessiah.Web\Pages\Admin\AudioVisual\WeeklyVideosRepository.cs

```
		Task<IReadOnlyList<EditGridVM>> GetTopWeeklyVideos(int top);

		// Used by Pages\UpcomingEvents\Index.razor(17):<RegularVideoedEvents/>	
		Task<IReadOnlyList<vwCurrentWeeklyVideo>> GetCurrentWeeklyVideos(int daysOld);

		public async Task<IReadOnlyList<EditGridVM>> GetTopWeeklyVideos(int top
		
```



# Deleted Code
						<p><small>VM.WeeklyVideoTypeEnum: @VM.WeeklyVideoTypeEnum</small></p>

						<label for="title" class="col-md-3 col-form-label">Title:</label>
						<InputText id="title" class="form-control" @bind-Value="VM.Title" />
						<ValidationMessage For="@(() => VM.Title)" />


												@*<label for="weeklyVideoTypeEnum" class="control-label">Weekly Event Type</label>
		<InputSelectEnum @bind-Value="VM.WeeklyVideoTypeEnum" class="form-control" id="weeklyVideoTypeEnum" />
		<ValidationMessage For="@(() => VM.WeeklyVideoTypeEnum)" />*@

				//private WeeklyVideoTypeEnum weeklyVideoTypeEnum; // = WeeklyVideoTypeEnum.InDepthStudy;



										@if (Check)
						{
							<p>Editing Record @VM.Id</p>
						}
						else
						{
							<p>Adding New Record</p>
						}


		//private Boolean Check = false;
		private bool Check = false;
		private string HeaderMsg = "";
		public void ActionCompleteHandler(ActionEventArgs<EditGridVM> args)
		{
			Logger.LogDebug(string.Format("...Inside {0} Begin, Check :{1}", nameof(ActionCompleteHandler), Check));
			if (args.RequestType.ToString() == "Add")
			{
				Check = true;
				HeaderMsg = "Adding new record";
			}
			else
			{
				Check = false;
				HeaderMsg = "Editing record ";
			}
			Logger.LogDebug(string.Format("...Inside {0} End, Check :{1}", nameof(ActionCompleteHandler), Check));
		}

				<div class="form-row">
					<div class="form-group col-md-12">
						<p class="text-info">@HeaderMsg</p>
					</div>
				</div>


						//Task<List<ShabbatWeekLookup>> GetShabbatWeekLookup(int top);
//public async Task<List<ShabbatWeekLookup>> GetShabbatWeekLookup(int top) 



				//string employee_name = employees.FirstOrDefault(a => a.EmployeeNumber == 20000)?.FirstName;
				//MaxId = rows.LastOrDefault(a => a.Id).Id;
				//MaxId = rows.LastOrDefault().Id?;
				//PhoneNumber = x.PhoneNumbers.Select(x => x.PhoneNumber).FirstOrDefault() ?? "";
				//MaxId = rows.Select(x => x.Id).LastOrDefault()?? 0;





						<SfDropDownList TItem="DropDownListVM" TValue="string"
											Placeholder="Select a book" PopupHeight="230px"
											@bind-Value="@BookChapterSelectedValue" DataSource="@DataSourceBibleBooks">
				<DropDownListEvents TItem="DropDownListVM" TValue="string" ValueChange="OnChangeBookChapterDDL"></DropDownListEvents>
				<DropDownListFieldSettings Text="Text" Value="Value"></DropDownListFieldSettings>
			</SfDropDownList>





						@*<label for="ShabbatWeekId" class="control-label">Shabbat Week Id</label>
						<InputNumber id="ShabbatWeekId" class="form-control" 
												 @bind-Value="VM.ShabbatWeekId"  />
						<ValidationMessage For="@(() => VM.ShabbatWeekId)" />*@




@*<SfDropDownList ID="ShabbatWeekId" TItem="EditGridVM"
									@bind-Value="@(VM.ShabbatWeekIdString)"
									TValue="string" DataSource="@GridData"
									FloatLabelType="FloatLabelType.Always" Placeholder="Shabbat Week">
		<DropDownListFieldSettings Value="ShabbatWeekLookup" Text="ShabbatWeekLookup"></DropDownListFieldSettings>
	</SfDropDownList>*@


		/*
		public async Task<IReadOnlyList<WeeklyVideoIndex>> GetTopWeeklyVideos(int top)
		{
			return await db.GetTopWeeklyVideos(top);
		}

		public int CurrentLastChapter = 150;
		*/


--, CASE 
--   WHEN tvf.WeeklyVideoTypeId = 1 or tvf.WeeklyVideoTypeId = 2 -- MS Eng/Esp
--	   THEN wv.Title
--	   ELSE '' -- ELSE 'Use Book/Chapter' 
--	END AS Title

--, wv.GraphicFile , wv.NotesFile




		/*
		public async Task ActionBeginHandler(ActionEventArgs<EditGridVM> arg)
		{
			if (arg.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
			{
				if (arg.Action == "Add")
				{

					//arg.Data.ID_CONTACT = CurContact.ID_CONTACT;
					//await _db.InsertContactEvent(arg.Data);
					//System.Console.WriteLine("Insert.");
					int NewId = 0;
					//NewId = await svc.WeeklyVideoAdd(WeeklyVideoModel);
					NewId = await db.
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

		}


		#region Service CRUD Calls

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

		//protected async Task Read()
		//{
		//	CrudOperationFailed = false;
		//	//Debug(nameof(Read) + "-Beg");

		//	try
		//	{
		//		WeeklyVideoIndex = await svc.GetTopWeeklyVideos(3);
		//	}

		//	catch (System.Exception ex)
		//	{
		//		CrudOperationFailed = true;
		//		Logger.LogError(ex, $"<br /><br /> {nameof(Read)}");
		//		NavManager.NavigateTo(LivingMessiah.Web.Links.Home.Error);
		//	}
		//	//Debug(nameof(Read) + "-End");
		//}

		#endregion
		*/



		/*

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug(string.Format("Inside {0} WeekCount:{1}", nameof(EditGrid) + "!" + nameof(OnInitialized), WeekCount));

			try
			{
				GridData = await db.GetTopWeeklyVideos(WeekCount);

				if (GridData is not null)
				{
					Logger.LogDebug(string.Format("...GridData.Count:{0}", GridData.Count));
					await PopulateShabbatWeek();
					Logger.LogDebug(string.Format("...MinShabbatWeekId:{0}/MaxShabbatWeekId:{1}", MinShabbatWeekId, MaxShabbatWeekId));
				}
				else
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = $"{nameof(GridData)} NOT FOUND";
				}

			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
		}
		*/
