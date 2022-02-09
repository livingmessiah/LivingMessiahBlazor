using System;

namespace LivingMessiah.Domain.Parasha.Queries;

public class Parasha
{
		public int Id { get; set; }

		public decimal TriNum { get; set; }
		public DateTime ShabbatDate { get; set; }

		public int? PrevId { get; set; }
		public int? NextId { get; set; }

		public int BookId { get; set; }
		public BibleBook BibleBook { get; set; }
		//public BibleConstants BibleConstants { get; set; }  // Don't need because vwCurrentParasha calls it

		public string TorahLong { get; set; }
		public string Haftorah { get; set; }
		public string Brit { get; set; }
		public string ParashaName { get; set; }
		public string AhavtaURL { get; set; }  // http://www.ahavta.org/Commentary Y-1/Y1-01.htm
		public string NameUrl { get; set; }  // b-reisheet-genesis-1-1-to-19-number-1-1
		public string BaseParashaUrl { get; set; }
		/*
		public int ShabbatWeekId { get; set; }
		public string Meaning { get; set; }  // In the beginning (Days 1 - 4)
		public string Name { get; set; }		 // 1.1, Gen 1:1-19, Sep 29 2018
		*/

		public override string ToString()
		{
				return $"Id: {Id}; BookId: {BookId}, TorahLong: {TorahLong}";
		}

		/**/
		public string ParashaHref
		{
				get
				{
						//return $"{BibleConstants.BaseParashaUrl}/{Id}?slug={NameUrl}/";  
						return $"{BaseParashaUrl}/{Id}?slug={NameUrl}/";
				}
		}

}
