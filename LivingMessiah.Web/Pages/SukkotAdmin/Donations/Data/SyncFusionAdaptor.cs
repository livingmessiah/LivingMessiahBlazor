using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain;
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Services;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data
{
	public class SyncFusionAdaptor : DataAdaptor
	{
		#region Constructor and DI
		private IDonationRepository db;
		private readonly ILogger<SyncFusionAdaptor> Logger;
		private readonly ISecurityClaimsService SvcClaims;
		public SyncFusionAdaptor(IDonationRepository donationRepository, ILogger<SyncFusionAdaptor> logger, ISecurityClaimsService svcClaims)
		{
			Logger = logger;
			db = donationRepository;
			SvcClaims = svcClaims;
		}
		#endregion

		public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
		{
			Logger.LogDebug($"Inside {nameof(SyncFusionAdaptor)}!{nameof(ReadAsync)}; calling {nameof(db.GetDonationDetailsAll)}");
			IEnumerable<DonationDetail> details = await db.GetDonationDetailsAll();
			int count = details.Count();
			return dataManagerRequest.RequiresCounts ? new DataResult() { Result = details, Count = count } : count;
		}

		public override async Task<object> InsertAsync(DataManager dataManager, object data, string key)
		{
			Logger.LogDebug($"Inside {nameof(SyncFusionAdaptor)}!{nameof(InsertAsync)}; calling {nameof(db.InsertRegistrationDonation)}; key:{key}");
			try
			{
				string email = await SvcClaims.GetEmail();
				if (String.IsNullOrEmpty(email)) email = "test@abc.com";
				Logger.LogDebug($"...email: {email}");
				await db.InsertRegistrationDonation(data as Domain.Donation, email);
			}
			catch (Exception ex)
			{
				string strErrMsg = $"...Error calling {nameof(db.InsertRegistrationDonation)}";
				Logger.LogError(ex, strErrMsg);
				throw new InvalidOperationException(strErrMsg, ex);
				//throw ex;
			}
			
			return data;
		}

		public override async Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key)
		{
			Logger.LogDebug($"Inside {nameof(SyncFusionAdaptor)}! {nameof(UpdateAsync)}; keyField:{keyField}; key:{key}");
			await db.UpdateDonationDetail(data as Domain.DonationDetail);
			return data;
		}

		public override async Task<object> RemoveAsync(DataManager dataManager, object primaryKeyValue, string keyField, string key)
		{
			Logger.LogDebug($"Inside {nameof(SyncFusionAdaptor)}!{nameof(RemoveAsync)}; keyField:{keyField}; key:{key}");
			await db.DeleteDonationDetail(Convert.ToInt32(primaryKeyValue));
			return primaryKeyValue;
		}
	
	}
}

/*
[Inject]
public IDonationRepository db { get; set; }

[Inject]
public ILogger<SyncFusionAdaptor> Logger { get; set; }

> No parameterless constructor defined for type 'LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data.SyncFusionAdaptor'.
public SyncFusionAdaptor()
{	}

 */

//IEnumerable<DonationDetail> details = await db.GetDonationDetails(registrationId:0); // ToDo: how to fix this

/*
Logger.LogDebug($"Inside {nameof(SyncFusionAdaptor)}!{nameof(ReadAsync)}; calling {nameof(db.GetDonationDetails)}");
IEnumerable<DonationDetail> details = await db.GetDonationDetails(registrationId: 3); 
*/
