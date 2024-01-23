using System;

namespace LivingMessiah.Web.Features.Parasha;

public class CurrentParasha
{
	public int Id { get; set; }
	public decimal TriNum { get; set; }
	public DateTime ShabbatDate { get; set; }
	public int BookId { get; set; }

	public string? TorahLong { get; set; }
	public string? Haftorah { get; set; }
	public string? Brit { get; set; }
	public string? AhavtaURL { get; set; }  // http://www.ahavta.org/Commentary Y-1/Y1-01.htm
	public string? NameUrl { get; set; }  // b-reisheet-genesis-1-1-to-19-number-1-1
	public string? BaseParashaUrl { get; set; }
	public string? ParashaName { get; set; }

	public override string ToString()
	{
		return $"Id: {Id}; BookId: {BookId}, TorahLong: {TorahLong}";
	}

	public string ParashaHref
	{
		get
		{
			//return $"{BibleConstants.BaseParashaUrl}/{Id}?slug={NameUrl}/";  
			return $"{BaseParashaUrl}/{Id}?slug={NameUrl}/";
		}
	}
}
