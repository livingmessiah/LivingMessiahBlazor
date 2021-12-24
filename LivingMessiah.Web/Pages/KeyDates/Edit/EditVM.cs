using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LivingMessiah.Web.Pages.KeyDates.Enums;

namespace LivingMessiah.Web.Pages.KeyDates.Edit
{
	// ToDo: NOT BEING USED
	public class EditVM
	{
		[Required]
		[Key]
		public int Id { get; set; }

		[Required]
		public DateTime Date { get; set; }
	}
}
