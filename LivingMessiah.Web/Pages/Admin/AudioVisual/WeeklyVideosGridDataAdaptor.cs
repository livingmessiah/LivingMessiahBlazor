using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual
{
	public interface IWeeklyVideosGridDataAdaptor
	{
		Task<object> InsertAsync(DataManager dataManager, object data, string key);
		Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null);
		Task<object> RemoveAsync(DataManager dataManager, object primaryKeyValue, string keyField, string key);
		Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key);
	}

	public class WeeklyVideosGridDataAdaptor : DataAdaptor, IWeeklyVideosGridDataAdaptor
	{
		#region Constructor and DI
		//protected readonly ILogger Logger;  // Unable to resolve service for type 'Microsoft.Extensions.Logging.ILogger' while attempting to activate 'LivingMessiah.Web.Data.GridDataAdaptor'.)
		public IWeeklyVideosRepository db;
		public WeeklyVideosGridDataAdaptor(IWeeklyVideosRepository weeklyVideosRepository)  //, ILogger logger
		{
			db = weeklyVideosRepository;
			//Logger = logger;
		}
		#endregion

		public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
		{
			//System.ArgumentNullException: Value cannot be null. (Parameter 'logger') at Microsoft.Extensions.Logging.LoggerExtensions.Log(ILogger logger,
			//Logger.LogDebug(string.Format("Inside {0}", nameof(GridDataAdaptor) + "!" + nameof(ReadAsync)));

			List<EditGridVM> recs = await db.GetTopWeeklyVideos();
			//int count = await db.GetUpcomingEventsEditCount();
			int count = recs.Count;

			//Logger.LogDebug(string.Format("...count:{0}", count));
			return dataManagerRequest.RequiresCounts ? new DataResult() { Result = recs, Count = count } : count;
		}


		public override async Task<object> InsertAsync(DataManager dataManager, object data, string key)
		{
			await db.WeeklyVideoAdd(data as EditGridVM);
			return data;
		}

		public override async Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key)
		{
			await db.WeeklyVideoUpdate(data as EditGridVM);
			return data;
		}

		public override async Task<object> RemoveAsync(DataManager dataManager, object primaryKeyValue, string keyField, string key)
		{
			await db.WeeklyVideoDelete(Convert.ToInt32(primaryKeyValue));
			return primaryKeyValue;
		}


	}
}
