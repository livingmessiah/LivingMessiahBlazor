﻿using LivingMessiah.Web.Pages.SukkotAdmin.Enums;
using System;

namespace LivingMessiah.Web.Pages.SukkotAdmin.Registration.Domain
{
	public class Registration
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string FamilyName { get; set; }
		public string SpouseName { get; set; }
		public string OtherNames { get; set; }
		public string EMail { get; set; }
		public string Phone { get; set; }  // NULL
		public int Adults { get; set; }
		public int ChildBig { get; set; }
		public int ChildSmall { get; set; }

		public BaseStatusSmartEnum BaseStatusSmartEnum { get; set; }
		public BaseCampTypeSmartEnum BaseCampTypeSmartEnum { get; set; }

		public DateTime[] AttendanceDateList { get; set; }  // NOT NULL
		public DateTime[] LodgingDateList { get; set; }			// NOT NULL

		public string Notes { get; set; }

		public int LocationInt { get; set; }
		public string LocationName
		{
			get
			{
				return BaseLocationSmartEnum.FromValue(LocationInt).Name;
			}
		}

		//public string Avitar { get; set; }
		//public string AssignedLodging { get; set; }
		//public decimal LmmDonation { get; set; }		// NOT NULL
		//public bool WillHelpWithMeals { get; set; } NOT NULL

	}
}
