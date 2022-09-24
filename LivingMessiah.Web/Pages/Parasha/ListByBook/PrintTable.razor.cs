using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Blazored.Toast.Services;
using Page = LivingMessiah.Web.Links.Parasha;
using LivingMessiah.Web.Pages.Parasha.Services;
using LivingMessiah.Web.Enums;

namespace LivingMessiah.Web.Pages.Parasha.ListByBook;

public partial class PrintTable
{
	[Inject]
	private IParashaService Service { get; set; }

	[Inject]
	public ILogger<PrintTable>? Logger { get; set; }

	[Inject]
	public IToastService Toast { get; set; }

	[Parameter]
	public int BookId { get; set; } = 0;

	protected string BibleBookName = String.Empty;
	protected IReadOnlyList<Parashot> Parashot;
	protected bool TurnSpinnerOff = false;

	protected override async Task OnInitializedAsync()
	{
		Logger!.LogDebug(string.Format("Inside Page: {0}, Class!Method: {1}; BookId: {2}"
			, Page.IndexPrint, nameof(PrintTable) + "!" + nameof(OnInitializedAsync), BookId));

		try
		{
			Parashot = await Service.GetParashotByBookId(BookId);
			if (Parashot is null || !Parashot.Any())
			{
				Toast.ShowWarning(Service.UserInterfaceMessage);
			}
			else
			{
				BibleBookName = BibleBook.FromValue(BookId).Name;
			}
		}

		catch (InvalidOperationException invalidOperationException)
		{
			Toast.ShowError(invalidOperationException.Message);
		}
		finally
		{
			TurnSpinnerOff = true;
		}

	}
}
