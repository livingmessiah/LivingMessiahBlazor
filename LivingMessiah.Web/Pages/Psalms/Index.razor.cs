using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using LivingMessiah.Domain;
using Microsoft.Extensions.Logging;
using System;
using LivingMessiah.Data;

namespace LivingMessiah.Web.Pages.Psalms;

public partial class Index
{
	[Inject] public IShabbatWeekRepository? db { get; set; }
	[Inject] public ILogger<Index>? Logger { get; set; }

	protected Status _status;
	protected string _msg = string.Empty;

	protected List<PsalmsVM>? PsalmsList;

	protected override async Task OnInitializedAsync()
	{
		try
		{
			_status = Status.Loading;
			PsalmsList = await db!.GetPsalms();
			_status = Status.Loaded;
		}
		catch (Exception ex)
		{
			_status = Status.Error;
			_msg = $"Error calling {nameof(db.GetPsalms)}"; 
			Logger!.LogError(ex, $"Failed to load page {nameof(Index)}");
		}
	}

	PsalmsVM? SelectedPsalms = new PsalmsVM();
	bool modalOpen = false;
	string modalClass = "modal";


	void ShowModal(PsalmsVM item)
	{
		SelectedPsalms = item;
		modalOpen = true;
		modalClass += " show";
		StateHasChanged();
	}

	void CloseModal()
	{
		SelectedPsalms = null; // I need to check for 
		modalOpen = false;
		modalClass = "modal";
	}

	//void Save()
	//{
	//	Logger.LogDebug(string.Format("Event: {0} clicked", nameof(Index) + "!" + nameof(Save)));
	//	// Do save logic if your doing edit/save stuff
	//	CloseModal();
	//}

}
