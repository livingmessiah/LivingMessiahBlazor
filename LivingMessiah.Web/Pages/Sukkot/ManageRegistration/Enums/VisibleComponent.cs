using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.Sukkot.ManageRegistration.Enums;

public abstract class VisibleComponent : SmartEnum<VisibleComponent>
{
	#region Id's
	private static class Id
	{
		internal const int MasterList = 1;  
		internal const int AddEditForm = 2;
		internal const int DisplayCard = 3;
		internal const int DonationForm = 4;
		internal const int HRA_Form = 5;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly VisibleComponent MasterList = new MasterListSE();
	public static readonly VisibleComponent AddEditForm = new AddEditFormSE();
	public static readonly VisibleComponent DisplayCard = new DisplayCardSE();  // Rename DisplayCard to just Display
	public static readonly VisibleComponent DonationForm = new DonationFormSE();
	public static readonly VisibleComponent HRA_Form = new HRA_FormSE();
	#endregion

	private VisibleComponent(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	//public abstract string Title { get; }
	#endregion


	#region Private Instantiation

	private sealed class MasterListSE : VisibleComponent
	{
		public MasterListSE() : base($"{nameof(Id.MasterList)}", Id.MasterList) { }
	}

	private sealed class AddEditFormSE : VisibleComponent
	{
		public AddEditFormSE() : base($"{nameof(Id.AddEditForm)}", Id.AddEditForm) { }
	}

	private sealed class DisplayCardSE : VisibleComponent
	{
		public DisplayCardSE() : base($"{nameof(Id.DisplayCard)}", Id.DisplayCard) { }
	}

	private sealed class DonationFormSE : VisibleComponent
	{
		public DonationFormSE() : base($"{nameof(Id.DonationForm)}", Id.DonationForm) { }
	}

	private sealed class HRA_FormSE : VisibleComponent
	{
		public HRA_FormSE() : base($"{nameof(Id.HRA_Form)}", Id.HRA_Form) { }
	}

	#endregion
}
// Ignore Spelling: HRA