using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public class MarkupLiterals
{

	public static MarkupString Col_2nd_CheckIcon(Status usersCurentStatus, Status comparisonStatus, bool isXs)
	{
		if (usersCurentStatus == Status.PartiallyPaid && comparisonStatus == Status.FullyPaid)
		{
			return isXs ?
				(MarkupString)$"<p class='text-center'><i class='{usersCurentStatus.Icon} fa-2x'></i></p>" :
				(MarkupString)$"<i class='{usersCurentStatus.Icon} fa-3x'></i>";
		}
		else
		{
			if (usersCurentStatus == Status.FullyPaid && comparisonStatus == Status.FullyPaid)
			{
				return isXs ?
					(MarkupString)$"<p class='text-center'><i class='{usersCurentStatus.Icon} fa-2x'></i></p>" :
					(MarkupString)$"<i class='{usersCurentStatus.Icon} fa-3x'></i>";
			}
			else
			{
				if (usersCurentStatus == Status.RegistrationFormCompleted && comparisonStatus == Status.RegistrationFormCompleted)
				{
					return isXs ?
						(MarkupString)$"<p class='text-center'><i class='{comparisonStatus.Icon} fa-2x'></i></p>" :
						(MarkupString)$"<i class='{comparisonStatus.Icon} fa-3x'></i>";
				}
				else
				{
					if (usersCurentStatus.Value <= comparisonStatus.Value)
					{
						return (MarkupString)(string.Empty);
					}
					else
					{
						return isXs ?
							(MarkupString)$"<p class='text-center'><i class='{comparisonStatus.Icon} fa-2x'></i></p>" :
							(MarkupString)$"<i class='{comparisonStatus.Icon} fa-3x'></i>";
					}
				}
			}
		}
	}

	public static MarkupString Col_3rd_Heading(Status comparisonStatus, string appendix)
	{
		return (MarkupString)$"<h4>Task - {comparisonStatus.Heading} {appendix}</h4>";
	}

	public static MarkupString Col_3rd_SubHeading(Status usersCurentStatus, Status comparisonStatus)
	{

		if (usersCurentStatus == Status.FullyPaid && comparisonStatus == Status.FullyPaid)
		{
			return (MarkupString)"<p class='lead'><b>Task completed</b></p>";
		}
		else
		{
			if (usersCurentStatus == Status.RegistrationFormCompleted && comparisonStatus == Status.RegistrationFormCompleted)
			{
				return (MarkupString)"<p class='lead'><b>Task completed</b></p>";
			}
			else
			{
				if (usersCurentStatus.Value <= comparisonStatus.Value)
				{
					return (MarkupString)(string.Empty);
				}
				else
				{
					return usersCurentStatus.Value <= comparisonStatus.Value ?
									(MarkupString)(string.Empty) :
									(MarkupString)"<p class='lead'><b>Task completed</b></p>";
				}
			}
		}

	}

}
