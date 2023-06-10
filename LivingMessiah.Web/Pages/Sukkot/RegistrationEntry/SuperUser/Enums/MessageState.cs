using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser.Enums;

public abstract class MessageState : SmartEnum<MessageState>
{
	#region Id's
	private static class Id
	{
		internal const int Empty = 1;
		internal const int HRA_Agree = 2;
		internal const int HRA_DoNotAgree = 3;
	}
	#endregion}

	#region  Declared Public Instances
	public static readonly MessageState Empty = new EmptySE();
	public static readonly MessageState HRA_Agree = new HRA_AgreeSE();
	public static readonly MessageState HRA_DoNotAgree = new HRA_DoNotAgreeSE();
	#endregion

	private MessageState(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Text { get; }
	public abstract string Color { get; }
	#endregion

	#region Private Instantiation

	private sealed class EmptySE : MessageState
	{
		public EmptySE() : base($"{nameof(Id.Empty)}", Id.Empty) { }
		public override string Text => string.Empty;
		public override string Color => "";
	}

	private sealed class HRA_AgreeSE : MessageState
	{
		public HRA_AgreeSE() : base($"{nameof(Id.HRA_Agree)}", Id.HRA_Agree) { }
		public override string Text => "Record updated for House Rules Agreement";
		public override string Color => "text-success";
	}

	private sealed class HRA_DoNotAgreeSE : MessageState
	{
		public HRA_DoNotAgreeSE() : base($"{nameof(Id.HRA_DoNotAgree)}", Id.HRA_DoNotAgree) { }
		public override string Text => "Not agreeing to the House Rules Agreement terminates the Registration Process";
		public override string Color => "text-danger";
	}

	#endregion
}
