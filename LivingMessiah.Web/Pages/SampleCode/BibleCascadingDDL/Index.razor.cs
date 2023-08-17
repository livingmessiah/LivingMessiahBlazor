using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Page = LivingMessiah.Web.Links.SampleCode.BibleCascadingDDL;
using BibleEnum = LivingMessiah.Web.Enums;
using System.Linq;

namespace LivingMessiah.Web.Pages.SampleCode.BibleCascadingDDL;

public partial class Index
{
	[Inject] public IToastService? Toast { get; set; }

	private IndexVM? VM = new IndexVM();

	private List<BibleEnum.BibleGroup> BibleGroups = new();
	private List<BibleEnum.BibleBook> Books = new();

	private int bibleGroupId = 0;

	readonly string inside = $"page {Page.Index}; class: {nameof(Index)}";

	/*
	[Parameter] public IndexVM VM { get; set; }
		If your editing an existing VM	than BibleGroup and BibleBook will have been selected, so
		you need to load Books based on BookGroupId

	*/
	protected override void OnInitialized()
	{

		if (VM!.BibleGroupId != 0)  // if (VM.BibleGroupId != null) an int Can't be null
		{
			bibleGroupId = VM!.BibleGroupId;
			LoadBooksFilteredByBibleGroup(bibleGroupId);
		}
		BibleGroups = BibleEnum.BibleGroup.List.OrderBy(o => o.Value).ToList();
	}


	protected void FormSubmitted()
	{
		//Logger!.LogDebug(string.Format("Inside {0} {1}", inside, nameof(FormSubmitted)));
		//Logger!.LogDebug(string.Format("... BookId: {0}; ChapterId {1}", selectedBook, selectChapter));

		if (VM!.BibleGroupId == 0)
		{
			Toast!.ShowWarning($"VM.BibleGroupId == 0; updating with bibleGroupId {bibleGroupId}");
			VM.BibleGroupId = bibleGroupId;
		}

		Toast!.ShowSuccess($"VM.Name {VM.Name}; VM.BibleGroupId {VM.BibleGroupId}; VM.BookId: {VM.BookId}");

	}

	private void BibleGroupIdHasChanged(int id)
	{
		Toast!.ShowInfo($"Inside {nameof(BibleGroupIdHasChanged)}; value: {id}");
		VM!.BookId = 0;
		bibleGroupId = id;

		if (id == 0)
		{
			Books.Clear();
		}
		else
		{
			LoadBooksFilteredByBibleGroup(id);
		}
	}

	private void LoadBooksFilteredByBibleGroup(int bibleGroupId)
	{
		Books = BibleEnum.BibleBook.List
			.Where(w => w.BibleGroup.Value == bibleGroupId)
			.OrderBy(o => o.Value).ToList();
	}

}
