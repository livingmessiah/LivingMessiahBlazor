using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Domain;
using LivingMessiah.Domain.Parasha.Queries;

namespace LivingMessiah.Web.Services
{
	public interface IShabbatWeekService
	{
		Task<List<vwPsalmsAndProverbs>> GetPsalmsAndProverbsList();

		// Weekly Videos
		Task<IReadOnlyList<WeeklyVideoIndex>> GetTopWeeklyVideos(int top);
		Task<int> WeeklyVideoAdd(WeeklyVideoModel dto);
		Task<int> WeeklyVideoUpdate(WeeklyVideoModel dto);
		Task<int> WeeklyVideoDelete(int id);

		// Parasha
		Task<IReadOnlyList<LivingMessiah.Domain.Parasha.Queries.ParashaList>> GetParashotByBookId(int bookId);

	}


	public class ShabbatWeekService : IShabbatWeekService
	{
		#region Constructor and DI
		private readonly LivingMessiah.Data.IShabbatWeekRepository db;
		//private readonly ILogger log;

		public ShabbatWeekService(
			LivingMessiah.Data.IShabbatWeekRepository dbRepository
			//, ILogger<ShabbatWeekService> logger
			)
		{
			db = dbRepository;
			//log = logger;
		}


		#endregion

		public async Task<List<vwPsalmsAndProverbs>> GetPsalmsAndProverbsList()
		{
			return await db.GetPsalmsAndProverbsList();
		}

		#region Weekly Videos

		public async Task<IReadOnlyList<WeeklyVideoIndex>> GetTopWeeklyVideos(int top)
		{
			return await db.GetTopWeeklyVideos(top);
		}

		public async Task<int> WeeklyVideoAdd(WeeklyVideoModel dto)
		{
			return await db.WeeklyVideoAdd(dto);
		}

		public async Task<int> WeeklyVideoDelete(int id)
		{
			return await db.WeeklyVideoDelete(id);
		}

		public async Task<int> WeeklyVideoUpdate(WeeklyVideoModel dto)
		{
			return await db.WeeklyVideoUpdate(dto);
		}
		#endregion



		// Parasha
		public async Task<IReadOnlyList<LivingMessiah.Domain.Parasha.Queries.ParashaList>> GetParashotByBookId(int bookId)
		{
			return await db.GetParashotByBookId(bookId);
		}
	}

}

