using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public class MarkupLiterals
{
	public static MarkupString Col_2nd_CheckIcon(Status usersCurentStatus, Status comparisonStatus, bool isXs)
	{
		if (usersCurentStatus.DisplayAsCompleted(comparisonStatus))
		{
			return isXs ?
				(MarkupString)$"<p class='text-center'><i class='{comparisonStatus.Icon} fa-2x'></i></p>" :
				(MarkupString)$"<i class='{comparisonStatus.Icon} fa-3x'></i>";
		}
		else
		{
			return (MarkupString)(string.Empty);
		}
	}

	public static MarkupString Col_3rd_Heading(Status comparisonStatus, string appendix)
	{
		return (MarkupString)$"<h4>Task - {comparisonStatus.Heading} {appendix}</h4>";
	}

	public static MarkupString Col_3rd_SubHeading(Status usersCurentStatus, Status comparisonStatus)
	{
		if (usersCurentStatus.DisplayAsCompleted(comparisonStatus))
		{
			return (MarkupString)"<p class='lead'><b>Task completed</b></p>";
		}
		else
		{
			return (MarkupString)(string.Empty);
		}
	}

}
