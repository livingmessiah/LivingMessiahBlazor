using System;
using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Domain
{
	public class Wirecast
	{
		[Required]
		public Int32 Id { get; set; }

		[Required]
		public String ShabbatDate { get; set; }

		[Required]
		[MaxLength(100)]
		[Url]
		public String WirecastLink { get; set; }

	}
}
