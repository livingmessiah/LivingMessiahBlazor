using System;
using LivingMessiah.Web.Enums;
namespace LivingMessiah.Web.Features.Parasha.ListByBook;

public class Parashot
{
	public int Id { get; set; }
	public int RowCntByBookId { get; set; } // , ROW_NUMBER() OVER(PARTITION BY BookId ORDER BY Id ) AS RowCntByBookId
	public decimal TriNum { get; set; } // TriNum
	public DateTime ShabbatDate { get; set; }

	public int BookId { get; set; }
	public BibleBook BibleBook
	{
		get
		{
			return BibleBook.FromValue(this.BookId);
		}
	}

	public int ShabbatWeekId { get; set; }

	public string? TorahLong { get; set; }
	public string? Name { get; set; }
	public string? ParashaName { get; set; }
	public string? NameUrl { get; set; }  // <a href="@MyHebrewBibleParashaUrl(@item.Id, @item.NameUrl)"><i>@item.ParashaName</i></a>
	public string? AhavtaURL { get; set; }
	public string? Meaning { get; set; }
	public string? Haftorah { get; set; }
	public string? Brit { get; set; }
	public bool IsNewBook { get; set; }       //, IIF(StartNewBookID IS NULL, 0, 1) AS IsNewBook

	public string? BaseParashaUrl { get; set; }

	public string ParashaHref
	{
		get
		{
			return $"{BaseParashaUrl}/{Id}?slug={NameUrl}/";
		}
	}

}
