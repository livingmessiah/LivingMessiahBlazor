//using LivingMessiah.Web.Pages.SpecialEvents.Enums;

namespace LivingMessiah.Web.Pages.SpecialEvents.Stores;

[FeatureState]
public class MainState
{
	public DateRange DateRange { get; }
	public Enums.CommandState CommandState { get; }
	public int CurrentId { get; }

	public MainState() { }
	public MainState(DateRange DateRange, Enums.CommandState CommandState, int CurrentId) 
	{
		this.DateRange = DateRange;
		this.CommandState = CommandState;
		this.CurrentId = CurrentId;
	}
}

