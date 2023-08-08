


	Task<List<YouTubeFeedModel>> GetViewModel(string url, int take);

	public async Task<List<YouTubeFeedModel>> GetViewModel(string url, int take)
	{
		Logger.LogDebug(string.Format("Inside {0}", nameof(YouTubeFeedService) + "!" + nameof(GetXml)));

		List<UpdateGridViewModel> weeklyVideos = await db.GetWeeklyVideos();

		using var reader = XmlReader.Create(url);

		var feed = SyndicationFeed.Load(reader);

		List<SyndicationItem> feedResult = new List<SyndicationItem>();

		feedResult = feed.Items
		.OrderByDescending(x => x.PublishDate)
		.Take(take)
		.ToList();

		List<YouTubeFeedModel> ytVM = new List<YouTubeFeedModel>();
		foreach (var item in feedResult)
		{
			ytVM.Add(new YouTubeFeedModel() {
				Id = item.Id.Replace("yt:video:", ""),  Title = item.Title.Text, PublishDate = item.PublishDate
			});
		}

		return ytVM;
	}




		c# linq projection outer join

		var q =
		from f in LatestVideoList
		join wv in weeklyVideos on f.Id equals "yt:video:" + wv.YouTubeId into wv
		from f in LatestVideoList.DefaultIfEmpty()
		where _t2 == null ? true :
				_t2.Table2WhereColumn1 == @someId
				&& _t2.Table2WhereColumn2 == @someOtherId
		orderby t1.OrderByColumn

		join wv in weeklyVideos on f.Id  equals "yt:video:" + wv.YouTubeId

		var query = (from wv in weeklyVideos.ToList()
								 select new
								 {wv.Id,	wv.WeeklyVideoTypeEnum, wv.YouTubeId, wv.Title, wv.Book, wv.Chapter
								 }).ToList();


var query =
	(from a in db.Articles
	 where a.FileNameNoExt.Contains(" ") || a.FileNameNoExt.Contains("–") || a.FileNameNoExt.Contains("%")
	 select new { a.Id, a.FileNameNoExt }).ToList();

ArticleOverviewVM.ArticleInvalidCharacters = new List<Bible.Domain.ArticleOverview.ArticleIdAndFilename>();

foreach (var item in query)
{
	ArticleOverviewVM.ArticleInvalidCharacters.Add(new Bible.Domain.ArticleOverview.ArticleIdAndFilename() { ID = item.Id, FileNameNoExt = item.FileNameNoExt });
}

query =
	(from a in db.ArticleBlobImport
	 where a.FileNameNoExt.Contains(" ") || a.FileNameNoExt.Contains("–") || a.FileNameNoExt.Contains("%")
	 select new { a.Id, a.FileNameNoExt }).ToList();
ArticleOverviewVM.BlobInvalidCharacters = new List<Models.ArticleIdAndFilename>();
foreach (var item in query)
{
	ArticleOverviewVM.BlobInvalidCharacters.Add(new Models.ArticleIdAndFilename() { ID = item.Id, FileNameNoExt = item.FileNameNoExt });
}




	#region CustomExceptions Classes
	/*
	public class UserNotAuthoirizedException : Exception
	{
	}
	
	*/
	#endregion