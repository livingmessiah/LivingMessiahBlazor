﻿using LivingMessiah.Web.Shared.Header.Enums;
using LivingMessiah.Web.Shared.Header.Store;
using BibleEnum = LivingMessiah.Web.Enums;
using System;

namespace LivingMessiah.Web.Pages.Admin.Video.Models;

public class WeeklyVideoTable
{
	public int Id { get; set; }
	public int ShabbatWeekId { get; set; }
	public DateTime ShabbatDate { get; set; }
	public int WeeklyVideoTypeId { get; set; }
	public string? WeeklyVideoTypeDescr { get; set; }

	public string? YouTubeId { get; set; }
	public string Url()
	{
		if (YouTubeId != null)
		{
			return $"https://www.youtube.com/watch?v={YouTubeId}";
		}
		else
		{
			return "";
		}
	}

	public int Book { get; set; }
	public int Chapter { get; set; }
	public string BC
	{
		get
		{
			return this.Book !=0 ?
				$"{BibleWebsite.MyHebrewBible.UrlBase}{BibleEnum.BibleBook.FromValue(this.Book).Title}/{Chapter}/slug"
				: "";
		}
	}

	public string? Title { get; set; }
}
