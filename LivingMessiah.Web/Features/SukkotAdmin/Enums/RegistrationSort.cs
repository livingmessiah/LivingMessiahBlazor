using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Features.SukkotAdmin.Enums;

public abstract class RegistrationSort : SmartEnum<RegistrationSort>
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
		public static readonly RegistrationSort ById = new ByIdSE();
		public static readonly RegistrationSort ByLastName = new ByLastNameSE();
		public static readonly RegistrationSort ByFirstName = new ByFirstNameSE();
		public static readonly RegistrationSort ByIdDesc = new ByIdDescSE();
		public static readonly RegistrationSort ByLastNameDesc = new ByLastNameDescSE();
		public static readonly RegistrationSort ByFirstNameDesc = new ByFirstNameDescSE();
		// Note: SE=SmartEnum
		#endregion

		private RegistrationSort(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		public abstract string ColumnName { get; }
		public abstract string SqlTableColumnName { get; }
		public abstract string Order { get; }
		#endregion

		#region Explicit ie Named Instances
		private sealed class ByIdSE : RegistrationSort
		{
				public ByIdSE() : base($"{nameof(Id.ById)}", Id.ById) { }
				public override string ColumnName => "Id"; public override string SqlTableColumnName => "Id"; public override string Order => "";
		}

		private sealed class ByLastNameSE : RegistrationSort
		{
				public ByLastNameSE() : base($"{nameof(Id.ByLastName)}", Id.ByLastName) { }
				public override string ColumnName => "Last Name"; public override string SqlTableColumnName => "FamilyName"; public override string Order => "";
		}

		private sealed class ByFirstNameSE : RegistrationSort
		{
				public ByFirstNameSE() : base($"{nameof(Id.ByFirstName)}", Id.ByFirstName) { }
				public override string ColumnName => "First Name"; public override string SqlTableColumnName => "FirstName"; public override string Order => "";
		}

		private sealed class ByIdDescSE : RegistrationSort
		{
				public ByIdDescSE() : base($"{nameof(Id.ByIdDesc)}", Id.ByIdDesc) { }
				public override string ColumnName => "Id"; public override string SqlTableColumnName => "Id"; public override string Order => " Desc";
		}

		private sealed class ByLastNameDescSE : RegistrationSort
		{
				public ByLastNameDescSE() : base($"{nameof(Id.ByLastNameDesc)}", Id.ByLastNameDesc) { }
				public override string ColumnName => "Last Name"; public override string SqlTableColumnName => "FamilyName"; public override string Order => " Desc";
		}

		private sealed class ByFirstNameDescSE : RegistrationSort
		{
				public ByFirstNameDescSE() : base($"{nameof(Id.ByFirstNameDesc)}", Id.ByFirstNameDesc) { }
				public override string ColumnName => "First Name"; public override string SqlTableColumnName => "FirstName"; public override string Order => " Desc";
		}

		#endregion

}
