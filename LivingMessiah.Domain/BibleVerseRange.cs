namespace LivingMessiah.Domain
{
	public class BibleVerseRange
	{
		public int BegVerse { get; set; }
		public int EndVerse { get; set; }
		public BibleVerseRange(int x, int y) => (BegVerse, EndVerse) = (x, y);
	}
}
