namespace LivingMessiah.Web.Store;

public class SetEnabledAction
{
	public bool Enabled { get; }
	public SetEnabledAction() { }

	public SetEnabledAction(bool Enabled)
	{
		this.Enabled = Enabled;
	}
}
