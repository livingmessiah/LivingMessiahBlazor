using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data; // using SukkotApi.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Domain;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Services;

using LivingMessiah.Web.Services;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Buttons;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations
{
	public partial class DonationsGridCmdColPopup
	{
		[Inject]
		public ILogger<DonationsGridCmdColPopup> Logger { get; set; }

		[Inject]
		public IDonationRepository db { get; set; }

		private SfGrid<DonationDetail> grid;

		public List<DonationDetail> DonationDetails { get; set; }

		public bool IsVisible { get; set; } = false;
		public DonationDetail RowDetails { get; set; }
		private string Xvalue = "center";
		private string Yvalue = "center";

		public void OnCommandClicked(CommandClickEventArgs<DonationDetail> args)
		{
			Logger.LogDebug($"Inside {nameof(DonationsGridCmdColPopup)}!{nameof(OnCommandClicked)}");
			RowDetails = args.RowData;
			IsVisible = true;
		}

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(DonationsGridCmdColPopup)}!{nameof(OnInitializedAsync)}");
			try
			{
				DonationDetails = await db.GetDonationDetailsAll();
				if (DonationDetails == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = $"{nameof(DonationDetails)} NOT FOUND";
					Logger.LogDebug($"{nameof(DonationDetails)} is null, Sql:{db.BaseSqlDump}");
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}

		}

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }

	}
}
