using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using Syncfusion.Blazor.Grids;

namespace LivingMessiah.Web.Pages.BlazorSyncFusion.Grid
{
	public partial class GridDialogTemplate
	{

		[Inject]
		public ILogger<GridDialogTemplate> Logger { get; set; }

		public List<OrdersDetails> GridData { get; set; }
		private Boolean Check = false;
		private DialogSettings DialogParams = new DialogSettings
		{ MinHeight = "400px", Width = "450px" };

		protected override void OnInitialized()
		{
			//Logger.LogDebug(string.Format("Inside {0}", nameof(GridDialogTemplate) + "!" + nameof(OnInitialized)));
			GridData = OrdersDetails.GetAllRecords();
		}

		public void ActionCompleteHandler(ActionEventArgs<OrdersDetails> args)
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

		private SfGrid<OrdersDetails> Grid;
		public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
		{
			if (args.Item.Id == SyncFusionToolbarCRUD.Add.ArgId)		{ await this.Grid.AddRecordAsync(); }
			if (args.Item.Id == SyncFusionToolbarCRUD.Edit.ArgId)		{	await this.Grid.StartEditAsync(); }
			if (args.Item.Id == SyncFusionToolbarCRUD.Delete.ArgId)	{	await this.Grid.DeleteRecordAsync();	}
			if (args.Item.Id == SyncFusionToolbarCRUD.Update.ArgId)
			{
				// update the specified row by given values without changing into edited state.
				//(double index, TValue data)
				//await this.Grid.UpdateRowAsync(); 
			}
			if (args.Item.Id == SyncFusionToolbarCRUD.Cancel.ArgId)	{ await this.Grid.CloseEditAsync();	}
		}


		/*
				public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
				{
					Logger.LogDebug(string.Format("Inside {0} args.Item.Id: {1}", nameof(ToolbarClickHandler), args.Item.Id));

					if (args.Item.Id == SyncFusionToolbarCRUD.Add.ArgId)
					{
						//await this.Grid.AddRecordAsync();
						Logger.LogDebug(string.Format("...args.Item.Text: {0}", args.Item.Text));
					}
					if (args.Item.Id == SyncFusionToolbarCRUD.Edit.ArgId)
					{
						//await this.Grid.StartEditAsync();
						Logger.LogDebug(string.Format("...args.Item.Text: {0}", args.Item.Text));
					}
					if (args.Item.Id == SyncFusionToolbarCRUD.Delete.ArgId)
					{
						//await this.Grid.DeleteRecordAsync();
						Logger.LogDebug(string.Format("...args.Item.Text: {0}", args.Item.Text));
					}
					if (args.Item.Id == SyncFusionToolbarCRUD.Update.ArgId)
					{
						
					//	 update the specified row by given values without changing into edited state.
					//	(double index, TValue data)
					//	await this.Grid.UpdateRowAsync(); 
					
		Logger.LogDebug(string.Format("...args.Item.Text: {0}", args.Item.Text));

			}
			if (args.Item.Id == SyncFusionToolbarCRUD.Cancel.ArgId)
			{
				//await this.Grid.CloseEditAsync();
				Logger.LogDebug(string.Format("...args.Item.Text: {0}", args.Item.Text));
			}

			await Task.Delay(0);
		}
*/

		public async Task ActionBeginHandler(ActionEventArgs<OrdersDetails> arg)
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

	}
}
