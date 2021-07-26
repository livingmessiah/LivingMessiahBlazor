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
		public IEnumerable<string> AttendanceDayList { get; set; }
		public int LodgingDaysBitwise { get; set; }
		public string[] LodgingDayList { get; set; }
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

		public int TotalLodgingDays
		{
			get
			{
				return CountLodging((SukkotLodgingDays)LodgingDaysBitwise);
			}
		}

		private static int CountLodging(SukkotLodgingDays days)
		{
			int count = 0;
			count += days.HasFlag(SukkotLodgingDays.Sep_30_Wed) ? 1 : 0;
			count += days.HasFlag(SukkotLodgingDays.Oct_01_Thu) ? 1 : 0;
			count += days.HasFlag(SukkotLodgingDays.Oct_02_Fri) ? 1 : 0;
			count += days.HasFlag(SukkotLodgingDays.Oct_03_Sat) ? 1 : 0;
			count += days.HasFlag(SukkotLodgingDays.Oct_04_Sun) ? 1 : 0;
			count += days.HasFlag(SukkotLodgingDays.Oct_05_Mon) ? 1 : 0;
			count += days.HasFlag(SukkotLodgingDays.Oct_06_Tue) ? 1 : 0;
			count += days.HasFlag(SukkotLodgingDays.Oct_07_Wed) ? 1 : 0;
			count += days.HasFlag(SukkotLodgingDays.Oct_08_Thu) ? 1 : 0;
			//count += days.HasFlag(SukkotLodgingDays.Oct_09_Fri) ? 1 : 0;
			//count += days.HasFlag(SukkotLodgingDays.Oct_10_Sat) ? 1 : 0;
			//count += days.HasFlag(SukkotLodgingDays.Oct_11_Sun) ? 1 : 0;
			return count;
		}
		#endregion
	}
}
