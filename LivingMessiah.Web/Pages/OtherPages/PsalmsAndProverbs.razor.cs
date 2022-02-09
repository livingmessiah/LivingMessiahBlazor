using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Services;
using LivingMessiah.Domain;
using Microsoft.Extensions.Logging;
using System;

namespace LivingMessiah.Web.Pages.OtherPages;

public partial class PsalmsAndProverbs
{
		[Inject]
		public IShabbatWeekService svc { get; set; }

		[Inject]
		public ILogger<PsalmsAndProverbs> Logger { get; set; }

		protected List<vwPsalmsAndProverbs> PsalmsAndProverbsList;

		protected bool LoadFailed;

		protected override async Task OnInitializedAsync()
		{
				try
				{
						LoadFailed = false;
						PsalmsAndProverbsList = await svc.GetPsalmsAndProverbsList();
				}
				catch (Exception ex)
				{
						LoadFailed = true;
						Logger.LogWarning(ex, $"Failed to load page {nameof(PsalmsAndProverbs)}");
				}

		}

}

