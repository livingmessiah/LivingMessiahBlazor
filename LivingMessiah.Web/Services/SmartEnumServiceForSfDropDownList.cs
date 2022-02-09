using System.Collections.Generic;
using System.Linq;
using LivingMessiah.Web.Enums;
using LivingMessiah.Web.Pages.KeyDates.Enums;

namespace LivingMessiah.Web.Services;

public interface ISmartEnumServiceForSfDropDownList
{
		List<DropDownListVM> GetBibleBooksVM();
		List<DropDownListVM> GetKeyDateYearVM();
}

public class DropDownListVM
{
		public string Value { get; set; }
		public string Text { get; set; }
}

public class SmartEnumServiceForSfDropDownList : ISmartEnumServiceForSfDropDownList
{

		public List<DropDownListVM> GetBibleBooksVM()
		{
				List<DropDownListVM> books = new List<DropDownListVM>();
				var query = (from b in BaseBibleBookSmartEnum.List.ToList()
										 select new { b.Value, b.Name }).ToList();

				foreach (var item in query)
				{
						books.Add(new DropDownListVM() { Value = item.Value.ToString(), Text = item.Name });
				}
				return books;
		}

		public List<DropDownListVM> GetKeyDateYearVM()
		{
				List<DropDownListVM> books = new List<DropDownListVM>();
				var query = (from b in BaseKeyDateYearSmartEnum.List.ToList().OrderBy(o => o.Value)
										 select new { b.Value, b.Name }).ToList();

				foreach (var item in query)
				{
						books.Add(new DropDownListVM() { Value = item.Value.ToString(), Text = item.Name });
				}
				return books;
		}

}
