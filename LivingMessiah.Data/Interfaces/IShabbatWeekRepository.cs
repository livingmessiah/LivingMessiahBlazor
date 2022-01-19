using LivingMessiah.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using System.Linq;

namespace LivingMessiah.Data
{
	public interface IShabbatWeekRepository
	{
		string BaseSqlDump { get; }

		// Wirecast
		Task<int> UpdateWirecastLink(int id, string wireCastLink);
		Task<int> UpdateScratchpad(string scratchPad);
		Task<Wirecast> GetCurrentWirecast();
		Task<ScratchPad> GetScratchPadWireCast();

		// Psalms and Proverbs
		Task<PsalmAndProverb> GetCurrentPsalmAndProverb();
		Task<List<vwPsalmsAndProverbs>> GetPsalmsAndProverbsList();

		// Weekly Videos
		Task<IReadOnlyList<vwCurrentWeeklyVideo>> GetCurrentWeeklyVideos(int daysOld);
		Task<IReadOnlyList<WeeklyVideoIndex>> GetTopWeeklyVideos(int top);
		Task<int> WeeklyVideoAdd(WeeklyVideoModel dto);
		Task<int> WeeklyVideoUpdate(WeeklyVideoModel dto);
		Task<int> WeeklyVideoDelete(int id);

		// Bible
		Task<BibleBook> GetTorahBookById(int id);

		//Parasha
		Task<LivingMessiah.Domain.Parasha.Queries.Parasha> GetCurrentParashaAndChildren();
		Task<IReadOnlyList<Domain.Parasha.Queries.ParashaList>> GetParashotByBookId(int bookId);
		Task<Tuple<Domain.Parasha.Queries.BibleBook, List<Domain.Parasha.Queries.ParashaList>>> GetParashotForCurrentBook();
		#region ToDo: Move somewhere else

		// ToDo Why are these here? It needs to be pulled out of here and ISukkotAdminRepository
		// and put into something like LivingMessiahAdmin
		Task<int> LogErrorTest();
		Task<List<zvwErrorLog>> GetzvwErrorLog();
		Task<int> EmptyErrorLog();


		// ToDo does this go with LivingMessiahAdmin as well
		Task<int> UpdateContactSukkotInviteDate(int id);
		Task<List<Download>> GetDownloads(bool selectAll, bool testEmails);
		#endregion
	}
}
