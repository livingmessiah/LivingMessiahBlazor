namespace SukkotApi.Domain;

public class vwLodgingDaysPivotOnCampCode
{
		public string LodgingDay2 { get; set; }
		public int Sort { get; set; }
		public int Tent { get; set; }
		public int RVHookup { get; set; }
		public int CabinBH { get; set; }
		public int CabinPeople { get; set; }
		public int RVDryCamp { get; set; }

		public int TotalChargeableUnits
		{
				get
				{
						return Tent + CabinPeople + RVHookup + RVDryCamp;
				}
		}

		public decimal TotalCost
		{
				get
				{
						return TotalChargeableUnits * Constants.LodgingRate;
				}
		}

}
