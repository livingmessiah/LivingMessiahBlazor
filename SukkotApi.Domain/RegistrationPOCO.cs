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

		public LocationEnum LocationEnum { get; set; }

		public CampType CampTypeEnum { get; set; }
		public StatusEnum StatusEnum { get; set; }

		public int AttendanceBitwise { get; set; }
		public string AttendanceDatesCSV { get; set; }

		public int LodgingDaysBitwise { get; set; }
		public string LodgingDatesCSV { get; set; }


		public string Notes { get; set; }
		public string Avitar { get; set; }
		public string AssignedLodging { get; set; }
		public Decimal LmmDonation { get; set; }
		public bool WillHelpWithMeals { get; set; }
		#endregion

		#region HelperMethods


		public DateTime[] AttendanceDateList
		{
			get
			{
				if (!String.IsNullOrEmpty(AttendanceDatesCSV))
				{
					int length = AttendanceDatesCSV.Split(",").Length;
					DateTime[] list = new DateTime[length];
					string[] array = AttendanceDatesCSV.Split(',');
					int i = 0;
					foreach (string value in array)
					{
						list[i] = (DateTime.Parse(value));
						i += 1;
					}
					return list;
				}
				else
				{
					return null;
				}
			}
		}

		public DateTime[] LodgingDateList
		{
			get
			{
				if (!String.IsNullOrEmpty(LodgingDatesCSV))
				{
					int length = LodgingDatesCSV.Split(",").Length;
					DateTime[] list = new DateTime[length];
					string[] array = LodgingDatesCSV.Split(',');
					int i = 0;
					foreach (string value in array)
					{
						list[i] = (DateTime.Parse(value));
						i += 1;
					}
					return list;
				}
				else
				{
					return null;
				}
			}
		}



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
