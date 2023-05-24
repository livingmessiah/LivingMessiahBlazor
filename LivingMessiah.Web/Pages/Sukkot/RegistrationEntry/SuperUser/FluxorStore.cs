using Fluxor;
using LivingMessiah.Web.Enums;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
/*

Show HRA tables with not registration just like  NoRegistrationTable (SukkotAdmin.HouseRulesAgreement)
- Include a Add Button on top
- Include a Delete Button Column

Show Combo GetEmailForm and HRA Modal Agreement Button

Show Registered Table
- Populate Columns with Registrations List<TableDetails>?
- Include a Add Button on top
- Include a Edit/View/Delete Button Action Columns




*/
namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser;

// 1. Action
public record SetId_Action(int Id);  
public record ShowRegisteredTable_Action(bool IsVisible);
public record ShowForm_Action(bool IsVisible);


// 2. State
public record State
{
	public string? SuccessMessage { get; init; }
	public string? WarningMessage { get; init; }
	public string? ErrorMessage { get; init; }

	public int Id { get; init; }
	public List<TableDetails>? Registrations { get; init; } 

	public bool ShowRegisteredTable { get; init; }
	public bool ShowForm { get; init; }
}


// 3. Feature
public class FeatureImplementation : Feature<State>
{
	public override string GetName() => "RegistrationEntry";

	protected override State GetInitialState()
	{
		return new State
		{
			//Id = 1,
			ShowRegisteredTable = false,
			ShowForm = false,
		};
	}
}


// 4. Reducers
public static class Reducers
{
	[ReducerMethod]
	public static State OnSetId(
		State state,
		SetId_Action action)
	{
		return state with { Id = action.Id };
	}

	[ReducerMethod]
	public static State OnShowRegisteredTable(
		State state, ShowRegisteredTable_Action action)
	{
		return state with { ShowRegisteredTable = action.IsVisible };
	}

	[ReducerMethod]
	public static State OnShowForm(
	State state, ShowForm_Action action)
	{
		return state with { ShowForm = action.IsVisible };
	}



	// 5. Effects
	public class Effects
	{
		#region Constructor and DI
		private readonly ILogger Logger;
		private IService svc;

		public Effects(ILogger<Effects> logger, IService service)
		{
			Logger = logger;
			svc = service;
		}
		#endregion

		//[EffectMethod]
	}
}