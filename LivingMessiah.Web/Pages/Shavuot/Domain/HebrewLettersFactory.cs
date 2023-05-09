using System.Collections.Generic;

namespace LivingMessiah.Web.Pages.Shavuot.Domain;

public static class HebrewLettersFactory
{
	public static List<HebrewLetter> HebrewLetters()
	{
		return new List<HebrewLetter>
			{

new HebrewLetter {Id = 1, UnicodeName = "Aleph", Hebrew = "א", Gematria = 1, Sofit = "", WithoutDagesh = ""},
new HebrewLetter {Id = 2, UnicodeName = "Bet | Vet", Hebrew = "בּ", Gematria = 2, Sofit = "", WithoutDagesh = "ב"},
new HebrewLetter {Id = 3, UnicodeName = "Gimel", Hebrew = "ג", Gematria = 3, Sofit = "", WithoutDagesh = ""},
new HebrewLetter {Id = 4, UnicodeName = "Dalet", Hebrew = "ד", Gematria = 4, Sofit = "", WithoutDagesh = ""},
new HebrewLetter {Id = 5, UnicodeName = "Hey", Hebrew = "ה", Gematria = 5, Sofit = "", WithoutDagesh = ""},
new HebrewLetter {Id = 6, UnicodeName = "Vav", Hebrew = "ו", Gematria = 6, Sofit = "", WithoutDagesh = ""},
new HebrewLetter {Id = 7, UnicodeName = "Zayin", Hebrew = "ז", Gematria = 7, Sofit = "", WithoutDagesh = ""},
new HebrewLetter {Id = 8, UnicodeName = "Chet", Hebrew = "ח", Gematria = 8, Sofit = "", WithoutDagesh = ""},
new HebrewLetter {Id = 9, UnicodeName = "Tet", Hebrew = "ט", Gematria = 9, Sofit = "", WithoutDagesh = ""},
new HebrewLetter {Id = 10, UnicodeName = "Yood", Hebrew = "י", Gematria = 10, Sofit = "", WithoutDagesh = ""},
new HebrewLetter {Id = 11, UnicodeName = "Chaf", Hebrew = "כּ", Gematria = 20, Sofit = "ך", WithoutDagesh = "כ"},
new HebrewLetter {Id = 12, UnicodeName = "Lamed", Hebrew = "ל", Gematria = 30, Sofit = "", WithoutDagesh = ""},
new HebrewLetter {Id = 13, UnicodeName = "Mem", Hebrew = "מ", Gematria = 40, Sofit = "ם", WithoutDagesh = ""},
new HebrewLetter {Id = 14, UnicodeName = "Nun", Hebrew = "נ", Gematria = 50, Sofit = "ן", WithoutDagesh = ""},
new HebrewLetter {Id = 15, UnicodeName = "Samech", Hebrew = "ס", Gematria = 60, Sofit = "", WithoutDagesh = ""},
new HebrewLetter {Id = 16, UnicodeName = "Ayin", Hebrew = "ע", Gematria = 70, Sofit = "", WithoutDagesh = ""},
new HebrewLetter {Id = 17, UnicodeName = "Pey | Fey", Hebrew = "פּ", Gematria = 80, Sofit = "ף", WithoutDagesh = "פ"},
new HebrewLetter {Id = 18, UnicodeName = "Tsadi", Hebrew = "צ", Gematria = 90, Sofit = "ץ", WithoutDagesh = ""},
new HebrewLetter {Id = 19, UnicodeName = "Koof", Hebrew = "ק", Gematria = 100, Sofit = "", WithoutDagesh = ""},
new HebrewLetter {Id = 20, UnicodeName = "Resh", Hebrew = "ר", Gematria = 200, Sofit = "", WithoutDagesh = ""},
new HebrewLetter {Id = 21, UnicodeName = "Sheen | Seen", Hebrew = "שׁ", Gematria = 300, Sofit = "", WithoutDagesh = "שׂ"},
new HebrewLetter {Id = 22, UnicodeName = "Tav", Hebrew = "ת", Gematria = 400, Sofit = "", WithoutDagesh = ""},

			};
	}
}

/*
SELECT 
'new HebrewChart {' + 
'Id = ' + CAST(SortOrder AS varchar(4)) + 
', UnicodeName = "' + UnicodeName + '"' + 
', Hebrew = "' + Hebrew + '"' + 
', Gematria = ' + CAST(Gematria AS varchar(4))  + 
', Sofit = "' + ISNULL(Sofit, '') + '"' + 
', WithoutDagesh = "' + ISNULL(WithoutDagesh, '') + + '"},' 
AS CodeGen
--, SortOrder AS Id, UnicodeName, Hebrew, Gematria, WithoutDagesh, Sofit
-- UniInt, UnicodeName, SortOrder, Hebrew, EngLetter, Gematria, Sofit, Strongs, WithoutDagesh, PrefixMeaning, LiteralMeaning, SymbolicMeaning, Footnote
FROM HebrewChart
ORDER BY SortOrder
*/
