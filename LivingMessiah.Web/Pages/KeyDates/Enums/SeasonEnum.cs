using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Web.Pages.KeyDates.Enums
{
	public enum SeasonEnum
	{
		Fall = 1,
		Winter = 2,
		Spring = 3,
		Summer = 4
	}

	public class SeasonLocal
	{
		public static List<SeasonLocal> All { get; } = new List<SeasonLocal>();

		public static SeasonLocal Fall { get; } = new SeasonLocal(SeasonEnum.Fall, 1, "Fall", "badge-warning", "fab fa-canadian-maple-leaf", "Equinox");
		public static SeasonLocal Winter { get; } = new SeasonLocal(SeasonEnum.Winter, 2, "Winter", "badge-primary", "fas fa-snowflake", "Solstice");
		public static SeasonLocal Spring { get; } = new SeasonLocal(SeasonEnum.Spring, 3, "Spring", "badge-success", "fas fa-cloud-sun-rain", "Equinox");
		public static SeasonLocal Summer { get; } = new SeasonLocal(SeasonEnum.Summer, 4, "Summer", "badge-danger", "far fa-sun", "Solstice");

		public SeasonEnum SeasonEnum { get; private set; }
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string BadgeColor { get; private set; }
		public string Icon { get; set; }
		public string Type { get; set; }

		private SeasonLocal(SeasonEnum seasonEnum, int id, string name, string badgeColor, string icon, string type)
		{
			SeasonEnum = seasonEnum;
			Id = id;
			Name = name;
			BadgeColor = badgeColor;
			Icon = icon;
			Type = type;
			All.Add(this);
		}

		public static SeasonLocal FromEnum(SeasonEnum enumValue)
		{
			return All.SingleOrDefault(r => r.SeasonEnum == enumValue);
		}

		public static SeasonLocal FromInt(int intValue)
		{
			return All.SingleOrDefault(r => r.Id == intValue);
		}

		/*
		public static Season FromString(string formatString)
		{
			return All.SingleOrDefault(r => String.Equals(r.Name, formatString, StringComparison.OrdinalIgnoreCase));
		}
		*/


	} // class Season
} // namespace


/*
SELECT 
'public static SeasonLocal ' + Name + ' { get; } = new SeasonLocal(SeasonEnum.' + Name + ', ' + CAST(EnumId AS varchar(30)) + 
', ' + QUOTENAME(Name, CHAR(34)) +
', ' + QUOTENAME(BadgeColor, CHAR(34)) +
	', ' + QUOTENAME(Icon, CHAR(34)) +
', ' + QUOTENAME(Type, CHAR(34)) + ');'
AS CodeGen
FROM KeyDate.Season 
WHERE Id < 5 
ORDER BY Id
*/