//using LivingMessiah.Web.Pages.SpecialEvents.Enums;

namespace LivingMessiah.Web.Pages.SpecialEvents.Stores;

[FeatureState]
public class MainState
{
	public DateRange DateRange { get; }
	public Enums.CommandState CommandState { get; }
	public int CurrentId { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public MainState() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public MainState(DateRange DateRange, Enums.CommandState CommandState, int CurrentId) 
	{
		this.DateRange = DateRange;
		this.CommandState = CommandState;
		this.CurrentId = CurrentId;
	}
}

