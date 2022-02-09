using LivingMessiah.Web.Domain;

namespace LivingMessiah.Web.Data;

public interface IAddressFactory
{
		LivingMessiah.Web.Domain.Address GetAddress();
}

// ToDo: This is redundant (and in conflict with) LivingMessiah.Web!Address 
public class AddressFactory : IAddressFactory
{
		private LivingMessiah.Web.Domain.Address address { get; set; }

		public LivingMessiah.Web.Domain.Address GetAddress()
		{
				address = new LivingMessiah.Web.Domain.Address
				{
						Name = "Living Messiah Ministries",
						Street1 = "19 North Robson #106",
						City = "Mesa",
						State = "AZ",
						Zip = 85201,
						LatLong = "33.415833, -111.836272",
						//Phone1 = "555.555.1212",
						Email = "info@livingmessiah.com"
				};

				return address;
		}
}
