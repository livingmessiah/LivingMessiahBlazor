﻿using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Features.Sukkot.ManageRegistration.Enums;
using System.Threading.Tasks;

namespace LivingMessiah.Web.Features.Sukkot.ManageRegistration.MasterDetail;

public partial class CrudButtons
{
	[Parameter, EditorRequired] public Crud? ParmCrud { get; set; }

	[Parameter] public string? EMail { get; set; } // Required for Add
	[Parameter, EditorRequired] public int Id { get; set; } // Use 0 for Add and Repopulate
	[Parameter] public string? FullName { get; set; } // Required for Donation
	[Parameter] public int DonationRowCount { get; set; }

	[Parameter] public EventCallback<CrudAndIdArgs> OnCrudActionSelected { get; set; }

	private async Task OnButtonClicked()  //Crud? crud
	{
		CrudAndIdArgs args = new CrudAndIdArgs
		{
			Crud = ParmCrud!,  // Crud = crud!,
			EMail = EMail ?? "???",
			Id = Id,
			FullName = FullName ?? "???"
		};
		await OnCrudActionSelected.InvokeAsync(args);
	}

	private string Title => Id == 0 ? "" : $"Id: {Id}";
}
