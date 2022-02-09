using LivingMessiah.Domain;
using LivingMessiah.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Shared;

public partial class YouTubeWeeklyVideo
{
		[Inject]
		public IShabbatWeekCacheService svc { get; set; }

		[Inject]
		public ILogger<YouTubeWeeklyVideo> Logger { get; set; }

		[Parameter]
		public WeeklyVideoType WeeklyVideoType { get; set; }

		public vwCurrentWeeklyVideo CurrentWeeklyVideo;

		protected override async Task OnInitializedAsync()
		{
				try
				{
						Logger.LogDebug(string.Format("Inside {0} WeeklyVideoType:{1}", nameof(YouTubeWeeklyVideo) + "!" + nameof(OnInitialized), (int)WeeklyVideoType));
						CurrentWeeklyVideo = await svc.GetCurrentWeeklyVideoByTypeId((int)WeeklyVideoType);

						if (CurrentWeeklyVideo == null)
						{
								DatabaseWarning = true;
								DatabaseWarningMsg = string.Format("{0} NOT FOUND, WeeklyVideoType:{1}", nameof(CurrentWeeklyVideo), (int)WeeklyVideoType);
						}
				}

				catch (System.Exception ex)
				{
						DatabaseError = true;
						DatabaseErrorMsg = $"Error reading database";
						Logger.LogError(ex, string.Format("...Exception, DatabaseErrorMsg: {0}", DatabaseErrorMsg));
				}

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
