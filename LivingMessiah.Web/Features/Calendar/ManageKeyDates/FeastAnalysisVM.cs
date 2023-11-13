using System;

namespace LivingMessiah.Web.Features.Calendar.ManageKeyDates;

public class FeastAnalysisVM
{
	public string? Event { get; set; }
	public string? PreviousDate { get; set; }
	public string? Date { get; set; }
	public int ActualDifference { get; set; }
	public int RequiredDifference { get; set; }
	public string ColorAndFontWeight
	{
		get
		{
			if (ActualDifference != RequiredDifference)
			{
				return " fw-bolder text-danger";
			}
			else
			{
			return " text-success";

			}
		}
	}
	public string Icon
	{
		get
		{
			if (ActualDifference != RequiredDifference)
			{
				//	<span class='text-warning'><i class="fas fa-exclamation"></i></span>
				return " fas fa-exclamation"; 
			}
			else
			{
				return " fas fa-thumbs-up";  
			}
		}
	}

}
