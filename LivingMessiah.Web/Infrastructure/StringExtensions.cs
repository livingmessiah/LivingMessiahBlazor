using System.Text;

namespace LivingMessiah.Web.Infrastructure
{
	public static class StringExtensions
	{
		/*
			string s = new StringBuilder()
				.Append(MyHebrewBible.Torah)
				.Append("<br />").Append(MyHebrewBible.Haftorah)
				.AppendWhen(", " + ParashaReadings.Haftorah2, ParashaReadings.Haftorah2 != "")
				.Append("<br />").Append(MyHebrewBible.Brit)
				.AppendWhen(", " + ParashaReadings.Brit2, ParashaReadings.Brit2 != "")
				.ToString();
	 
	 http://edcharbeneau.com/csharp-functional-workshop-instructions/
	 */
		public static StringBuilder AppendWhen(
			this StringBuilder sb, string value, bool predicate) => predicate ? sb.Append(value) : sb;


		// See D:\TFS\OsisXmlToSql\BuildLetter\Helper\StringExtensions.cs
		public static StringBuilder AppendIf(this StringBuilder builder, bool condition, string value)
		{
			if (condition) builder.Append(value);
			return builder;
		}

		public static string Truncate(this string value, int maxLength)
		{
			if (string.IsNullOrEmpty(value)) return value;
			return value.Length <= maxLength ? value : value.Substring(0, maxLength);
		}


	}
}
