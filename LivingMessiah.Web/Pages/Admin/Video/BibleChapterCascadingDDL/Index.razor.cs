using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

using Blazored.Toast.Services;
using Page = LivingMessiah.Web.Links.Admin.BibleChapterCascadingDDL;

namespace LivingMessiah.Web.Pages.Admin.Video.BibleChapterCascadingDDL;

public partial class Index
{
	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	//private IndexVM? VM => new IndexVM();
	private IndexVM IndexVM = new IndexVM();
	readonly string inside = $"page {Page.Index}; class: {nameof(Index)}; ";

	/*
	protected override void OnInitialized()
	{
		Logger!.LogDebug(string.Format("Inside {0} {1}", inside, nameof(OnInitialized)));
	//IndexVM.BookId = 1;
		try
		{
			CascadingVM.Books = DropdownService!.Books();
			CascadingVM.BookId = "";

			CascadingVM.Chapters = new List<SelectListItem>()
				{
						new SelectListItem()
						{
								Text = "Select",
								Value = ""
						}
				};

			CascadingVM.ChapterId = "";
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, inside);
			Toast!.ShowError($"Error; {inside}; {nameof(OnInitialized)}");
		}
	}
*/

	//string value
	protected void OnBookChange()
	{
		Logger!.LogDebug(string.Format("Inside {0} {1}", inside, nameof(OnBookChange)));
		IndexVM.ChapterId = 1;
		Toast!.ShowInfo($"Inside : {nameof(OnBookChange)}; value {IndexVM.BookId}");
		/*
		try
		{
			if (value != null)
			{
				CascadingVM.BookId = value.ToString();
				CascadingVM.ChapterId = "";
				CascadingVM.Chapters = new List<SelectListItem>()
								{
										new SelectListItem()
										{
												Text = "Select",
												Value = ""
										}
								};

				CascadingVM.Chapters = DropdownService!.Chapters(Convert.ToInt32(CascadingVM.ChapterId));
				StateHasChanged();
			}

		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, inside);
			Toast!.ShowError($"Error; {inside}; {nameof(OnBookChange)}");
		}
		*/
	}

	//Never Called
	protected void OnChapterChange(string value)
	{
		/*
		if (value != null)
		{
			CascadingVM.ChapterId = value.ToString();
		}
		*/
	}

	protected async void FormSubmitted()
	{
		Logger!.LogDebug(string.Format("Inside {0} {1}", inside, nameof(FormSubmitted)));
		await Task.Delay(0);

		var selectedBook = IndexVM.BookId;
		var selectChapter = IndexVM.ChapterId;
		Logger!.LogDebug(string.Format("... BookId: {0}; ChapterId {1}", selectedBook, selectChapter));
		Toast!.ShowInfo($"BookId: {selectedBook}; ChapterId {selectChapter}");
	}

}

