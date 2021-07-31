using System;
using System.Collections.Generic;
using SukkotApi.Domain.Enums;

namespace SukkotApi.Domain
{
	public class RegistrationPOCO
	{
		#region Simple Types
		public int Id { get; set; }
		public string FamilyName { get; set; }
		public string FirstName { get; set; }
		public string SpouseName { get; set; }
		public string OtherNames { get; set; }
		public string EMail { get; set; }
		public string Phone { get; set; }
		public int Adults { get; set; }
		public int ChildBig { get; set; }
		public int ChildSmall { get; set; }
		public CampType CampTypeEnum { get; set; }
		public StatusEnum StatusEnum { get; set; }

		public int AttendanceBitwise { get; set; }
		public DateTime? AttendanceStartDate { get; set; }
		public DateTime? AttendanceEndDate { get; set; }
		//public List<DateTime> AttendanceDateList { get; set; }
		public DateTime[] AttendanceDateList { get; set; }

		public int LodgingDaysBitwise { get; set; }
		public DateTime? LodgingStartDate { get; set; }
		public DateTime? LodgingEndDate { get; set; }

		public string Notes { get; set; }
		public string Avitar { get; set; }
		public string AssignedLodging { get; set; }
		public Decimal LmmDonation { get; set; }
		public bool WillHelpWithMeals { get; set; }
		#endregion

		#region HelperMethods
		public int WillHelpWithMealsToInt
		{
			get
			{
				return Convert.ToInt32(WillHelpWithMeals);
			}
		}

		public string NotesScrubbed
		{
			get
			{
				if (!string.IsNullOrEmpty(Notes))
				{
					return Notes.Replace("\"", string.Empty).Replace("'", string.Empty);
				}
				else
				{
					return Notes;
				}

			}
		}
		#endregion
	}
}
