using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Services;
using LivingMessiah.Domain;
using Microsoft.Extensions.Logging;
using System;
using Blazored.Toast.Services;
using Page = LivingMessiah.Web.Links.PsalmsAndProverbs;

namespace LivingMessiah.Web.Pages.OtherPages;

public partial class PsalmsAndProverbs
{
	[Inject] public IShabbatWeekService? svc { get; set; }
	[Inject] public ILogger<PsalmsAndProverbs>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	protected List<vwPsalmsAndProverbs>? PsalmsAndProverbsList;

	private const string Message = $"Failed to load page {Page.Index}, Class!Method:{nameof(PsalmsAndProverbs)}!{nameof(OnInitializedAsync)}";
	protected bool TurnSpinnerOff = false;

	protected override async Task OnInitializedAsync()
	{
		try
		{
			PsalmsAndProverbsList = await svc!.GetPsalmsAndProverbsList();
		}
		catch (Exception ex)
		{
			Logger!.LogWarning(ex, Message);
			Toast!.ShowError(Message);
		}
		finally
		{
			TurnSpinnerOff = true;
		}
	}

}

