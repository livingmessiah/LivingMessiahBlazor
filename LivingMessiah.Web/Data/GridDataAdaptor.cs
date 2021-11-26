using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using LivingMessiah.Web.Pages.UpcomingEvents.Data;
using LivingMessiah.Web.Pages.UpcomingEvents;
//using Microsoft.AspNetCore.Components;


namespace LivingMessiah.Web.Data
{
	public class GridDataAdaptor : DataAdaptor
	{
		//https://blazor.syncfusion.com/documentation/datagrid/custom-binding
		//https://www.syncfusion.com/forums/160311/is-there-any-other-way-of-injecting-a-service-into-a-blazor-component-other-than-the-inject

		/*
		[Inject]
		protected IGridDataRepository dbUpcomingEvents { get; set; }
		public GridDataAdaptor()
		{
		}
		*/
		public GridDataRepository db;
		public GridDataAdaptor(GridDataRepository gridDataRepository)
		{
			db = gridDataRepository;
		}

		private int RowsAffected = 0;


		public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
		{
			List<NonKeyDateCrudVM> recs = await db.GetNonKeyDataCrudList();
			int count = await db.GetNonKeyDataCrudCount();
			//int count = recs.Count;
			return dataManagerRequest.RequiresCounts ? new DataResult() { Result = recs, Count = count } : count;
		}
		
		public override async Task<object> InsertAsync(DataManager dataManager, object data, string key)
		{
			await db.Create(data as NonKeyDateCrudVM);
			return data;
		}

		public override async Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key)
		{
			await db.UpdateNonKeyDate(data as NonKeyDateCrudVM);
			return data;
		}

		public override async Task<object> RemoveAsync(DataManager dataManager, object primaryKeyValue, string keyField, string key)
		{
			await db.RemoveNonKeyDate(Convert.ToInt32(primaryKeyValue));
			return primaryKeyValue;
		}
	
	}
}
