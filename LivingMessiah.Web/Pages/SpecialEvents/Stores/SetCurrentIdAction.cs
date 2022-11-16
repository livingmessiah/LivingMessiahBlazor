namespace LivingMessiah.Web.Pages.SpecialEvents.Stores;

public class SetCurrentIdAction
{
	public int CurrentId { get; }
	public SetCurrentIdAction() { }

	public SetCurrentIdAction(int CurrentId)
	{
		this.CurrentId = CurrentId;
	}
}
