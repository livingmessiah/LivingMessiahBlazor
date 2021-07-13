namespace LivingMessiah.Domain
{
	public class vwParasha
	{
		public int Id { get; set; }
		public int RowCntByBookId { get; set; }
		public int ShabbatWeekId { get; set; }
		public string Torah { get; set; }
		public string Name { get; set; }
		public decimal TriNum { get; set; } // TriNum
		public string ParashaName { get; set; }
		public string NameUrl { get; set; }
		public string AhavtaURL { get; set; }
		public string Meaning { get; set; }
		public string Haftorah { get; set; }
		public string Brit { get; set; }
		public bool IsNewBook { get; set; }
		public System.DateTime ShabbatDate { get; set; }
		public string BaseParashaUrl { get; set; }
	}
}
