using Ardalis.SmartEnum;

namespace SukkotApi.Domain.Enums
{
	public abstract class BaseStatusSmartEnum: SmartEnum<BaseStatusSmartEnum>
	{

		#region Id's
		private static class Id
		{
			internal const int OffSite = 1;
			internal const int Tent = 2;
			internal const int RvOrCampTrailer = 3;
			internal const int CabinOrBunkhouse = 4;
			internal const int RvDryCampOnly = 5;
		}
		#endregion

		#region  Declared Public Instances
		public static readonly BaseStatusSmartEnum Offsite = new OffSiteSE();  
		public static readonly BaseStatusSmartEnum Tent = new TentSE();
		public static readonly BaseStatusSmartEnum RvOrCampTrailer = new RvOrCampTrailerSE();
		public static readonly BaseStatusSmartEnum CabinOrBunkhouse = new CabinOrBunkhouseSE();
		public static readonly BaseStatusSmartEnum RvDryCampOnly = new RvDryCampOnlySE();
		// SE=
		#endregion

		private BaseStatusSmartEnum(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		public abstract string LongDescr { get; }
		#endregion

		#region Private Instantiation

		private sealed class OffSiteSE : BaseStatusSmartEnum
		{
			public OffSiteSE() : base($"{nameof(Id.OffSite)}", Id.OffSite) { }
			public override string LongDescr => "OffSite"; 
		}
		private sealed class TentSE : BaseStatusSmartEnum
		{
			public TentSE() : base($"{nameof(Id.Tent)}", Id.Tent) { }
			public override string LongDescr => "Tent";
		}
		private sealed class RvOrCampTrailerSE : BaseStatusSmartEnum
		{
			public RvOrCampTrailerSE() : base($"{nameof(Id.RvOrCampTrailer)}", Id.RvOrCampTrailer) { }
			public override string LongDescr => "RV or Camp Trailer";
		}
		private sealed class CabinOrBunkhouseSE : BaseStatusSmartEnum
		{
			public CabinOrBunkhouseSE() : base($"{nameof(Id.CabinOrBunkhouse)}", Id.CabinOrBunkhouse) { }
			public override string LongDescr => "Indoor Facility";
		}
		private sealed class RvDryCampOnlySE : BaseStatusSmartEnum
		{
			public RvDryCampOnlySE() : base($"{nameof(Id.RvDryCampOnly)}", Id.RvDryCampOnly) { }
			public override string LongDescr => "RV Dry Camp Only, NO HOOKUPs";
		}

		#endregion

	}
}
