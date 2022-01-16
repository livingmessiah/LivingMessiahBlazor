using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using LivingMessiah.Web.Pages.UpcomingEvents.Data;
using LivingMessiah.Web.Pages.UpcomingEvents.Edit;
using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.UpcomingEvents.Data
{
	public interface IUpcomingEventsGridDataAdaptor
	{
		Task<object> InsertAsync(DataManager dataManager, object data, string key);
		Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null);
		Task<object> RemoveAsync(DataManager dataManager, object primaryKeyValue, string keyField, string key);
		Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key);
	}

	public class UpcomingEventsGridDataAdaptor	: DataAdaptor, IUpcomingEventsGridDataAdaptor  //GridDataAdaptor : DataAdaptor
	{
		//https://blazor.syncfusion.com/documentation/datagrid/custom-binding
		//https://www.syncfusion.com/forums/160311/is-there-any-other-way-of-injecting-a-service-into-a-blazor-component-other-than-the-inject

		/*
		[Inject]
		protected IGridDataRepository db { get; set; }

		public GridDataAdaptor()
		{
		}
		*/

		//[Microsoft.AspNetCore.Components.Inject]
		//public ILogger<GridDataAdaptor> Logger { get; set; }

		#region Constructor and DI
		//protected readonly ILogger Logger;  // Unable to resolve service for type 'Microsoft.Extensions.Logging.ILogger' while attempting to activate 'LivingMessiah.Web.Data.GridDataAdaptor'.)
		public IGridDataRepository db;
		public UpcomingEventsGridDataAdaptor(IGridDataRepository gridDataRepository)  //, ILogger logger
		{
			db = gridDataRepository;
			//Logger = logger;
		}
		#endregion

		public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
		{
			//System.ArgumentNullException: Value cannot be null. (Parameter 'logger') at Microsoft.Extensions.Logging.LoggerExtensions.Log(ILogger logger,
			//Logger.LogDebug(string.Format("Inside {0}", nameof(GridDataAdaptor) + "!" + nameof(ReadAsync)));

			List<EditVM> recs = await db.GetUpcomingEventsEditList();
			int count = await db.GetUpcomingEventsEditCount();
			//int count = recs.Count;

			//Logger.LogDebug(string.Format("...count:{0}", count));
			return dataManagerRequest.RequiresCounts ? new DataResult() { Result = recs, Count = count } : count;
		}

		/*
				public override async Task<object> InsertAsync(DataManager dataManager, object data, string key)
				{
					await _dataLayer.AddBugAsync(data as Bug);
					return data;
				}
		*/
		public override async Task<object> InsertAsync(DataManager dataManager, object data, string key)
		{
			await db.Create(data as EditVM);
			return data;
		}

		public override async Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key)
		{
			await db.UpdateNonKeyDate(data as EditVM);
			return data;
		}

		public override async Task<object> RemoveAsync(DataManager dataManager, object primaryKeyValue, string keyField, string key)
		{
			await db.RemoveNonKeyDate(Convert.ToInt32(primaryKeyValue));
			return primaryKeyValue;
		}
	}
}
