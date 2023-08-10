using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Page = LivingMessiah.Web.Links.Admin.BibleGroup;
using BibleEnum = LivingMessiah.Web.Enums;
using System.Linq;

namespace LivingMessiah.Web.Pages.Admin.Video.BibleChapterCascadingDDL;

public partial class BibleGroupIndex
{
	[Inject] public IToastService? Toast { get; set; }
	
	private BibleGroupIndexVM? VM = new BibleGroupIndexVM();

	private List<BibleEnum.BibleGroup> BibleGroups = new();
	private List<BibleEnum.BibleBook> Books = new();
	
	private int bibleGroupId = 0;

	readonly string inside = $"page {Page.Index}; class: {nameof(BibleGroupIndex)}; ";

	/*
	[Parameter] public BibleGroupIndexVM VM { get; set; }
		If your editing an existing VM	than BibleGroup and BibleBook will have been selected, so
		you need to load Books based on BookGroupId

	*/
	protected override void OnInitialized()
	{

		if (VM!.BibleGroupId != 0)  // if (VM.BibleGroupId != null) an int Can't be null
		{
			bibleGroupId = 0;
			//LoadBooks(value);
			Books = BibleEnum.BibleBook.List
				.Where(w => w.BibleGroup.Value == bibleGroupId)
				.OrderBy(o => o.Value).ToList();
		}
		BibleGroups = BibleEnum.BibleGroup.List.OrderBy(o => o.Value).ToList();
	}
	

	protected void FormSubmitted()
	{
		//Logger!.LogDebug(string.Format("Inside {0} {1}", inside, nameof(FormSubmitted)));
		//Logger!.LogDebug(string.Format("... BookId: {0}; ChapterId {1}", selectedBook, selectChapter));

		// ToDo: VM.BibleGroupId is zero but bibleGroupId=the correct number
		Toast!.ShowInfo($"VM.Name {VM.Name}; VM.BibleGroupId {VM.BibleGroupId}; bibleGroupId {bibleGroupId}; VM.BookId: {VM.BookId}");
	}

	private void BibleGroupIdHasChanged(int value)
	{
		Toast!.ShowInfo($"Inside {nameof(BibleGroupIdHasChanged)}; value: {value}");
		VM.BookId = 0;
		bibleGroupId = value;

		if (value == 0)
		{
			Books.Clear();
		}
		else
		{
			//LoadBooks(value);
			Books = BibleEnum.BibleBook.List
				.Where(w => w.BibleGroup.Value == bibleGroupId)
				.OrderBy(o => o.Value).ToList();
		}
	}
/*
	private void LoadBooks(int bibleGroupId)
	{
		Books = BibleEnum.BibleBook.List
			.Where(w => w.BibleGroup.Value == bibleGroupId)
			.OrderBy(o => o.Value).ToList();
	}
*/
}
