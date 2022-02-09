using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using LivingMessiah.Web.Services;
using LivingMessiah.Domain;

namespace LivingMessiah.Web.Pages.ArchivedVideos;

public partial class Index
{
		[Inject]
		public IShabbatWeekService svc { get; set; }

		[Inject]
		public ILogger<Index> Logger { get; set; }

		[Parameter]
		public int Top { get; set; } = 10;

		protected bool ReadOperationFailed = false;
		protected IReadOnlyList<WeeklyVideoIndex> ArchivedVideos;


		protected override async Task OnInitializedAsync()
		{
				try
				{
						ReadOperationFailed = false;
						ArchivedVideos = await svc.GetTopWeeklyVideos(Top);
				}

				catch (System.Exception ex)
				{
						ReadOperationFailed = true;
						Logger.LogError(ex, $"<br /><br /> {nameof(OnInitializedAsync)}");
				}

		}
}
