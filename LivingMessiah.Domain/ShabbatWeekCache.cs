using System;

namespace LivingMessiah.Domain
{
	// ToDo: Am I actually using this?
	public class ShabbatWeekCache
	{
		public int TypeId { get; set; }
		public string Descr { get; set; }
		public int RowNum { get; set; }
		public int ShabbatWeekId { get; set; }
		public DateTime ShabbatDate { get; set; }
		public bool IsCurrentShabbat { get; set; }
		public string YouTubeId { get; set; }
		public string Title { get; set; }
		public int Book { get; set; }
		public int Chapter { get; set; }

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

		public DateTime EventDate()
		{
			if (TypeId == (int)WeeklyVideoType.TorahTuesday)
			{
				return ShabbatDate.AddDays(3);
			}
			else
			{
				return ShabbatDate;
			}
		}
	}
}
