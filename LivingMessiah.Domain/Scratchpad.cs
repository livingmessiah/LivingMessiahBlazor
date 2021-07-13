using System;
using System.ComponentModel.DataAnnotations;

namespace LivingMessiah.Domain
{
	public class ScratchPad
	{
		//[DataType(DataType.MultilineText)]
		[MaxLength(4000)]
		public string WireCast { get; set; }
	}
}
