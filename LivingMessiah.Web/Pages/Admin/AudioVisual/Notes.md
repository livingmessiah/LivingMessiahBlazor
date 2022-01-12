# EditGrid.razor

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



