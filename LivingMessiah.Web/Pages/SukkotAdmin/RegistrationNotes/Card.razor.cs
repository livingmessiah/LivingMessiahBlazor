﻿using Blazored.Toast.Services;
using LivingMessiah.Web.Pages.SukkotAdmin.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes;

public partial class Card
{
	[Inject] protected ILogger<Card>? Logger { get; set; }
	[Inject] protected ISukkotAdminRepository? db  { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	public const string ResponseMessageFailure = "An invalid operation occurred, contact your administrator";

	protected List<Domain.Notes>? NotesList { get; set; }

	[Parameter, EditorRequired] public required Enums.NotesFilter CurrentFilter { get; set; }

	protected bool TurnSpinnerOff = false;

	protected override async Task OnParametersSetAsync()
	{
		if (CurrentFilter != null)
		{
			Logger!.LogDebug(string.Format("Inside {0}, CurrentFilter: {1}"
				, nameof(Card) + "!" + nameof(OnParametersSetAsync), CurrentFilter.Name));
			try
			{
				NotesList = await db!.GetAdminOrUserNotes(CurrentFilter);
			}
			catch (Exception ex)
			{
				Logger!.LogError(ex, string.Format("...Inside catch of {0}"
					, nameof(Card) + "!" + nameof(OnParametersSetAsync) ));
				Toast!.ShowError(ResponseMessageFailure);
			}
			finally
			{
				TurnSpinnerOff = true;
			}
		}
		else
		{
			Logger!.LogDebug(string.Format("Inside {0}, CurrentFilter: == null "
				, nameof(Card) + "!" + nameof(OnParametersSetAsync)));
		}

	}

}