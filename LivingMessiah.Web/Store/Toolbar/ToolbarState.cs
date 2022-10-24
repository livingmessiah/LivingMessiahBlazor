namespace LivingMessiah.Web.Store.Toolbar;

[FeatureState]
public class ToolbarState
{
	public string Message { get; }
	public bool Enabled { get; }
	public ToolbarState() { }

	public ToolbarState(string Message, bool Enabled)
	{
		this.Message = Message;
		this.Enabled = Enabled;
	}
}

