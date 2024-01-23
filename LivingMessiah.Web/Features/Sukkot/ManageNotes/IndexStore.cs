using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Linq;
using ToasterEnums = LivingMessiah.Web.Features.Sukkot.ManageNotes.Toaster;
using LivingMessiah.Web.Features.Sukkot.ManageNotes.Data;

namespace LivingMessiah.Web.Features.Sukkot.ManageNotes;

// 1. Action
public record Initialize_List_Action();
public record Set_ListFiltered_Action(Enums.Filter notesFilter);
public record Set_NotesList_Action(List<NotesQuery>? notesList);

public record Set_ShowDetailCard_Action(bool toggle);
public record Set_CurrentFilter_Action(Enums.Filter notesFilter);
public record Set_SelectedNote_Action(NotesQuery? selectedNote);

public record Response_Message_Action(ToasterEnums.ResponseMessage MessageType, string Message);

// 2. State
public record State
{
	public bool ShowDetailCard { get; set; }
	public Enums.Filter? CurrentFilter { get; init; }
	public NotesQuery? SelectedNote { get; init; }
	public List<NotesQuery>? NotesList { get; set; }
	public List<NotesQuery>? NotesListFiltered { get; set; }
}

// 3. Feature
public class FeatureImplementation : Feature<State>  // <IndexState>
{
	public override string GetName() => Constants.FluxorStores.Index;

	protected override State GetInitialState()
	{
		return new State
		{
			NotesList = new List<NotesQuery>(),
			NotesListFiltered = new List<NotesQuery>(),
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
		List<NotesQuery>? filteredList = new List<NotesQuery>(); // default;

		switch (action.notesFilter.Name)
		{
			case nameof(Enums.Filter.All):
				filteredList = state.NotesList!.OrderBy(o => o.FirstName).ToList();
				break;

			case nameof(Enums.Filter.Admin):
				filteredList = state.NotesList!.Where(w => w.HasAdminNotes).OrderBy(o => o.FirstName).ToList();
				break;

			case nameof(Enums.Filter.User):
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
	private readonly IRepository db;

	public Effects(ILogger<Effects> logger, IRepository repository)
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
			List<NotesQuery>? notesList = new List<NotesQuery>();
			notesList = await db!.GetAdminOrUserNotes(Enums.Filter.All);

			if (notesList is not null)
			{
				dispatcher.Dispatch(new Set_NotesList_Action(notesList));
				dispatcher.Dispatch(new Set_ListFiltered_Action(Enums.Filter.Admin));
				dispatcher.Dispatch(new Response_Message_Action(ToasterEnums.ResponseMessage.Info, $"Got notesList from Database, RowCount {notesList.Count}"));
				Logger.LogDebug(string.Format("...{0}; notesList.Count: {1} ", inside, notesList.Count));
			}
			else
			{
				Logger.LogWarning(string.Format("...{0}; {1} is null", inside, nameof(notesList)));
				dispatcher.Dispatch(new Response_Message_Action(ToasterEnums.ResponseMessage.Warning, $"notesList is null"));
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, string.Format("...Inside catch of {0}", inside));
			dispatcher.Dispatch(new Response_Message_Action(ToasterEnums.ResponseMessage.Failure, Constants.Effects.ResponseMessageFailure));
		}
	}
}
