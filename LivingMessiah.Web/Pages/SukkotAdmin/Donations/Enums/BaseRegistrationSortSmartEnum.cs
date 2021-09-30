using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Donations.Enums
{
	public abstract class BaseRegistrationSortSmartEnum : SmartEnum<BaseRegistrationSortSmartEnum>
	{		
		#region Id's
		private static class Id
		{
			internal const int ById = 1;
			internal const int ByLastName = 2;
			internal const int ByFirstName = 3;
			internal const int ByIdDesc = 4;
			internal const int ByLastNameDesc = 5;
			internal const int ByFirstNameDesc = 6;
		}
		#endregion

		#region Explicit ie Named Instances
		public static readonly BaseRegistrationSortSmartEnum ById = new ByIdSE();  
		public static readonly BaseRegistrationSortSmartEnum ByLastName = new ByLastNameSE();
		public static readonly BaseRegistrationSortSmartEnum ByFirstName = new ByFirstNameSE();
		public static readonly BaseRegistrationSortSmartEnum ByIdDesc = new ByIdDescSE();
		public static readonly BaseRegistrationSortSmartEnum ByLastNameDesc = new ByLastNameDescSE();
		public static readonly BaseRegistrationSortSmartEnum ByFirstNameDesc = new ByFirstNameDescSE();
		// Note: SE=SmartEnum
		#endregion

		private BaseRegistrationSortSmartEnum(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		public abstract string ColumnName { get; }
		public abstract string SqlTableColumnName { get; }
		public abstract string Order { get; }
		#endregion

		#region Explicit ie Named Instances
		private sealed class ByIdSE : BaseRegistrationSortSmartEnum
		{
			public ByIdSE() : base($"{nameof(Id.ById)}", Id.ById) { }
			public override string ColumnName => "Id"; public override string SqlTableColumnName => "Id"; public override string Order => "";
		}

		private sealed class ByLastNameSE : BaseRegistrationSortSmartEnum
		{
			public ByLastNameSE() : base($"{nameof(Id.ByLastName)}", Id.ByLastName) { }
			public override string ColumnName => "Last Name"; public override string SqlTableColumnName => "FamilyName"; public override string Order => "";
		}

		private sealed class ByFirstNameSE : BaseRegistrationSortSmartEnum
		{
			public ByFirstNameSE() : base($"{nameof(Id.ByFirstName)}", Id.ByFirstName) { }
			public override string ColumnName => "First Name"; public override string SqlTableColumnName => "FirstName"; public override string Order => "";
		}

		private sealed class ByIdDescSE : BaseRegistrationSortSmartEnum
		{
			public ByIdDescSE() : base($"{nameof(Id.ByIdDesc)}", Id.ByIdDesc) { }
			public override string ColumnName => "Id"; public override string SqlTableColumnName => "Id"; public override string Order => " Desc";
		}

		private sealed class ByLastNameDescSE : BaseRegistrationSortSmartEnum
		{
			public ByLastNameDescSE() : base($"{nameof(Id.ByLastNameDesc)}", Id.ByLastNameDesc) { }
			public override string ColumnName => "Last Name"; public override string SqlTableColumnName => "FamilyName"; public override string Order => " Desc";
		}

		private sealed class ByFirstNameDescSE : BaseRegistrationSortSmartEnum
		{
			public ByFirstNameDescSE() : base($"{nameof(Id.ByFirstNameDesc)}", Id.ByFirstNameDesc) { }
			public override string ColumnName => "First Name"; public override string SqlTableColumnName => "FirstName"; public override string Order => " Desc";
		}

		#endregion

	}
}
