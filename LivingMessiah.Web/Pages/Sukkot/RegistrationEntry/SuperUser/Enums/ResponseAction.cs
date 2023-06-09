using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser.Enums;

public abstract class ResponseAction : SmartEnum<ResponseAction>
{
	#region Id's
	private static class Id
	{
		internal const int Get_Items = 1;                 // Get_EditItem_Action; FormMode = action.FormMode Get HDR and Registrations records to populate MasterList
		internal const int Get_Item_To_Edit = 2;          // Edit_Action: VisibleComponent=AddEit, FormMode=Edit
		internal const int Get_Item_To_Display = 3;       // Display_Action: VisibleComponent=DisplayCard, FormMode=Edit
		internal const int Request_HRA_Add = 4;						// Add_HRA_Action
		internal const int Request_HRA_Delete = 5;        // Delete_HRA_Action
		internal const int Request_Registration_Add = 6;	// Submitting_Request_Action Enums.FormMode!Add
		internal const int Request_Registration_Edit = 7; // Submitting_Request_Action Enums.FormMode!Edit
		//internal const int Request_Donation = 8;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly ResponseAction Get_Items = new Get_ItemsSE();
	public static readonly ResponseAction Get_Item_To_Edit = new Get_Item_To_EditSE();
	public static readonly ResponseAction Get_Item_To_Display = new Get_Item_To_DisplaySE();  
	#endregion

	private ResponseAction(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract bool DisplayToast { get; }
	#endregion


	#region Private Instantiation

	private sealed class Get_ItemsSE : ResponseAction
	{
		public Get_ItemsSE() : base($"{nameof(Id.Get_Items)}", Id.Get_Items) { }
		public override bool DisplayToast => false;
	}


	private sealed class Get_Item_To_EditSE : ResponseAction
	{
		public Get_Item_To_EditSE() : base($"{nameof(Id.Get_Item_To_Edit)}", Id.Get_Item_To_Edit) { }
		public override bool DisplayToast => true;
	}
	private sealed class Get_Item_To_DisplaySE : ResponseAction
	{
		public Get_Item_To_DisplaySE() : base($"{nameof(Id.Get_Item_To_Display)}", Id.Get_Item_To_Display) { }
		public override bool DisplayToast => true;
	}

	#endregion
}
