using System;
using System.Collections.Generic;
using System.Linq;
using LivingMessiah.Web.Enums;

namespace LivingMessiah.Web.Services
{
	public interface IBibleService
	{
		List<Tuple<string, string>> GetBibleBooks();  // NO WORKY
		List<DropDownListVM> GetBibleBooksVM();
	}

	public class DropDownListVM
	{
		public string Value { get; set; }
		public string Text { get; set; }
	}

	public class BibleService : IBibleService
	{

		public List<Tuple<string, string>> GetBibleBooks()
		{
			List<Tuple<string, string>> books = new List<Tuple<string, string>>();
			var query = (from b in BaseBibleBookSmartEnum.List.ToList()
									 select new { b.Value, b.Name }).ToList();

			foreach (var item in query)
			{
				books.Add(new Tuple<string, string>(item.Value.ToString(), item.Name)); 
			}
			return books;
		}

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

	}
}
