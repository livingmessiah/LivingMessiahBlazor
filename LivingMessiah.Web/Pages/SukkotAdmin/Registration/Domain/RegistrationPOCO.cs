﻿using System;
using LivingMessiah.Web.Pages.SukkotAdmin.Enums;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain;

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

		/*
		public BaseCampTypeSmartEnum  CampTypeSmartEnum { get; set; }
		public BaseStatusSmartEnum StatusSmartEnum { get; set; }
		*/


		/*
		Why are these enums? why not just ints
		public SukkotApi.Domain.Enums.CampType CampTypeEnum { get; set; }
		public SukkotApi.Domain.Enums.StatusEnum StatusEnum { get; set; }
		*/
		public int CampId { get; set; }
		public int StatusId { get; set; }

		public int AttendanceBitwise { get; set; }
		public string AttendanceDatesCSV { get; set; }

		public string Notes { get; set; }
		public string Avitar { get; set; }
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
