using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static SukkotApi.Domain.Constants;

namespace SukkotApi.Domain
{
	public class vwRegistration
	{
		public int Id { get; set; }
		[DisplayName("Family Name")]
		public string FamilyName { get; set; }

		[DisplayName("First Name")]
		public string FirstName { get; set; }
		
		[DisplayName("Spouse Name")]
		public string SpouseName { get; set; }

		[DisplayName("Other Names")]
		public string OtherNames { get; set; }

		[DataType(DataType.EmailAddress)]
		public string EMail { get; set; }

		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }

		[DisplayName("Number of Adults")]
		public int Adults { get; set; }

		[DisplayName("Children 6 to 12")]
		public int ChildBig { get; set; }
		[DisplayName("Children under 6")]
		public int ChildSmall { get; set; }

		[DisplayName("WHWM?")]
		public bool WillHelpWithMeals { get; set; }

		public int StatusId { get; set; } 

		[DisplayName("Lodging Days Total")]
		public int LodgingDaysTotal { get; set; }

		public int LodgingDaysBitwise { get; set; }

		[DisplayName("Attendance Total")]
		public int AttendanceTotal { get; set; }

		public string Camp { get; set; }
		public string CampCD { get; set; }
		public string Status { get; set; }
		public string StatusCD { get; set; }

		[DataType(DataType.Currency)]
		public decimal RegistrationFee { get; set; }

		[DataType(DataType.Currency)]
		[DisplayName("Camp Cost")]
		public decimal CampCost { get; set; }

		[DisplayName("Paid")]
		[DataType(DataType.Currency)]
		public decimal LmmDonation { get; set; }

		public string Notes { get; set; }
		public string AssignedLodging { get; set; }

		//Regular
		public int TotalAdultLun { get; set; }
		public int TotalAdultDin { get; set; }
		public int TotalChildBigLun { get; set; }
		public int TotalChildBigDin { get; set; }
		public int TotalChildSmallLun { get; set; }
		public int TotalChildSmallDin { get; set; }

		//Vegetarian
		public int TotalAdultLunVeg { get; set; }
		public int TotalAdultDinVeg { get; set; }
		public int TotalChildBigLunVeg { get; set; }
		public int TotalChildBigDinVeg { get; set; }
		public int TotalChildSmallLunVeg { get; set; }
		public int TotalChildSmallDinVeg { get; set; }

		public int AttendanceBitwise { get; set; }

		public string PayWithCheckMessage { get; set; }

		public string FullName(bool includeOthers)
		{
			string s = FirstName;
			if (!string.IsNullOrEmpty(SpouseName)) { s += " and " + SpouseName;  }
			s += " " + FamilyName;
			if (includeOthers) { s += " and " + OtherNames;			}
			return s;
		}

		public int TotalAdultMeals
		{
			get
			{
				return TotalAdultLun + TotalAdultDin + TotalAdultLunVeg + TotalAdultDinVeg;
			}
		}

		[DataType(DataType.Currency)]
		public decimal AdultMealCost
		{
			get
			{
				return TotalAdultMeals * AdultMeals;
			}
		}

		
		public int TotalChildBigMeals
		{
			get
			{
				return TotalChildBigLun + TotalChildBigDin + TotalChildBigLunVeg + TotalChildBigDinVeg;
			}
		}


		[DataType(DataType.Currency)]
		public decimal ChildBigMealCost
		{
			get
			{
				return TotalChildBigMeals * ChildMeals; // 2.5m
			}
		}

		[DataType(DataType.Currency)]
		public decimal TotalCost
		{
			get
			{
				return CampCost + AdultMealCost + ChildBigMealCost + RegistrationFee;
			}
		}

	}
}
