using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain;
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data
{


	public class SyncFusionAdaptor : DataAdaptor
	{
		[Inject]
		public IDonationRepository db { get; set; }

		[Inject]
		public ILogger<SyncFusionAdaptor> Logger { get; set; }

		/*
		 * 
		> No parameterless constructor defined for type 'LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data.SyncFusionAdaptor'.

		private DonationRepository db;
		public SyncFusionAdaptor(DonationRepository donationRepository)
		{
			db = donationRepository;
		}
		*/

		public SyncFusionAdaptor()
		{	}

		public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
		{
			Logger.LogDebug($"Inside {nameof(ReadAsync)}");
			IEnumerable<DonationDetail> details = await db.GetDonationDetails(registrationId:0); // ToDo: how to fix this
			int count = details.Count();
			return dataManagerRequest.RequiresCounts ? new DataResult() { Result = details, Count = count } : count;
		}


		public override async Task<object> InsertAsync(DataManager dataManager, object data, string key)
		{
			Logger.LogDebug($"Inside {nameof(InsertAsync)}; key:{key}");
			await db.InsertRegistrationDonation(data as Domain.Donation);  
			return data;
		}

		/*	*/
		public override async Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key)
		{
			Logger.LogDebug($"Inside {nameof(UpdateAsync)}; keyField:{keyField}; key:{key}");
			await db.UpdateDonationDetail(data as Domain.DonationDetail);
			return data;
		}

		public override async Task<object> RemoveAsync(DataManager dataManager, object primaryKeyValue, string keyField, string key)
		{
			Logger.LogDebug($"Inside {nameof(RemoveAsync)}; keyField:{keyField}; key:{key}");
			await db.DeleteDonationDetail(Convert.ToInt32(primaryKeyValue));
			return primaryKeyValue;
		}
	
	}

}
