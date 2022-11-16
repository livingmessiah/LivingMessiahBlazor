namespace LivingMessiah.Web.Pages.SpecialEvents.Stores;

public class SetDateRangeAction
{
	public DateRange DateRange { get; }
	public SetDateRangeAction() { }

	public SetDateRangeAction(DateRange DateRange)
	{
		this.DateRange = DateRange;
	}
}
