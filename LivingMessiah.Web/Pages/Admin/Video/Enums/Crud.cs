using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.Admin.Video.Enums;

public abstract class Crud : SmartEnum<Crud>
{
	#region Id's
	private static class Id
	{
		internal const int Add = 1;
		internal const int Delete = 2;
		internal const int Edit = 3;
		//internal const int Display = 4;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Crud Add = new AddSE();
	public static readonly Crud Delete = new DeleteSE();
	public static readonly Crud Edit = new EditSE();
	//public static readonly Crud Display = new DisplaySE();
	#endregion

	private Crud(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Text { get; }
	public abstract string Icon { get; }
	public abstract string Color { get; }
	public abstract string ButtonColor { get; }
	public abstract int Sort { get; }
	public abstract bool IsAddMode { get; }
	#endregion

	#region Private Instantiation

	private sealed class AddSE : Crud
	{
		public AddSE() : base($"{nameof(Id.Add)}", Id.Add) { }
		public override string Text => "Add Video";
		public override string Icon => "fas fa-plus";
		public override string Color => "text-success";
		public override string ButtonColor => "btn btn-outline-success";
		public override int Sort => 1;
		public override bool IsAddMode => true;
	}

	private sealed class DeleteSE : Crud
	{
		public DeleteSE() : base($"{nameof(Id.Delete)}", Id.Delete) { }
		public override string Text => "Delete Video";
		public override string Icon => "fa fa-times";
		public override string Color => "text-danger";
		public override string ButtonColor => "btn btn-outline-danger";
		public override int Sort => 2;
		public override bool IsAddMode => false;
	}

	private sealed class EditSE : Crud
	{
		public EditSE() : base($"{nameof(Id.Edit)}", Id.Edit) { }
		public override string Text => "Edit";
		public override string Icon => "fas fa-pencil-alt";
		public override string Color => "text-primary";
		public override string ButtonColor => "btn btn-outline-primary";
		public override int Sort => 1;
		public override bool IsAddMode => false;
	}

	/*
	private sealed class DisplaySE : Crud
	{
		public DisplaySE() : base($"{nameof(Id.Display)}", Id.Display) { }
		public override string Text => "Display";
		public override string Icon => "fa fa-binoculars";
		public override string Color => "text-info";
		public override string ButtonColor => "btn btn-outline-info";
		public override int Sort => 3;
	}
	*/
	#endregion
}
