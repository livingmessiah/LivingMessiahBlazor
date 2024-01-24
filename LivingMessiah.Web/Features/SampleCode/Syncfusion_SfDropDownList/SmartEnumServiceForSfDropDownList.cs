using System.Collections.Generic;
using System.Linq;
using LivingMessiah.Web.Enums;

namespace LivingMessiah.Web.Features.SampleCode.Syncfusion_SfDropDownList;

public interface ISmartEnumServiceForSfDropDownList
{
	List<DropDownListVM> GetBibleBooksVM();
}

public class DropDownListVM
{
	public string? Value { get; set; }
	public string? Text { get; set; }
}

public class SmartEnumServiceForSfDropDownList : ISmartEnumServiceForSfDropDownList
{

	public List<DropDownListVM> GetBibleBooksVM()
	{
		List<DropDownListVM> books = new List<DropDownListVM>();
		var query = (from b in BibleBook.List.ToList()
								 select new { b.Value, b.Name }).ToList();

		foreach (var item in query)
		{
			books.Add(new DropDownListVM() { Value = item.Value.ToString(), Text = item.Name });
		}
		return books;
	}


}
