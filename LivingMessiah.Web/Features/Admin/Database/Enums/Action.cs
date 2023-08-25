using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Features.Admin.Database.Enums;

public abstract class Action : SmartEnum<Action>
{
	#region Id's
	private static class Id
	{
		internal const int TestInsert = 1;
		internal const int EmptyLog = 2;
	}
	#endregion


	#region  Declared Public Instances
	public static readonly Action TestInsert = new TestInsertSE();
	public static readonly Action EmptyLog = new EmptyLogSE();
	// SE=SmartEnum
	#endregion

	private Action(string name, int value) : base(name, value) { }  // Constructor

	#region Extra Fields
	public abstract string Title { get; }
	#endregion

	#region Private Instantiation

	private sealed class TestInsertSE : Action
	{
		public TestInsertSE() : base("TestInsert", Id.TestInsert) { }
		public override string Title => "Test Insert";
	}

	private sealed class EmptyLogSE : Action
	{
		public EmptyLogSE() : base("EmptyLog", Id.EmptyLog) { }
		public override string Title => "Empty Log";
	}

	#endregion

}
