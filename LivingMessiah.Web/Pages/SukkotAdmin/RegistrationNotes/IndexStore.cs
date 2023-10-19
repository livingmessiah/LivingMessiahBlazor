using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes.Enums;
using System.Linq;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes;

// 1. Action
public record Initialize_List_Action();
public record Set_ListFiltered_Action(Enums.NotesFilter notesFilter);
public record Set_NotesList_Action(List<Notes>? notesList);

public record Set_ShowDetailCard_Action(bool toggle);
public record Set_CurrentFilter_Action(Enums.NotesFilter notesFilter);
public record Set_SelectedNote_Action(Notes? selectedNote);

public record Response_Message_Action(Enums.ResponseMessage MessageType, string Message);

// 2. State
public record State
{
	public bool ShowDetailCard { get; set; }
	public Enums.NotesFilter? CurrentFilter { get; init; }
	public Notes? SelectedNote { get; init; }
	public List<Notes>? NotesList { get; set; }
	public List<Notes>? NotesListFiltered { get; set; }
}

// 3. Feature
public class FeatureImplementation : Feature<State>  // <IndexState>
{
	public override string GetName() => Constants.FluxorStores.Index;

	protected override State GetInitialState()
	{
		return new State
		{
			NotesList = new List<Notes>(),
			NotesListFiltered = new List<Notes>(),
			CurrentFilter = Constants.DefaultFilter, 
			ShowDetailCard = Constants.DefaultShowDetailCard
		};
	}
}

// 4. Reducers
public static class Reducers
{

	[ReducerMethod]
	public static State On_Set_CurrentFilter(State state, Set_CurrentFilter_Action action)
	{
		return state with
		{
			CurrentFilter = action.notesFilter
		};
	}


	[ReducerMethod]
	public static State On_Set_SelectedNote(State state, Set_SelectedNote_Action action)
	{
		return state with
		{
			SelectedNote = action.selectedNote
		};
	}

	[ReducerMethod]
	public static State On_Set_ShowDetailCard(State state, Set_ShowDetailCard_Action action)
	{
		return state with
		{
			ShowDetailCard = action.toggle
		};
	}

	//

	[ReducerMethod]
	public static State On_Set_ListFiltered(State state, Set_ListFiltered_Action action)
	{
		List<Notes>? filteredList = new List<Notes>(); // default;

		switch (action.notesFilter.Name)
		{
			case nameof(NotesFilter.All):
				filteredList = state.NotesList!.OrderBy(o => o.FirstName).ToList();
				break;

			case nameof(NotesFilter.Admin):
				filteredList = state.NotesList!.Where(w => w.HasAdminNotes).OrderBy(o => o.FirstName).ToList();
				break;

			case nameof(NotesFilter.User):
				filteredList = state.NotesList!.Where(w => w.HasUserNotes).OrderBy(o => o.FirstName).ToList();
				break;
		}

		return state with { NotesListFiltered = filteredList! };
	}


	[ReducerMethod]
	public static State On_Set_NotesList(State state, Set_NotesList_Action action)
	{
		return state with
		{
			NotesList = action.notesList
		};
	}


}


public class Effects
{
	#region Constructor and DI
	private readonly ILogger Logger;
	private readonly Data.IRepository db;

	public Effects(ILogger<Effects> logger, Data.IRepository repository)
	{
		Logger = logger;
		db = repository;
	}
	#endregion

	[EffectMethod]
	public async Task Get(Initialize_List_Action action, IDispatcher dispatcher)
	{
		string inside = $"{nameof(Effects)}!{nameof(Get)}";

		Logger.LogDebug(string.Format("Inside {0}", inside));
		try
		{
			List<Notes>? notesList = new List<Notes>();
			notesList = await db!.GetAdminOrUserNotes(Enums.NotesFilter.All);

			if (notesList is not null)
			{
				dispatcher.Dispatch(new Set_NotesList_Action(notesList));
				dispatcher.Dispatch(new Set_ListFiltered_Action(Enums.NotesFilter.Admin));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Info, $"Got notesList from Database, RowCount {notesList.Count}"));
				Logger.LogDebug(string.Format("...{0}; notesList.Count: {1} ", inside, notesList.Count));
			}
			else
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(notesList)));
				dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Warning, $"notesList is null"));
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}
	}
}
