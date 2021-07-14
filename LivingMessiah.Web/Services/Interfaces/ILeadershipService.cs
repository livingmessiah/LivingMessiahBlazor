using LivingMessiah.Web.Domain;
using System.Collections.Generic;

namespace LivingMessiah.Web.Services
{
	public interface ILeadershipService
	{
		List<Domain.Person> LoadPeople();
	}
}
