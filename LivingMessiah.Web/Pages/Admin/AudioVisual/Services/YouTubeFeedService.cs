namespace LivingMessiah.Web.Pages.Admin.AudioVisual.Services;

using Microsoft.Extensions.Logging;
using LivingMessiah.Web.Pages.Admin.AudioVisual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

public class DropDownListVM
{
	public string Value { get; set; }
	public string Text { get; set; }
}

public interface IYouTubeFeedService
{
	Task<List<YouTubeFeedModel>> GetModel(string url, int take);
	
	// THESE TWO NOT USED
	Task<List<DropDownListVM>> GetDropDownList(string url, int take); 
	Task<List<SyndicationItem>> GetXml(string url, int take);         
}

public class YouTubeFeedService : IYouTubeFeedService
{
	#region Constructor and DI
	private readonly IWeeklyVideosRepository db;
	private readonly ILogger Logger;

	public YouTubeFeedService(
		IWeeklyVideosRepository weeklyVideosRepository, ILogger<YouTubeFeedService> logger)
	{
		db = weeklyVideosRepository;
		Logger = logger;
	}

	#endregion


	public async Task<List<YouTubeFeedModel>> GetModel(string url, int take)
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(YouTubeFeedService) + "!" + nameof(GetModel)));
		await Task.Delay(0);
		using var reader = XmlReader.Create(url);
		var feed = SyndicationFeed.Load(reader);
		List<SyndicationItem> si = new List<SyndicationItem>();
		si = feed.Items
			.OrderByDescending(x => x.PublishDate)
			.Take(take)
			.ToList();

		List<YouTubeFeedModel> l = new();
		if (si.Any())
		{
			foreach (var item in si)
			{
				l.Add(new YouTubeFeedModel()
				{
					Id = null,
					YouTubeId = item.Id.Replace("yt:video:", ""),
					Title = item.Title.Text,
					PublishDate = item.PublishDate
				});
			}
		}
		return l;
	}


	public async Task<List<DropDownListVM>> GetDropDownList(string url, int take)
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(YouTubeFeedService) + "!" + nameof(GetDropDownList)));
		await Task.Delay(0);
		using var reader = XmlReader.Create(url);
		var feed = SyndicationFeed.Load(reader);
		List<SyndicationItem> feedResult = new List<SyndicationItem>();
		feedResult = feed.Items
			.OrderByDescending(x => x.PublishDate)
			.Take(take)
			.ToList();

		List<DropDownListVM> vm = new List<DropDownListVM>();
		foreach (var item in feedResult)
		{
			vm.Add(new DropDownListVM()
			{
				Value = item.Id.Replace("yt:video:", ""),
				Text = item.Id.Replace("yt:video:", "")
			});
		}

		return vm;
	}


	public async Task <List<SyndicationItem>> GetXml(string url, int take)
	{
		await Task.Delay(0);
		Logger.LogDebug(string.Format("Inside {0}", nameof(YouTubeFeedService) + "!" + nameof(GetXml)));

		using var reader = XmlReader.Create(url);
		var feed = SyndicationFeed.Load(reader);

		List<SyndicationItem> si = new List<SyndicationItem>();

		si = feed.Items
		.OrderByDescending(x => x.PublishDate)
		.Take(take)
		.ToList();
		return si;
	}
}

