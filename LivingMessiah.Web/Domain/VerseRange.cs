namespace LivingMessiah.Web.Domain
{
	public class VerseRange
	{
		public int BegVerse { get; set; }
		public int EndVerse { get; set; }
		public VerseRange(int x, int y) => (BegVerse, EndVerse) = (x, y);
	}
}
