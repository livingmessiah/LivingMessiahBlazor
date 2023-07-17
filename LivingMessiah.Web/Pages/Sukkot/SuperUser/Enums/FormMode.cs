using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.Enums;

public abstract class FormMode : SmartEnum<FormMode>
{
	#region Id's
	private static class Id
	{
		internal const int Add = 1;
		internal const int Edit = 2;
		internal const int Display = 3;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly FormMode Add = new AddSE();
	public static readonly FormMode Edit = new EditSE();
	public static readonly FormMode Display = new DisplaySE();
	#endregion

	private FormMode(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string SubmitText { get; }
	#endregion


	#region Private Instantiation

	private sealed class AddSE : FormMode
	{
		public AddSE() : base($"{nameof(Id.Add)}", Id.Add) { }
		public override string SubmitText => "Add Registration";
	}

	private sealed class EditSE : FormMode
	{
		public EditSE() : base($"{nameof(Id.Edit)}", Id.Edit) { }
		public override string SubmitText => "Update Registration";
	}

	private sealed class DisplaySE : FormMode
	{
		public DisplaySE() : base($"{nameof(Id.Display)}", Id.Display) { }
		public override string SubmitText => "N/A";
	}

	#endregion


}
