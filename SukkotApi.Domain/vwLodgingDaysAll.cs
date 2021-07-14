namespace SukkotApi.Domain
{
	public class vwLodgingDaysAll
	{

		public string LodgingDay2 { get; set; } // Thu 20
		public int Sort { get; set; }           // Sorted version of LodgingDateId
		public int CampId { get; set; }					// Sukkot.Camp.Id
		public int LodgingDays { get; set; }		// Count of lodgings for that day for that CampId
		public string CampCode { get; set; }       // Sukkot.Camp.Code
	}
}
