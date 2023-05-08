using System;

namespace LivingMessiah.Domain;

public class vwCurrentWeeklyVideo
{
		public int Id { get; set; }
		public int ShabbatWeekId { get; set; }
		public int WvtId { get; set; }
		public DateTime ShabbatDate { get; set; }
		public string? YouTubeId { get; set; }
		public string? YouTubeUrl { get; set; }
		public string? Title { get; set; }
		public string? GraphicFile { get; set; }
		public string? NotesFile { get; set; }
		public string? WvtDescr { get; set; }
		public string? WvtIcon { get; set; }
		public string? BookChapterTitle { get; set; }
		public string? Chapter { get; set; }
		public string? BookTitle { get; set; }
		public string? HebrewTitle { get; set; }
		public string? HebrewName { get; set; }
		public string? ParashaName { get; set; }
		public string? BiblicalUrlReference { get; set; }


		public override string ToString()
		{
				return $"Id: {Id}, WvtId: {WvtId}, ShabbatWeekId: {ShabbatWeekId}, YouTubeId: {YouTubeId ?? "NULL"}";
		}

		// Can this be handled in the sql server view?
		public DateTime EventDate()
		{
				if (WvtId == (int)WeeklyVideoType.TorahTuesday)
				{
						return ShabbatDate.AddDays(3);
				}
				else
				{
						return ShabbatDate;
				}
		}

}
