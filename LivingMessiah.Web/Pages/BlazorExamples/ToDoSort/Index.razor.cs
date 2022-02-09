using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.BlazorExamples.ToDoSort;

public partial class Index
{
		[Inject]
		public IToDoService svc { get; set; }

		List<ToDoItem> items = new List<ToDoItem>();
		private string newItem;
		private bool IsSortedAscending;
		private string CurrentSortColumn;

		protected override void OnInitialized()
		{
				items = svc.Get();
		}

		private void AddTodo()
		{
				if (!string.IsNullOrWhiteSpace(newItem))
				{
						var newToDoItem = new ToDoItem()
						{
								DateCreated = DateTime.Now,
								Description = newItem,
								ID = Guid.NewGuid()
						};

						items = svc.Add(newToDoItem);

						newItem = string.Empty; //We need to reset this string, otherwise the text box will still contain the value typed in.
				}
		}

		private void ToggleToDo(Guid id)
		{
				items = svc.Toggle(id);
		}

		private void RemoveTodo(Guid id)
		{
				items = svc.Delete(id);
		}

		private string GetSortStyle(string columnName)
		{
				if (CurrentSortColumn != columnName)
				{
						return string.Empty;
				}
				if (IsSortedAscending)
				{
						return "▲";
				}
				else
				{
						return "▼";
				}
		}

		private void SortTable(string columnName)
		{
				if (columnName != CurrentSortColumn)
				{
						//We need to force order by descending on the new column
						items = items.OrderBy(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
						CurrentSortColumn = columnName;
						IsSortedAscending = true;

				}
				else //Sorting against same column but in different direction
				{
						if (IsSortedAscending)
						{
								items = items.OrderByDescending(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
						}
						else
						{
								items = items.OrderBy(x => x.GetType().GetProperty(columnName).GetValue(x, null)).ToList();
						}

						IsSortedAscending = !IsSortedAscending;
				}
		}

}
