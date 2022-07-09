using Microsoft.AspNetCore.Components;
using SukkotApi.Domain.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationSteps;

public class MarkupLiterals
{

	public static MarkupString Col_2nd_CheckIcon(Status currentStatus, Status comparisonStatus)
	{
		// "<p><small>not yet completed</small></p>"
		return currentStatus.Value <= comparisonStatus.Value ?
						(MarkupString)(string.Empty) :
						(MarkupString)"<i class='fas fa-check fa-3x'></i>"; 
	}

	public static MarkupString Col_3rd_Heading(Status comparisonStatus, string appendix)
	{
		return (MarkupString)$"<h4>Task - {comparisonStatus.Heading} {appendix}</h4>";
	}

	public static MarkupString Col_3rd_SubHeading(Status currentStatus, Status comparisonStatus)
	{
		return currentStatus.Value <= comparisonStatus.Value ?
						(MarkupString)(string.Empty) :
						(MarkupString)"<p class='lead'><b>Task completed</b></p>";  //
	}

}

/*

	public bool IsXsOrSm { get; set; }
	protected readonly MarkupString AnchorEmail =
		new MarkupString($"<a href='mailto:{Emails.Info.Email()}{Emails.Info.Subject}'>{Emails.Info.Email()}</a>");

	private string _WarningCaretRight = "";
	public MarkupString GetWarningCaretRight() { return IsXsOrSm ? (MarkupString)string.Empty : (MarkupString)_WarningCaretRight; }

*/