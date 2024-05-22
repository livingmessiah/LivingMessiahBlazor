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
	
	public string GraphicFileUrl
	{
		get
		{
			return !String.IsNullOrEmpty(GraphicFile) ? Blobs.ImageFullPath(GraphicFile) : "";
		}
	}

public override string ToString()
	{
		return $"Id: {Id}, YouTubeId: {YouTubeId ?? "NULL"}";
	}

	public MarkupString YouTube
	{
		get
		{
			return String.IsNullOrEmpty(YouTubeId)
				? (MarkupString)"<mark><b>MISSING</b></mark>"
				: (MarkupString)$"<a href='{YouTubeUrl}' target=`_blank` title='{YouTubeUrl}' <u>{YouTubeId}</u></a> <span class='btn btn-danger btn-xxs'><i class='fab fa-youtube'></i></span>";
		}
	}

	public MarkupString CatSubCat
	{
		get
		{
			return String.IsNullOrEmpty(Category) && String.IsNullOrEmpty(SubCategory)
				? (MarkupString)"<mark>?</mark>"
				: (MarkupString)$"{Category} <br /> {SubCategory}";
		}
	}

	public string BookChapter
	{
		get
		{
			return String.IsNullOrEmpty(BookTitle) && String.IsNullOrEmpty(Chapter) ? "" : $"{BookTitle} {Chapter}";
		}
	}

	public string ThumbnailImgSrc
	{
		get
		{
			return !String.IsNullOrEmpty(YouTubeId) ? $"http://img.youtube.com/vi/{YouTubeId}/{"default.jpg"}" : "";
		}
	}

	public string MaxResDefaultImgSrc
	{
		get
		{
			return !String.IsNullOrEmpty(YouTubeId) ? $"http://img.youtube.com/vi/{YouTubeId}/{"maxresdefault.jpg"}" : "";
		}
	}

}

// Ignore Spelling: Img
