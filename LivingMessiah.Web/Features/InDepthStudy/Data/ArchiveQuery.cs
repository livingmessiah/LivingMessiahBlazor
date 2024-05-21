using System;

namespace LivingMessiah.Web.Features.InDepthStudy.Data;

public class ArchiveQuery
{
	public int Id { get; set; }
	public DateTime ShabbatDate { get; set; }
	public string? YouTubeId { get; set; }
	public string? YouTubeUrl { get; set; }
	public string? Title { get; set; }
	public string? GraphicFile { get; set; }

	public string? BookTitle { get; set; }
	public string? Chapter { get; set; }
	public string? BiblicalUrlReference { get; set; }

	public string? Category { get; set; }
	public string? SubCategory { get; set; }

	public override string ToString()
	{
		return $"Id: {Id}, YouTubeId: {YouTubeId ?? "NULL"}";
	}

	public string CatSubCat
	{
		get
		{
			if (String.IsNullOrEmpty(Category) && String.IsNullOrEmpty(SubCategory))
			{
				return "";
			}
			else
			{
				return $"{Category} | {SubCategory}";
			}
		}

	}


	/*

	https://myhebrewbible.com/bookchapter/Amos/3/the-necessity-of-gods-judgment-testimony-against-israel

	BiblicalUrlReference: 

	public DateTime EventDate()
	{
		return ShabbatDate;
	}
	*/
}

//, ShabbatWeekId: {ShabbatWeekId}
// public string? ShabbatDate { get; set; }
//	public int ShabbatWeekId { get; set; }
