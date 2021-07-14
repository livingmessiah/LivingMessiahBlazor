using System.Collections.Generic;
using LivingMessiah.Web.Domain;
using LivingMessiah.Web.Data;

namespace LivingMessiah.Web.Services
{
	public interface IAddressService
	{
		LivingMessiah.Web.Domain.Address GetAddress();
	}

	public class AddressService : IAddressService
	{
		public LivingMessiah.Web.Domain.Address GetAddress()
		{
			AddressFactory address = new AddressFactory();
			return address.GetAddress();
		}
	}
}
