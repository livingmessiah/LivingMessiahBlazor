using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using Blazored.Toast.Services;
using Page = LivingMessiah.Web.Links.PsalmsAndProverbs;

namespace LivingMessiah.Web.Pages.PsalmsAndProverbs;

public partial class Index
{
	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }
	[Inject] public IRepository? db { get; set; }

	protected List<vwPsalmsAndProverbs>? PsalmsAndProverbsList;

	readonly string inside = $"page {Page.Index}; class: {nameof(Index)}";
	
	protected bool TurnSpinnerOff = false;
	//protected LoadingStatusEnum _status;  // Note needed because I'm using <`LoadingComponent ...`

	protected override async Task OnInitializedAsync()
	{
		try
		{
			//_status = LoadingStatusEnum.Loading;
			PsalmsAndProverbsList = await db!.GetPsalmsAndProverbsList();
			//_status = LoadingStatusEnum.Loaded;
			if (PsalmsAndProverbsList == null)
			{
				Toast!.ShowWarning("Psalms and Proverbs List NOT FOUND");
			}
		}
		catch (Exception ex)
		{
			//_status = LoadingStatusEnum.Error;
			Logger!.LogError(ex, string.Format("...Inside catch of {0}", inside + "!" + nameof(OnInitializedAsync)));
			Toast!.ShowError($"{Global.ToastShowError}; inside: {inside}!{nameof(OnInitializedAsync)}");
		}
		finally
		{
			TurnSpinnerOff = true;
		}
	}

}

