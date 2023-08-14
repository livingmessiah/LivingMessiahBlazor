using System.Collections.Generic;
using System.Threading.Tasks;
using LivingMessiah.Domain;

namespace LivingMessiah.Web.Services;

public interface IShabbatWeekService
{
	Task<List<vwPsalmsAndProverbs>> GetPsalmsAndProverbsList();

	// Weekly Videos
	Task<IReadOnlyList<WeeklyVideoIndex>> GetTopWeeklyVideos(int top);
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
	#endregion

}

