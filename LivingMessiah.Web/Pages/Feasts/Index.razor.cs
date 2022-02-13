namespace LivingMessiah.Web.Pages.Feasts;

using LivingMessiah.Web.LinkSmartEnums;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Page = Links.Feast;

public partial class Index
{
	[Inject]
	public ILogger<Index> Logger { get; set; }

	protected List<Feast> Feasts { get; set; }

	protected override void OnInitialized()
	{
		Logger.LogDebug(string.Format("Inside Page: {0}, Class!Method: {1}", Page.Index, nameof(Index) + "!" + nameof(OnInitializedAsync)));
		try
		{
			Feasts = Feast.List.OrderBy(o => o.Value).ToList();
			if (Feasts is null)
			{
				DatabaseWarning = true;
				DatabaseWarningMsg = "Feasts NOT FOUND";
			}
		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error reading database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}

	}

	// protected override async Task OnInitializedAsync() 	{ }

	#region ErrorHandling
	//protected bool DatabaseInformation = false;
	//protected string DatabaseInformationMsg { get; set; }
	protected bool DatabaseError { get; set; } = false;
	protected string DatabaseErrorMsg { get; set; }
	protected bool DatabaseWarning = false;
	protected string DatabaseWarningMsg { get; set; }

	#endregion

}


