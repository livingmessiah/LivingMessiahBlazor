using Ardalis.SmartEnum;
using SukkotApi.Domain.Enums;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Enums;

public abstract class BaseLocationSmartEnum : SmartEnum<BaseLocationSmartEnum>
{

		#region Id's
		private static class Id
		{
				internal const int GTHF = 1;
				internal const int Wilderness = 2;
				internal const int Windmill = 3;
		}
		#endregion


		#region  Declared Public Instances
		public static readonly BaseLocationSmartEnum GreenhouseTrolleyHobbyFarm = new GreenhouseTrolleyHobbyFarmSE();
		public static readonly BaseLocationSmartEnum WildernessRanch = new WildernessRanchSE();
		public static readonly BaseLocationSmartEnum WindmillRanch = new WindmillRanchSE();
		//SE=SmartEnum
		#endregion

		private BaseLocationSmartEnum(string name, int value) : base(name, value) { } // Constructor


		#region Extra Fields
		public abstract string ShortDescr { get; }
		public abstract string LongDescr { get; }
		public abstract string TextColor { get; }
		//public abstract LocationEnum LocationEnum { get; }
		#endregion

		#region Private Instantiation
		private sealed class GreenhouseTrolleyHobbyFarmSE : BaseLocationSmartEnum
		{
				public GreenhouseTrolleyHobbyFarmSE() : base($"{nameof(Id.GTHF)}", Id.GTHF) { }
				public override string ShortDescr => "Greenhouse Trolley Hobby Farm";
				public override string TextColor => "text-success";
				public override string LongDescr => "Greenhouse Trolley Hobby Farm (Near Sierra Vista)";
				//public override LocationEnum LocationEnum => LocationEnum.GreenhouseTrolleyHobbyFarm;
		}

		private sealed class WildernessRanchSE : BaseLocationSmartEnum
		{
				public WildernessRanchSE() : base($"{nameof(Id.Wilderness)}", Id.Wilderness) { }
				public override string ShortDescr => "Wilderness Ranch";
				public override string TextColor => "text-danger";
				public override string LongDescr => "Wilderness Ranch (Near Show Low)";
				//public override LocationEnum LocationEnum => LocationEnum.WildernessRanch;
		}

		private sealed class WindmillRanchSE : BaseLocationSmartEnum
		{
				public WindmillRanchSE() : base($"{nameof(Id.Windmill)}", Id.Windmill) { }
				public override string ShortDescr => "Windmill Ranch";
				public override string TextColor => "text-warning";
				public override string LongDescr => "Windmill Ranch (Near Bisbee)";
				//public override LocationEnum LocationEnum => LocationEnum.WindmillRanch;
		}
		#endregion

}
