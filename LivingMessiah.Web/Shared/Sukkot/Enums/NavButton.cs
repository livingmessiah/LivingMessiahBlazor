using Ardalis.SmartEnum;
using Link = LivingMessiah.Web.Links.Sukkot;

namespace LivingMessiah.Web.Shared.Sukkot.Enums;

public abstract class NavButton : SmartEnum<NavButton>
{
	#region Id's
	private static class Id
	{
		internal const int Details = 1;
		internal const int DetailsPrint = 2;
		internal const int Edit = 3;
		internal const int DeleteConfirmation = 4;
		internal const int Donation = 5;
		internal const int CreateRegistrationForm = 6;
		internal const int Add = 7;
	}
	#endregion

	private NavButton(string name, int value) : base(name, value) { } // Constructor

	#region  Declared Public Instances
	public static readonly NavButton Details = new DetailsSE();
	public static readonly NavButton DetailsPrint = new DetailsPrintSE();
	public static readonly NavButton Edit = new EditSE();
	public static readonly NavButton DeleteConfirmation = new DeleteConfirmationSE();
	public static readonly NavButton Donation = new DonationSE();
	public static readonly NavButton CreateRegistrationForm = new CreateRegistrationFormSE();
	public static readonly NavButton Add = new AddSE();
	// SE=SmartEnum
	#endregion

	#region Extra Fields
	public abstract string Label { get; }
	public abstract string Icon { get; }
	public abstract string Title { get; }
	public abstract string Css { get; }
	public abstract string Route { get; }
	public abstract string RouteSuffix { get; }
	#endregion

	#region Private Instantiation

	private sealed class DetailsSE : NavButton
	{
		public DetailsSE() : base($"{nameof(Id.Details)}", Id.Details) { }
		public override string Label => "View";
		public override string Title => "Registration View";
		public override string Icon => "fas fa-info";
		public override string Css => "btn btn-outline-info";
		public override string Route => Link.Details;
		public override string RouteSuffix => "/false";
	}

	private sealed class DetailsPrintSE: NavButton
	{
		public DetailsPrintSE() : base($"{nameof(Id.DetailsPrint)}", Id.DetailsPrint) { }
		public override string Label => "Print";
		public override string Title => "Registration Print";
		public override string Icon => "fas fa-print";
		public override string Css => "btn btn-outline-info";
		public override string Route => Link.Details;
		public override string RouteSuffix => "/true";
	}

	private sealed class EditSE : NavButton
	{
		public EditSE() : base($"{nameof(Id.Edit)}", Id.Edit) { }
		public override string Label => "Edit";
		public override string Title => "Registration Edit";
		public override string Icon => "fas fa-edit";
		public override string Css => "btn btn-outline-success";
		public override string Route => Link.CreateEdit;
		public override string RouteSuffix => "";
	}

	private sealed class DeleteConfirmationSE : NavButton
	{
		public DeleteConfirmationSE() : base($"{nameof(Id.DeleteConfirmation)}", Id.DeleteConfirmation) { }
		public override string Label => "Delete";
		public override string Title => "Delete";
		public override string Icon => "fas fa-times";
		public override string Css => "btn btn-outline-danger";
		public override string Route => Link.DeleteConfirmation;
		public override string RouteSuffix => "";
	}

	private sealed class DonationSE : NavButton
	{
		public DonationSE() : base($"{nameof(Id.Donation)}", Id.Donation) { }
		public override string Label => "Donation";
		public override string Title => "Donation";
		public override string Icon => "fab fa-paypal";
		public override string Css => "btn btn-warning";  // btn-sm
		public override string Route => Link.Links2.Payment;
		public override string RouteSuffix => "";
	}

	private sealed class CreateRegistrationFormSE : NavButton
	{
		public CreateRegistrationFormSE() : base($"{nameof(Id.CreateRegistrationForm)}", Id.CreateRegistrationForm) { }
		public override string Label => "Create Registration";
		public override string Title => "Create Registration";
		public override string Icon => "fas fa-plus";  //fas fa-chevron-right I want this to be on the right, not the left (default)
		public override string Css => "btn-success btn-lg mb-3";  // btn-success btn-lg mb-3
		public override string Route => Link.CreateEdit;
		public override string RouteSuffix => "";
	}

	private sealed class AddSE : NavButton
	{
		public AddSE() : base($"{nameof(Id.Add)}", Id.Add) { }
		public override string Label => "Add";
		public override string Title => "Add";
		public override string Icon => "fas fa-plus";  
		public override string Css => "btn btn-success";  // btn-success btn-lg mb-3
		public override string Route => Link.CreateEdit;
		public override string RouteSuffix => "";
	}

	#endregion

}
