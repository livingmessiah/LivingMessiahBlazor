using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SukkotApi.Domain
{
	public class Lunch
	{
		[DisplayName("Day 1")]
		[Range(0, 12, ErrorMessage = "{0} must be between {1} and {2}")]
		public int Day01 { get; set; }

		[DisplayName("Day 2")]
		[Range(0, 12, ErrorMessage = "{0} must be between {1} and {2}")]
		public int Day02 { get; set; }

		[DisplayName("Day 3")]
		[Range(0, 12, ErrorMessage = "{0} must be between {1} and {2}")]
		public int Day03 { get; set; }

		[DisplayName("Day 4")]
		[Range(0, 12, ErrorMessage = "{0} must be between {1} and {2}")]
		public int Day04 { get; set; }

		[DisplayName("Day 5")]
		[Range(0, 12, ErrorMessage = "{0} must be between {1} and {2}")]
		public int Day05 { get; set; }

		[DisplayName("Day 6")]
		[Range(0, 12, ErrorMessage = "{0} must be between {1} and {2}")]
		public int Day06 { get; set; }

		[DisplayName("Day 7")]
		[Range(0, 12, ErrorMessage = "{0} must be between {1} and {2}")]
		public int Day07 { get; set; }

		[DisplayName("Day 8")]
		[Range(0, 12, ErrorMessage = "{0} must be between {1} and {2}")]
		public int Day08 { get; set; }

		/*
		[DisplayName("Day 9")]
		[Range(0, 12, ErrorMessage = "{0} must be between {1} and {2}")]
		public int Day09 { get; set; }

		[DisplayName("Day 10")]
		[Range(0, 12, ErrorMessage = "{0} must be between {1} and {2}")]
		public int Day10 { get; set; }
		*/

		public int MealCount { get; set; }

		[DisplayFormat(DataFormatString = "{0:C0}")]
		public decimal MealCost { get; set; }
	}
}
