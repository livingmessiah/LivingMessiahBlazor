namespace LivingMessiah.Web.Pages.Admin.AudioVisual.Components;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

public interface IWeekCrudGridDA
{
	Task<object> InsertAsync(DataManager dataManager, object data, string key);
	Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null);
	Task<object> RemoveAsync(DataManager dataManager, object primaryKeyValue, string keyField, string key);
	Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key);
}

public class WeekCrudGridDA : DataAdaptor, IWeekCrudGridDA
{
	#region Constructor and DI
	public IWeeklyVideosRepository db;
	public WeekCrudGridDA(IWeeklyVideosRepository weeklyVideosRepository)  
	{
		db = weeklyVideosRepository;
	}
	#endregion

	public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
	{
		List<WeekCrudGridVM> recs = await db.GetTopWeeklyVideos();
		int count = recs.Count;
		return dataManagerRequest.RequiresCounts ? new DataResult() { Result = recs, Count = count } : count;
	}

	public override async Task<object> InsertAsync(DataManager dataManager, object data, string key)
	{
		await db.WeeklyVideoAdd(data as WeekCrudGridVM);
		return data;
	}

	public override async Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key)
	{
		await db.WeeklyVideoUpdate(data as WeekCrudGridVM);
		return data;
	}

	public override async Task<object> RemoveAsync(DataManager dataManager, object primaryKeyValue, string keyField, string key)
	{
		await db.WeeklyVideoDelete(Convert.ToInt32(primaryKeyValue));
		return primaryKeyValue;
	}


}
