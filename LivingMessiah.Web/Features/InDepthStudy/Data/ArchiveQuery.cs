using Microsoft.AspNetCore.Components;
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

	public MarkupString YouTube
	{
		get
		{
			if (String.IsNullOrEmpty(YouTubeId))
			{
				return (MarkupString)"<mark><b>MISSING</b></mark>";
			}
			else
			{
				return (MarkupString)$"<a href='{YouTubeUrl}' target=`_blank` title='{YouTubeUrl}' <u>{YouTubeId}</u></a> <span class='btn btn-danger btn-xs'><i class='fab fa-youtube'></i></span>";
				//<i class='fab fa-youtube'></i>    <i class='fas fa-external-link-square-alt'></i>
			}
		}
	}

	public MarkupString CatSubCat
	{
		get
		{
			if (String.IsNullOrEmpty(Category) && String.IsNullOrEmpty(SubCategory))
			{
				
				return (MarkupString)"<mark>?</mark>";
			}
			else
			{
				return (MarkupString)$"{Category} <br /> {SubCategory}";
			}
		}
	}

	public string BookChapter
	{
		get
		{
			if (String.IsNullOrEmpty(BookTitle) && String.IsNullOrEmpty(Chapter))
			{
				return "";
			}
			else
			{
				return $"{BookTitle} {Chapter}";
			}
		}
	}

	public string Thumbnail
	{
		get
		{
			if (!String.IsNullOrEmpty(YouTubeId))
			{
				return $"http://img.youtube.com/vi/{YouTubeId}/{"default.jpg"}";
			}
			else
			{
				return "";
			}
		}
	}

}


