using System.Collections.Generic;

namespace LivingMessiah.Web.Pages.Shavuot.Domain
{
	public static class OmerGematriaFactory
	{
		public static string GetHebrew(int count)
		{
			bool found;
			found = _dictionary.TryGetValue(count, out string hebrew);
			if (found)
			{
				return hebrew;
			}
			else
			{
				return "";
			}
		}

		static Dictionary<int, string> _dictionary = new Dictionary<int, string>
		{
				{ 1, "א" },
				{ 2, "ב" },
				{ 3, "ג" },
				{ 4, "ד" },
				{ 5, "ה" },
				{ 6, "ו" },
				{ 7, "ז" },
				{ 8, "ח" },
				{ 9, "ט" },
				{ 10, "י" },
				{ 11, "יא" },
				{ 12, "יב" },
				{ 13, "יג" },
				{ 14, "יד" },
				{ 15, "טו" },
				{ 16, "טז" },
				{ 17, "יז" },
				{ 18, "יח" },
				{ 19, "יט" },
				{ 20, "כ" },
				{ 21, "כא" },
				{ 22, "כב" },
				{ 23, "כג" },
				{ 24, "כד" },
				{ 25, "כה" },
				{ 26, "כו" },
				{ 27, "כז" },
				{ 28, "כח" },
				{ 29, "כט" },
				{ 30, "ל" },
				{ 31, "לא" },
				{ 32, "לב" },
				{ 33, "לג" },
				{ 34, "לד" },
				{ 35, "לה" },
				{ 36, "לו" },
				{ 37, "לז" },
				{ 38, "לח" },
				{ 39, "לט" },
				{ 40, "מ" },
				{ 41, "מא" },
				{ 42, "מב" },
				{ 43, "מג" },
				{ 44, "מד" },
				{ 45, "מה" },
				{ 46, "מו" },
				{ 47, "מז" },
				{ 48, "מח" },
				{ 49, "מט" },
				{ 50, "נ" }
		};

	}
}
