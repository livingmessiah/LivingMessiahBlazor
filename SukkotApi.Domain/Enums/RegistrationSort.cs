using System;
using System.Collections.Generic;
using System.Linq;

namespace SukkotApi.Domain.Enums
{

	public enum RegistrationSortEnum
	{
		Id = 1,
		LastName = 2,
		FirstName = 3
	}

	public class RegistrationSort
	{
		public static List<RegistrationSort> All { get; } = new List<RegistrationSort>();
		public static RegistrationSort ById { get; } = new RegistrationSort(RegistrationSortEnum.Id, "Id", "Id");
		public static RegistrationSort ByLastName { get; } = new RegistrationSort(RegistrationSortEnum.LastName, "Last Name", "FamilyName");
		public static RegistrationSort ByFirstName { get; } = new RegistrationSort(RegistrationSortEnum.FirstName, "First Name", "FirstName");

		public RegistrationSortEnum RegistrationSortEnum { get; private set; }
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string SqlFieldName { get; private set; }

		private RegistrationSort(RegistrationSortEnum registrationSortEnum, string name, string sqlFieldName)
		{
			RegistrationSortEnum = registrationSortEnum;
			Id = (int)registrationSortEnum;
			Name = name;
			SqlFieldName = sqlFieldName;
			All.Add(this);
		}

		public static RegistrationSort FromEnum(RegistrationSortEnum enumValue)
		{
			return All.SingleOrDefault(r => r.RegistrationSortEnum == enumValue);
		}

		public static RegistrationSort FromString(string formatString)
		{
			return All.SingleOrDefault(r => String.Equals(r.Name, formatString, StringComparison.OrdinalIgnoreCase));
		}
	}
}
