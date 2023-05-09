using LivingMessiah.Web.Pages.SpecialEvents.Enums;

namespace LivingMessiah.Web.Pages.SpecialEvents.Stores;

public class SetCommandStateAction
{
	public CommandState CommandState { get; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public SetCommandStateAction() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

	public SetCommandStateAction(CommandState CommandState)
	{
		this.CommandState = CommandState;
	}
}
