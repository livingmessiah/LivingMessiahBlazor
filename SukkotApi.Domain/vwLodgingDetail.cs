using System;

namespace SukkotApi.Domain;

public class vwLodgingDetail
{
		public Int32 Id { get; set; }
		public String FamilyName { get; set; }
		public String CampCD { get; set; }
		public String Status { get; set; }
		public int PeopleCount { get; set; }
		public int LodgingDays { get; set; }
		public Decimal CampCost { get; set; }
		public string CampDays { get; set; }
		public int StatusId { get; set; }
}
