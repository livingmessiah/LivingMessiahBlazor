# NO WORKY

# BibleBookChapterCascadingDDL.razor

@using Syncfusion.Blazor.DropDowns
@using Syncfusion.Blazor.Inputs

<SfDropDownList TItem="BookVM" TValue="string" DataSource="@Books"
								@bind-Value="@SelectedValue"
								Placeholder="Select a bible book" PopupHeight="auto">
	<DropDownListEvents TItem="BookVM" TValue="string" ValueChange="OnChangeBook"></DropDownListEvents>
	<DropDownListFieldSettings Text="BookName" Value="BookId"></DropDownListFieldSettings>
</SfDropDownList>

<SfDropDownList Enabled="@EnableChapterDropDown" TValue="string" TItem="ChapterVM"
								@bind-Value="@ChapterValue"
								Query="@ChapterQuery" DataSource="@Chapters">
	<DropDownListFieldSettings Text="ChapterName" Value="ChapterId"></DropDownListFieldSettings>
</SfDropDownList>

<SfNumericTextBox TValue="int" Value=1 Min="1" Max="@CurrentLastChapter" />
<p class="mt-1">Last Chapter: @CurrentLastChapter</p>

<hr />

@if (CurrentBookVM != null)
{
	<h4>CurrentBookVM</h4>
	<ul>

		<li>BookId: @CurrentBookVM.BookId</li>
		<li>BookName: @CurrentBookVM.BookName</li>
		<li>LastChapter: @CurrentBookVM.LastChapter</li>
	</ul>
}
else
{
	<p>CurrentBookVM is null</p>
}


# BibleBookChapterCascadingDDL.razor.cs
```csharp
using LivingMessiah.Web.Enums;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Data;

namespace LivingMessiah.Web.Pages.Admin.AudioVisual
{
	public partial class BibleBookChapterCascadingDDL
	{
		public bool EnableChapterDropDown = false;
		public string ChapterValue { get; set; } = null;

		// private readonly
		private List<BookVM> Books = new List<BookVM>();
		//private List<int> Chapters = new List<int>();
		private List<ChapterVM> Chapters = new List<ChapterVM>();
		public Query ChapterQuery { get; set; } = null;

		public BookVM CurrentBookVM { get; set; } = null;
		public string SelectedValue;

		protected override void OnInitialized()
		{
			var query = (from b in BaseBibleBookSmartEnum.List.ToList()
									 select new { b.Value, b.Name, b.LastChapter })
									 .OrderBy(o => o.Value).ToList();

			foreach (var item in query)
			{
				Books.Add(new BookVM()
				{ BookName = item.Value.ToString(), BookId = item.Name, LastChapter = item.LastChapter });
			}
			CurrentBookVM = Books.FirstOrDefault();
		}

		public int CurrentLastChapter = 150;

		public void OnChangeBook(ChangeEventArgs<string, BookVM> args)
		{
			this.EnableChapterDropDown = !string.IsNullOrEmpty(args.Value);
			this.ChapterQuery = new Query().Where(new WhereFilter() 
				{ Field = "ChapterId", Operator = "equal", value = args.Value
					, IgnoreCase = false, IgnoreAccent = false });
			this.ChapterValue = null;

			string currentText = args.ItemData.BookId;
			int i = int.TryParse(SelectedValue, out i) ? i : 0;
			//SelectedBookId = i;
			CurrentBookVM = Books.Where(w => w.BookId == currentText).SingleOrDefault();
			if (CurrentBookVM != null)
			{
				CurrentLastChapter = CurrentBookVM.LastChapter;
			}
			else
			{
				CurrentLastChapter = 150;
			}
		}

		//private void PopulateChapterDDL(int lastChapter) 
		//{
		//	for (int i = 0; i < lastChapter; i++)
		//	{

		//	}
		//}

		/*
		public void ChangeCountry(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, Countries> args)
		{
			this.EnableStateDropDown = !string.IsNullOrEmpty(args.Value);
			this.EnableCitytDropDown = false;
			this.StateQuery = new Query().Where(new WhereFilter() { Field = "CountryId", Operator = "equal", value = args.Value, IgnoreCase = false, IgnoreAccent = false });
			this.StateValue = null;
			this.CityValue = null;

		}
		*/
	}

	public class BookVM
	{
		public string BookName { get; set; }  // 
		public string BookId { get; set; }   // BookId
		public int LastChapter { get; set; }
	}

	public class ChapterVM
	{
		public string ChapterName { get; set; }     // ChapterName 
		public string BookId { get; set; }    // The Foreign Key ie BookVM.Text
		public string ChapterId { get; set; }
	}
}

```



# BibleBookChapterPage.razor
@page "/BBCP"
@using LivingMessiah.Web.Enums

<div class="pb-1 mt-4 mb-4 border-bottom">
	<h3>Bible Book Chapter Page</h3>
</div>

<BibleBookChapterCascadingDDL></BibleBookChapterCascadingDDL>
