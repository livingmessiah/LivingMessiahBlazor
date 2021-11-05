using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Web.Pages.Contacts.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using static LivingMessiah.Web.Services.Auth0;
using Microsoft.AspNetCore.Authorization;

using Syncfusion.Blazor.Grids;

namespace LivingMessiah.Web.Pages.Contacts
{
	[Authorize(Roles = Roles.AdminOrElder)]
	public partial class Index
	{
		[Inject]
		public ILogger<Index> Logger { get; set; }

		[Inject]
		public IContactRepository db { get; set; }

		public IEnumerable<Domain.ContactVM> Contacts { get; set; }

		//private SfGrid<Domain.ContactVM> Grid;

		protected override async Task OnInitializedAsync()
		{
			Logger.LogDebug($"Inside {nameof(Index)}!{nameof(OnInitializedAsync)}");
			try
			{
				Contacts = await db.GetAll();
				if (Contacts == null)
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = "Contacts NOT FOUND";
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
			StateHasChanged();
		}


		#region ErrorHandling

		private void InitializeErrorHandling()
		{
			DatabaseInformationMsg = "";
			DatabaseInformation = false;
			DatabaseWarningMsg = "";
			DatabaseWarning = false;
			DatabaseErrorMsg = "";
			DatabaseError = false;
		}

		protected bool DatabaseInformation = false;
		protected string DatabaseInformationMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }
		protected bool DatabaseError { get; set; } // = false; handled by InitializeErrorHandling
		protected string DatabaseErrorMsg { get; set; }
		#endregion


	}
}
