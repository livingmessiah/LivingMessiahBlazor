using LivingMessiah.Web.Pages.SpecialEvents.Enums;

namespace LivingMessiah.Web.Pages.SpecialEvents.Stores;

public class SetCommandStateAction
{
	public CommandState CommandState { get; }
	public SetCommandStateAction() { }

	public SetCommandStateAction(CommandState CommandState)
	{
		this.CommandState = CommandState;
	}
}
