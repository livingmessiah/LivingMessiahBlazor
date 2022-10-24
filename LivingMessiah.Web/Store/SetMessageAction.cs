namespace LivingMessiah.Web.Store;

public class SetMessageAction
{
	public string Message { get; }
	public SetMessageAction() { }

	public SetMessageAction(string Message)
	{
		this.Message = Message;
	}
}
