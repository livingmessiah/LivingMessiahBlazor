using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.Sukkot.ManageRegistration.Enums;

public abstract class Crud : SmartEnum<Crud>
{
	#region Id's
	private static class Id
	{
		internal const int AddRegistration = 1;
		internal const int DeleteHRA = 2;
		internal const int Edit = 3;
		internal const int Donation = 4;
		internal const int Display = 5;
		internal const int DeleteRegistration = 6;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Crud AddRegistration = new AddRegistrationSE();
	public static readonly Crud DeleteHRA = new DeleteHRASE();
	public static readonly Crud Edit = new EditSE();
	public static readonly Crud Donation = new DonationSE();
	public static readonly Crud Display = new DisplaySE();
	public static readonly Crud DeleteRegistration = new DeleteRegistrationSE();
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
	public abstract bool IsStartRegistration { get; }
	#endregion

	#region Private Instantiation

	private sealed class AddRegistrationSE : Crud
	{
		public AddRegistrationSE() : base($"{nameof(Id.AddRegistration)}", Id.AddRegistration) { }
		public override string Text => "Add Reg.";
		public override string Icon => "fas fa-plus";
		public override string Color => "text-success";
		public override string ButtonColor => "btn btn-outline-success";
		public override int Sort => 1;
		public override bool IsStartRegistration => true;
	}

	private sealed class DeleteHRASE : Crud
	{
		public DeleteHRASE() : base($"{nameof(Id.DeleteHRA)}", Id.DeleteHRA) { }
		public override string Text => "Delete HRA";
		public override string Icon => "fa fa-times";
		public override string Color => "text-danger";
		public override string ButtonColor => "btn btn-outline-danger";
		public override int Sort => 2;
		public override bool IsStartRegistration => true;
	}

	private sealed class EditSE : Crud
	{
		public EditSE() : base($"{nameof(Id.Edit)}", Id.Edit) { }
		public override string Text => "Edit";
		public override string Icon => "fas fa-pencil-alt";
		public override string Color => "text-primary";
		public override string ButtonColor => "btn btn-outline-primary";
		public override int Sort => 1;
		public override bool IsStartRegistration => false;
	}

	private sealed class DonationSE : Crud
	{
		public DonationSE() : base($"{nameof(Id.Donation)}", Id.Donation) { }
		public override string Text => "Donations";
		public override string Icon => "fas fa-dollar-sign"; 
		public override string Color => "text-success";
		public override string ButtonColor => "btn btn-outline-success";
		public override int Sort => 2;
		public override bool IsStartRegistration => false;
	}

	private sealed class DisplaySE : Crud
	{
		public DisplaySE() : base($"{nameof(Id.Display)}", Id.Display) { }
		public override string Text => "Display";
		public override string Icon => "fa fa-binoculars";
		public override string Color => "text-info";
		public override string ButtonColor => "btn btn-outline-info";
		public override int Sort => 3;
		public override bool IsStartRegistration => false;
	}

	private sealed class DeleteRegistrationSE : Crud
	{
		public DeleteRegistrationSE() : base($"{nameof(Id.DeleteRegistration)}", Id.DeleteRegistration) { }
		public override string Text => "Delete Reg.";
		public override string Icon => "fa fa-times";
		public override string Color => "text-danger";
		public override string ButtonColor => "btn btn-outline-danger";
		public override int Sort => 4;
		public override bool IsStartRegistration => false;
	}
	#endregion
}
