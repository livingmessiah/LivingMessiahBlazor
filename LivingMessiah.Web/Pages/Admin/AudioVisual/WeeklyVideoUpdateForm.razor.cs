using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Authorization;
using static LivingMessiah.Web.Services.Auth0;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual;

[Authorize(Roles = Roles.AdminOrAudiovisual)]
public partial class WeeklyVideoUpdateForm
{
	[Parameter]
	public int? id { get; set; }

	[Inject]
	public ILogger<WeeklyVideoUpdateForm> Logger { get; set; }

	[Inject]
	public IWeeklyVideosRepository db { get; set; }

	public WeeklyVideoUpdateVM vm { get; set; } = new WeeklyVideoUpdateVM();
	public List<ShabbatWeek> ShabbatWeekList { get; set; }  // Populates EditForm!InputSelect control (id=shabbatWeekId)

	protected override async Task OnInitializedAsync()
	{
		Logger.LogDebug(string.Format("Inside {0}; Id:(1)", nameof(WeeklyVideoUpdateForm) + "!" + nameof(OnInitialized), id));

		int Id2 = id.HasValue ? id.Value : 0;

		try
		{
			vm = await db.GetWeeklyVideoById(Id2);
			
			if (vm is null)
			{
				DatabaseWarning = true;
				DatabaseWarningMsg = $"{nameof(vm)} NOT FOUND";
			}

		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error reading database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}

		await PopulateShabbatWeek();
	}

	#region Shabbat Week Lookup
	private int WeekCount = 3;

	private async Task PopulateShabbatWeek()
	{
		Logger.LogDebug(string.Format("Inside {0}; WeekCount:{1}", nameof(Index) + "!" + nameof(PopulateShabbatWeek), WeekCount));

		try
		{
			ShabbatWeekList = await db.GetShabbatWeekList(WeekCount);

			if (ShabbatWeekList is null)
			{
				DatabaseWarning = true;
				DatabaseWarningMsg = $"{nameof(ShabbatWeekList)} NOT FOUND";
			}

		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error reading database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}
	}

	#endregion

	#region Events
	protected async Task HandleValidSubmit()  
	{
		Logger.LogDebug(string.Format("...{0}", nameof(WeeklyVideoUpdateForm) + "!" + nameof(HandleValidSubmit)));
		int affectedrows = 0;
		WeeklyVideoUpdate dto = new WeeklyVideoUpdate();
		dto.Id = vm.Id;
		dto.ShabbatWeekId = vm.ShabbatWeekId;
		dto.WeeklyVideoTypeId = vm.WeeklyVideoTypeId;
		dto.YouTubeId = vm.YouTubeId;
		dto.Title = vm.Title;
		dto.Book = 0;
		dto.Chapter = 0;

		try
		{
			affectedrows = await db.WeeklyVideoUpdate(dto);
		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error updated database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}

		Logger.LogDebug(string.Format("...newId: {0}", affectedrows));

		DatabaseInformation = true;
		DatabaseInformationMsg = $"Record updated";
	}

	#endregion

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
