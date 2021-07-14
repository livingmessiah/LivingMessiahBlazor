
namespace LivingMessiah.Web.Domain
{
	public class BookTitle
	{
		public string EnglishTitle { get; set; }
		public string HebrewTitle { get; set; }
		public string HebrewName { get; set; }
		public string Group { get; set; }
		public string Url
		{
			get
			{
				return EnglishTitle.Replace(" ", "");
			}
		}

	}
}
