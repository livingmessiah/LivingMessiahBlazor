using System;

namespace LivingMessiah.Web.Pages.BlazorExamples.ToDoSort;

public class ToDoItem
{
		public Guid ID { get; set; }
		public string Description { get; set; }
		public bool IsComplete { get; set; }
		public DateTime DateCreated { get; set; }
}
