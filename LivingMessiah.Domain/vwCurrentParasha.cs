namespace LivingMessiah.Domain
{
	public class vwCurrentParasha
	{
		public int Id { get; set; }
		public int ShabbatWeekId { get; set; }
		public int BookId { get; set; }
		public string Torah { get; set; }
		public string Name { get; set; }
		public decimal TriNum { get; set; } // TriNum
		public string ParashaName { get; set; }
		public string NameUrl { get; set; }
		public string AhavtaURL { get; set; }
		public string Meaning { get; set; }
		public string Haftorah { get; set; }
		public string Brit { get; set; }
		public System.DateTime ShabbatDate { get; set; }
		public string CurrentParashaUrl { get; set; }
		
		public override string ToString()
		{
			//string x = $"Translation: {Meaning}, Haftorah: {Haftorah}, Brit: {Brit}, NameUrl: {NameUrl}";
			string x = $"Id: {Id}; TriNum: {TriNum}, Torah: {Torah}, Name: {Name}";
			return x;
		}
	}
}
