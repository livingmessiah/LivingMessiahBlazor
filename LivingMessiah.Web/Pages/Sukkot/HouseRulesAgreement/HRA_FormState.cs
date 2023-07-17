using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.Sukkot.HouseRulesAgreement;

public abstract class HRA_FormState : SmartEnum<HRA_FormState>
{
	#region Id's
	private static class Id
	{
		internal const int Start = 1;
		internal const int Agreed = 2;
		internal const int DidNotAgree = 3;
		//internal const int WaitingForEmailEntry = 4;
	}
	#endregion}

	#region  Declared Public Instances

	public static readonly HRA_FormState Start = new StartSE(); 
	public static readonly HRA_FormState Agreed = new AgreedSE();
	public static readonly HRA_FormState DidNotAgree = new DidNotAgreeSE();
	#endregion

	private HRA_FormState(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Text { get; }
	public abstract string Color { get; }
	#endregion

	#region Private Instantiation

	private sealed class StartSE : HRA_FormState
	{
		public StartSE() : base($"{nameof(Id.Start)}", Id.Start) { }
		public override string Text => "Waiting for user's response to Agree or NOT Agree"; //Click the Red or Green button
		public override string Color => "text-info";
	}

	private sealed class AgreedSE : HRA_FormState
	{
		public AgreedSE() : base($"{nameof(Id.Agreed)}", Id.Agreed) { }
		public override string Text => "User agreed to the House Rules Agreement, waiting to add their email";  // Record updated for House Rules Agreement
		public override string Color => "text-success";
	}

	private sealed class DidNotAgreeSE : HRA_FormState
	{
		public DidNotAgreeSE() : base($"{nameof(Id.DidNotAgree)}", Id.DidNotAgree) { }
		public override string Text => "Not agreeing to the House Rules Agreement terminates the Registration Process";
		public override string Color => "text-danger";
	}

	#endregion
}
