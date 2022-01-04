using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using LivingMessiah.Web.Pages.UpcomingEvents.Queries;
using LivingMessiah.Web.Pages.UpcomingEvents.Data;
using Markdig;

namespace LivingMessiah.Web.Pages.UpcomingEvents
{
	public partial class Index
	{
		[Inject]
		public IUpcomingEventsRepository db { get; set; }

		[Inject]
		public ILogger<Index> Logger { get; set; }

		protected List<UpcomingEvent> UpcomingEventList;

		protected bool DatabaseError { get; set; } = false;
		protected string DatabaseErrorMsg { get; set; }
		protected bool DatabaseWarning = false;
		protected string DatabaseWarningMsg { get; set; }
		protected MarkdownPipeline pipeline { get; set; }

		protected override async Task OnInitializedAsync()
		{
			try
			{
				Logger.LogDebug(string.Format("Inside {0} i:{1}", nameof(Index) + "!" + nameof(OnInitializedAsync), 0));

				//ToDo: Instead of using a service, use LazyCache (https://github.com/alastairtree/LazyCache) to cache this content
				UpcomingEventList = await db.GetEvents(daysAhead: 100, daysPast: -100);
				if (UpcomingEventList is not null)
				{
					Logger.LogDebug($"...UpcomingEventList.Count:{UpcomingEventList.Count}");
					pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
				}
				else
				{
					DatabaseWarning = true;
					DatabaseWarningMsg = $"{nameof(UpcomingEventList)} NOT FOUND";
				}
			}
			catch (Exception ex)
			{
				DatabaseError = true;
				DatabaseErrorMsg = $"Error reading database";
				Logger.LogError(ex, $"...{DatabaseErrorMsg}");
			}
		}


	}
}

