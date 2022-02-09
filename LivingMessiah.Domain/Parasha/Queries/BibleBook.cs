namespace LivingMessiah.Domain.Parasha.Queries;

public class BibleBook                      // 8 references
{
		public int Id { get; set; }
		public string Abrv { get; set; }
		public string EnglishTitle { get; set; }  // used only by ParashaPrint.razor (2 places)
		public string HebrewTitle { get; set; }   // used only by ParashaPrint.razor
		public string HebrewName { get; set; }    // used only by ParashaPrint.razor
}
