using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain;
using LivingMessiah.Web.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations.Services
{
	public interface IDonationService
	{
		Task<int> InsertRegistrationDonation(DonationInsertModel donation);
	}
	
	public class DonationService : IDonationService
	{
		#region Constructor and DI
		private readonly IDonationRepository db;
		private readonly ILogger log;
		private readonly ISecurityClaimsService svcClaims;

		public DonationService(
			IDonationRepository dbRepository, ILogger<DonationService> logger, ISecurityClaimsService serviceClaims)
		{
			db = dbRepository;
			log = logger;
			svcClaims = serviceClaims;
		}
		#endregion

		public string ExceptionMessage { get; set; } = "";

		public async Task<int> InsertRegistrationDonation(DonationInsertModel donationInsertModel)
		{
			int count = 0;
			string email = await svcClaims.GetEmail();
			try
			{
				//count = await db.InsertRegistrationDonation(DTO(donationInsertModel, email));
				count = await db.InsertRegistrationDonation(DTO(donationInsertModel, email), email);
			}
			catch (Exception ex)
			{
				ExceptionMessage = $"Inside {nameof(InsertRegistrationDonation)}, {nameof(db.InsertRegistrationDonation)}";
				log.LogError(ex, ExceptionMessage); // , donation.ToString()
				ExceptionMessage += ex.Message ?? "-- ex.Message was null --";
				throw new InvalidOperationException(ExceptionMessage);
			}
			return count;
		}

		private Donation DTO(DonationInsertModel donationInsertModel, string email)
		{
			Donation poco = new Donation
			{
				RegistrationId = donationInsertModel.RegistrationId,
				Amount = donationInsertModel.Amount,
				Notes = donationInsertModel.Notes,
				ReferenceId = donationInsertModel.ReferenceId,
				CreateDate = donationInsertModel.CreateDate,
				CreatedBy = email
			};
			return poco;
		}


	}
}
