﻿using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.Enums;

public abstract class Crud : SmartEnum<Crud>
{
	#region Id's
	private static class Id
	{
		internal const int AddRegistration = 1;
		internal const int Edit = 2;
		internal const int Display = 3;
		internal const int DeleteRegistration = 4;
		internal const int DeleteHRA = 5;
		internal const int Repopulate = 6;
		internal const int Donation = 7;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Crud AddRegistration = new AddRegistrationSE();
	public static readonly Crud Edit = new EditSE();
	public static readonly Crud Display = new DisplaySE();
	public static readonly Crud DeleteRegistration = new DeleteRegistrationSE();
	public static readonly Crud DeleteHRA = new DeleteHRASE();
	public static readonly Crud Repopulate = new RepopulateSE();
	public static readonly Crud Donation = new DonationSE();
	#endregion

	private Crud(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Text { get; }
	public abstract string Icon { get; }
	public abstract string Color { get; }
	public abstract string ButtonColor { get; }
	#endregion

	#region Private Instantiation

	private sealed class AddRegistrationSE : Crud
	{
		public AddRegistrationSE() : base($"{nameof(Id.AddRegistration)}", Id.AddRegistration) { }
		public override string Text => "Add Reg.";
		public override string Icon => "fas fa-plus";
		public override string Color => "text-success";
		public override string ButtonColor => "btn btn-outline-success";
	}

	private sealed class EditSE : Crud
	{
		public EditSE() : base($"{nameof(Id.Edit)}", Id.Edit) { }
		public override string Text => "Edit";
		public override string Icon => "fas fa-pencil-alt";
		public override string Color => "text-primary";
		public override string ButtonColor => "btn btn-outline-primary";
	}

	private sealed class DisplaySE : Crud
	{
		public DisplaySE() : base($"{nameof(Id.Display)}", Id.Display) { }
		public override string Text => "Display";
		public override string Icon => "fa fa-binoculars";
		public override string Color => "text-info";
		public override string ButtonColor => "btn btn-outline-info";
	}

	private sealed class DeleteRegistrationSE : Crud
	{
		public DeleteRegistrationSE() : base($"{nameof(Id.DeleteRegistration)}", Id.DeleteRegistration) { }
		public override string Text => "Delete Reg.";
		public override string Icon => "fa fa-times";
		public override string Color => "text-danger";
		public override string ButtonColor => "btn btn-outline-danger";
	}

	private sealed class DeleteHRASE : Crud
	{
		public DeleteHRASE() : base($"{nameof(Id.DeleteHRA)}", Id.DeleteHRA) { }
		public override string Text => "Delete HRA";
		public override string Icon => "fa fa-times";
		public override string Color => "text-danger";
		public override string ButtonColor => "btn btn-outline-danger";
	}

	private sealed class RepopulateSE : Crud
	{
		public RepopulateSE() : base($"{nameof(Id.Repopulate)}", Id.Repopulate) { }
		public override string Text => "Re-populate";
		public override string Icon => "fas fa-retweet";
		public override string Color => "text-warning";
		public override string ButtonColor => "btn btn-outline-warning";
	}

	private sealed class DonationSE : Crud
	{
		public DonationSE() : base($"{nameof(Id.Donation)}", Id.Donation) { }
		public override string Text => "Donation(s)";
		public override string Icon => "fas fa-dollar-sign"; 
		public override string Color => "text-success";
		public override string ButtonColor => "btn btn-outline-success";
	}

	#endregion
}
