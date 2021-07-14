namespace LivingMessiah.Web.Domain
{
	public class Address
    {
		public string Name { get; set; }
		public string Street1 { get; set; }
		public string Street2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public int Zip { get; set; }
		public string LatLong { get; set; }
		public string Email { get; set; }

		//public string Phone1 { get; set; }
		//public string Phone2 { get; set; }

		public string FullAddress
		{
			get
			{
				//var result = $"descending? {(isDescending ? "yes" : "no")}";
				return $"{Street1 ?? "na"}  {Street2 ?? ""} {City ?? "na"} {Zip}";
			}
		}

	}
}
