using System;
using System.Collections.Generic;
using System.Linq;

namespace SukkotApi.Domain.Donations.Enums
{
	public enum DonationStatusEnum
	{
		FullList = 0,
		NoPayments = 2,  // either 2 (RFC) or 3 (MFC)
		PartiallyPaid = 4,
		FullyPaid = 5
	}

	public class DonationStatus
	{
		public static List<DonationStatus> All { get; } = new List<DonationStatus>();

		public static DonationStatus FullList { get; } = new DonationStatus(DonationStatusEnum.FullList, "Full List", null, null, "FirstName, FamilyName", "Id");
		public static DonationStatus NoPayments { get; } = new DonationStatus(DonationStatusEnum.NoPayments, "No Payments", 2, 3, "FirstName, FamilyName", "Id");
		public static DonationStatus PartiallyPaid { get; } = new DonationStatus(DonationStatusEnum.PartiallyPaid, "Partially Paid", 4, null, "FirstName, FamilyName", "Id");
		public static DonationStatus FullyPaid { get; } = new DonationStatus(DonationStatusEnum.FullyPaid, "Fully Paid", 5, null, "FirstName, FamilyName", "Id");

		private DonationStatus(DonationStatusEnum donationStatusEnum, string name, int? statusId1, int? statusId2, string sortFieldName, string sortFieldId)
		{
			DonationStatusEnum = donationStatusEnum;
			Name = name;
			StatusId1 = statusId1;
			StatusId2 = statusId2;
			SortFieldName = sortFieldName;
			SortFieldId = sortFieldId;
			All.Add(this);
		}

		public DonationStatusEnum DonationStatusEnum { get; private set; }
		public string Name { get; private set; }
		public int? StatusId1 { get; private set; }
		public int? StatusId2 { get; private set; }
		public string SortFieldName { get; private set; }
		public string SortFieldId { get; private set; }

		public static DonationStatus FromString(string formatString)
		{
			return All.Single(r => String.Equals(r.Name, formatString, StringComparison.OrdinalIgnoreCase));
		}

		public static DonationStatus FromEnum(DonationStatusEnum enumValue)
		{
			return All.SingleOrDefault(r => r.DonationStatusEnum == enumValue);
		}

		//public static Dictionary<string, string> DictionaryList()
		//{
		//	Dictionary<string, string> d = new Dictionary<string, string>();
		//	foreach (DonationStatus f in All)
		//	{
		//		d.Add(f.DonationStatusEnum.ToString(), f.Name);
		//	}
		//	return d;
		//}

	}

}
