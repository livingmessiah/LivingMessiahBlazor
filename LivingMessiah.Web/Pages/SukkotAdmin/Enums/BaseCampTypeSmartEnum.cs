using Ardalis.SmartEnum;
using SukkotApi.Domain.Enums;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Enums
{
	public abstract class BaseCampTypeSmartEnum: SmartEnum<BaseCampTypeSmartEnum>
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
		public static readonly BaseCampTypeSmartEnum Offsite = new OffSiteSE();  
		public static readonly BaseCampTypeSmartEnum Tent = new TentSE();
		public static readonly BaseCampTypeSmartEnum RvOrCampTrailer = new RvOrCampTrailerSE();
		public static readonly BaseCampTypeSmartEnum CabinOrBunkhouse = new CabinOrBunkhouseSE();
		public static readonly BaseCampTypeSmartEnum RvDryCampOnly = new RvDryCampOnlySE();
		// SE=
		#endregion

		private BaseCampTypeSmartEnum(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		public abstract string LongDescr { get; }
		//public abstract CampType CampTypeEnum { get; }
		#endregion

		#region Private Instantiation

		private sealed class OffSiteSE : BaseCampTypeSmartEnum
		{
			public OffSiteSE() : base($"{nameof(Id.OffSite)}", Id.OffSite) { }
			public override string LongDescr => "OffSite";
			//public override CampType CampTypeEnum => CampType.OffSite;
		}
		private sealed class TentSE : BaseCampTypeSmartEnum
		{
			public TentSE() : base($"{nameof(Id.Tent)}", Id.Tent) { }
			public override string LongDescr => "Tent";
			//public override CampType CampTypeEnum => CampType.Tent;
		}
		private sealed class RvOrCampTrailerSE : BaseCampTypeSmartEnum
		{
			public RvOrCampTrailerSE() : base($"{nameof(Id.RvOrCampTrailer)}", Id.RvOrCampTrailer) { }
			public override string LongDescr => "RV or Camp Trailer";
			//public override CampType CampTypeEnum => CampType.RvDryCampOnly;
		}
		private sealed class CabinOrBunkhouseSE : BaseCampTypeSmartEnum
		{
			public CabinOrBunkhouseSE() : base($"{nameof(Id.CabinOrBunkhouse)}", Id.CabinOrBunkhouse) { }
			public override string LongDescr => "Indoor Facility";
			//public override CampType CampTypeEnum => CampType.CabinOrBunkhouse;
		}
		private sealed class RvDryCampOnlySE : BaseCampTypeSmartEnum
		{
			public RvDryCampOnlySE() : base($"{nameof(Id.RvDryCampOnly)}", Id.RvDryCampOnly) { }
			public override string LongDescr => "RV Dry Camp Only, NO HOOKUPs";
			//public override CampType CampTypeEnum => CampType.RvDryCampOnly;
		}

		#endregion

	}
}
