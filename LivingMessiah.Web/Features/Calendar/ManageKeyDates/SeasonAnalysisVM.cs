namespace LivingMessiah.Web.Features.Calendar.ManageKeyDates;

public class SeasonAnalysisVM
{
	public string? Season { get; set; }
	public string? PreviousDate { get; set; }
	public string? Date { get; set; }
	public int ActualDifference { get; set; }

	public string AcceptableRange
	{
		get
		{
			return $"{Constants.Defaults.SeasonLengthAverage - 1} <= {ActualDifference} <= {Constants.Defaults.SeasonLengthAverage + 1}";
		}
	}


	public string ColorAndFontWeight
	{
		get
		{
			if (Constants.Defaults.SeasonLengthAverage - 1 <= ActualDifference
					&& ActualDifference <= Constants.Defaults.SeasonLengthAverage + 1)
			{
				return " text-success";
			}
			else
			{
				return " fw-bolder text-danger";

			}
		}
	}

	public string Icon
	{
		get
		{
			if (Constants.Defaults.SeasonLengthAverage - 1 <= ActualDifference
					&& ActualDifference <= Constants.Defaults.SeasonLengthAverage + 1)
			{
				//	<span class='text-warning'><i class="fas fa-exclamation"></i></span>
				return " fas fa-thumbs-up";
			}
			else
			{
				return " fas fa-exclamation";
			}
		}
	}

}
