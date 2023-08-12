using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using Microsoft.Extensions.Logging;

using ParentState = LivingMessiah.Web.Pages.Admin.Video.Index;
using LivingMessiah.Web.Enums;
using System.Collections.Generic;
using System.Linq;
using LivingMessiah.Web.Pages.Admin.Video.Models;

namespace LivingMessiah.Web.Pages.Admin.Video.AddEdit;

public partial class Form
{
	[Inject] public ILogger<Form>? Logger { get; set; }
	[Inject] private IState<AddEditState>? State { get; set; }
	[Inject] public IDispatcher? Dispatcher { get; set; }

	private FormVM? VM => State!.Value.FormVM; // Should this be a Parameter?
	private YouTubeFeed? YouTubeFeed  => State!.Value.YouTubeFeed;

	private FluentValidationValidator? _fluentValidationValidator;

	private int bookId = 0;
	private List<BibleBook> Books = new();
	private List<int> Chapters = new();
	
	string inside = $"Inside Admin.Video.AddEdit!{nameof(Form)}";

	protected override void OnInitialized()
	{
		Logger!.LogDebug(string.Format("{0}!{1}", inside, nameof(OnInitialized)));

		if (State!.Value.FormMode!.Value == Enums.FormMode.Add)
		{
			VM!.YouTubeId = YouTubeFeed!.YouTubeId;
			VM!.Title = YouTubeFeed!.Title;
		}

		if (State!.Value.ShabbatWeekList is null)
		{
			Logger!.LogDebug(string.Format("...Call {0} because ShabbatWeekList is null", nameof(DB_Populate_ShabbatWeekList)));
			Dispatcher!.Dispatch(new DB_Populate_ShabbatWeekList());
		}

		Logger!.LogDebug(string.Format("...VM.Book: {0}", VM!.Book));
		if (VM!.Book != 0)  
		{
			bookId = VM!.Book;
			LoadChaptersFilteredByBook(bookId);
		}
		Books = BibleBook.List.OrderBy(o => o.Value).ToList();

		base.OnInitialized();
	}

	private void BookHasChanged(int id)
	{
		Logger!.LogDebug(string.Format("...Inside {0}; id: {1}", nameof(BookHasChanged), id ));
		
		bookId = id;
		VM!.Book = id;

		VM!.Chapter = 0;
		if (id == 0)
		{
			Chapters.Clear();
		}
		else
		{
			LoadChaptersFilteredByBook(id);
		}
	}

	private void LoadChaptersFilteredByBook(int id)
	{
		Chapters = Enumerable.Range(1, BibleBook.FromValue(id).LastChapter).ToList();
	}

	protected void HandleValidSubmit()
	{
		Logger!.LogDebug(string.Format("Inside {0}, FormMode: {1}"
			, nameof(Form) + "!" + nameof(HandleValidSubmit), State!.Value.FormMode!.Name));
		Dispatcher!.Dispatch(new DB_InsertOrUpdate_Action(State!.Value.FormVM!, State!.Value.FormMode!));
		//Dispatcher!.Dispatch(new MasterDetail.GetAll_Action());
		Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Index_Action(Video.Constants.GetPageHeaderForIndexVM()));
	}


	void CancelActionHandler()
	{
		Logger!.LogDebug(string.Format("Inside {0}", nameof(Form) + "!" + nameof(CancelActionHandler)));
		Dispatcher!.Dispatch(new ParentState.Set_PageHeader_For_Index_Action(Video.Constants.GetPageHeaderForIndexVM()));
	}
}

