using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SukkotApi.Domain
{
	// ToDo, Task 512: Merge AgeEnum with MealAges I don't need both 
	public enum AgeEnum
	{
		Adult = 1,
		ChildBig = 2,
		ChildSmall = 3
	}

	public enum MealTypeEnum
	{
		Regular = 1,    // R
		Vegetarian = 2  // V
	}


	public enum MealTimeEnum
	{
		Brunch = 1,
		Dinner = 2
	}

	public class MealTime
	{
		public static List<MealTime> All { get; } = new List<MealTime>();
		public static MealTime Bru { get; } = new MealTime(MealTimeEnum.Brunch, 9, 12);  // "09:00", "11:59"  9:30-11 
		public static MealTime Din { get; } = new MealTime(MealTimeEnum.Dinner, 15, 17);  // "03:00", "5:00" 3:30-4:30

		public MealTimeEnum MealTimeEnum { get; private set; }

		public int TimeBegin { get; private set; }  //public string TimeBegin { get; private set; }
		public int TimeEnd { get; private set; }   // public string TimeEnd { get; private set; }

		private MealTime(MealTimeEnum mealTimesEnum, int timeBegin, int timeEnd)  //, string timeBegin, string timeEnd
		{
			MealTimeEnum = mealTimesEnum;
			TimeBegin = timeBegin;
			TimeEnd = timeEnd;
			All.Add(this);
		}

		public static MealTime FromEnum(MealTimeEnum enumValue)
		{
			return All.SingleOrDefault(r => r.MealTimeEnum == enumValue);
		}

		public static MealTime FromHour(int hour)
		{
			return All.SingleOrDefault(r => r.TimeBegin >= hour & r.TimeEnd <= hour);
		}

		public override string ToString()
		{
			if (this.MealTimeEnum == MealTimeEnum.Brunch)
			{
				return "B";
			}
			else
			{
				return "D";
			}
		}

	}

	public enum MealEnum
	{
		AdultLunch = 2,
		AdultDinner = 3,
		ChildBigLunch = 5,
		ChildBigDinner = 6,
		ChildSmallLunch = 8,
		ChildSmallDinner = 9,
		AdultLunchVeg = 10,
		AdultDinnerVeg = 11,
		ChildBigLunchVeg = 13,
		ChildBigDinnerVeg = 14,
		ChildSmallLunchVeg = 16,
		ChildSmallDinnerVeg = 17
	}

	public enum MealDateEnum
	{
		Day01 = 1,
		Day02 = 2,
		Day03 = 3,
		Day04 = 4,
		Day05 = 5,
		Day06 = 6,
		Day07 = 7,
		Day08 = 8
		//		Day09 = 9,
		//		Day10 = 10
	}

	//ToDo Task 574 Determine if table MealDate (and MealDateEnum) ...can be eliminated
	//ToDo Instead, should I port this to MealDateTime and cordinate that with MealTicketEnum
	public class MealDate
	{
		public static List<MealDate> All { get; } = new List<MealDate>();
		public static MealDate Day01 { get; } = new MealDate(MealDateEnum.Day01, "2019-10-13", "Sun, Oct 13<sup>nd</sup>", "Sukkot.vwMealsDay01");
		public static MealDate Day02 { get; } = new MealDate(MealDateEnum.Day02, "2019-10-14", "Mon, Oct 14<sup>rd</sup>", "Sukkot.vwMealsDay02");
		public static MealDate Day03 { get; } = new MealDate(MealDateEnum.Day03, "2019-10-15", "Tue, Oct 15<sup>th</sup>", "Sukkot.vwMealsDay03");
		public static MealDate Day04 { get; } = new MealDate(MealDateEnum.Day04, "2019-10-16", "Wed, Oct 16<sup>th</sup>", "Sukkot.vwMealsDay04");
		public static MealDate Day05 { get; } = new MealDate(MealDateEnum.Day05, "2019-10-17", "Thu, Oct 17<sup>th</sup>", "Sukkot.vwMealsDay05");
		public static MealDate Day06 { get; } = new MealDate(MealDateEnum.Day06, "2019-10-18", "Fri, Oct 18<sup>th</sup>", "Sukkot.vwMealsDay06");
		public static MealDate Day07 { get; } = new MealDate(MealDateEnum.Day07, "2019-10-19", "Sat, Oct 19<sup>th</sup> &dagger;", "Sukkot.vwMealsDay07");
		public static MealDate Day08 { get; } = new MealDate(MealDateEnum.Day08, "2019-10-20", "Sun, Oct 20<sup>th</sup> &Dagger;", "Sukkot.vwMealsDay08");
		//public static MealDate Day09 { get; } = new MealDate(MealDateEnum.Day09, "2019-10-30", "Sun, Oct 21<sup>th</sup>", "Sukkot.vwMealsDay09");
		//public static MealDate Day10 { get; } = new MealDate(MealDateEnum.Day10, "2019-10-01", "Mon, Oct 22<sup>st</sup>", "Sukkot.vwMealsDay10");

		public int Id { get; private set; }
		public MealDateEnum MealDateEnum { get; private set; }
		public string Name { get; private set; }
		public DateTime MealDateTime { get; private set; }
		public string DateHtml { get; private set; }
		public string SqlView { get; set; }

		private MealDate(MealDateEnum mealDatesEnum, string dateString, string dateHtml, string sqlView)
		{
			Id = (int)mealDatesEnum;
			MealDateEnum = mealDatesEnum;
			Name = dateString;
			MealDateTime = DateTime.Parse(dateString);
			DateHtml = dateHtml;
			SqlView = sqlView;
			All.Add(this);
		}

		public static MealDate FromString(string formatString)
		{
			return All.SingleOrDefault(r => String.Equals(r.Name, formatString, StringComparison.OrdinalIgnoreCase));
		}

		public static MealDate FromEnum(MealDateEnum enumValue)
		{
			return All.SingleOrDefault(r => r.MealDateEnum == enumValue);
		}

		public static Dictionary<string, string> DictionaryList()
		{
			Dictionary<string, string> d = new Dictionary<string, string>();
			foreach (MealDate f in All)
			{
				d.Add(f.MealDateEnum.ToString(), f.Name);
			}
			return d;
		}

		/*
		public static IEnumerable<Format> ListHebrewFormat()
		{
			return new[] { InterlinearWlc, InterlinearKjv, Wlc };
		}
		*/

	}


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

	//ToDo: I need a MealTicketFilterEnum
	public enum MealTicketEnum
	{
		UpCommingMeal = 0,  // 
		Day01Brunch = 1,  // Sun	10/13 9am Brunch
		Day01Dinner = 2,  // Sun	10/13 5pm Dinner
		Day02Brunch = 3,  // Mon	10/14 9am Brunch
		Day02Dinner = 4,  // Mon	10/14 5pm Dinner
		Day03Brunch = 5,  // Tue	10/15 9am Brunch
		Day03Dinner = 6,  // Tue	10/15 5pm Dinner
		Day04Brunch = 7,  // Wed	10/16 9am Brunch
		Day04Dinner = 8,  // Wed	10/16 5pm Dinner
		Day05Brunch = 9,  // Thu	10/17 9am Brunch
		Day05Dinner = 10, // Thu	10/17 5pm Dinner
		Day06Brunch = 11, // Fri	10/18 9am Brunch
		Day06Dinner = 12, // Fri	10/18 5pm Dinner
		Day07Brunch = 13, // Sat	10/19 9am Brunch
		Day07Dinner = 14, // Sat	10/19 5pm Dinner
		Day08Brunch = 15, // Sun	10/20 9am Brunch
		Day08Dinner = 16  // Sun	10/20 9am Dinner HasTicket = false, so should this be in here?
	}

	public class MealTicket
	{
		public static List<MealTicket> All { get; } = new List<MealTicket>();


		// "2019-10-13 16:00:00" = 16:00 UTC = 09:00 (9 am AZ)
		// "2019-10-13 19:01:00" = 12:01 UTC = 12:01 Noon + 1 AZ
		// "2019-10-13 22:00:00" = 22:00 UTC = 15:00 (3 pm AZ                                                                           
		public static MealTicket Day_01_Brunch { get; } = new MealTicket(MealTicketEnum.Day01Brunch, MealDateEnum.Day01, "2019-10-13 16:00:00", "Sun Oct 13 Brunch", true);
		public static MealTicket Day_01_Dinner { get; } = new MealTicket(MealTicketEnum.Day01Dinner, MealDateEnum.Day01, "2019-10-13 22:00:00", "Sun Oct 13 Dinner", true);
		public static MealTicket Day_02_Brunch { get; } = new MealTicket(MealTicketEnum.Day02Brunch, MealDateEnum.Day02, "2019-10-14 16:00:00", "Mon Oct 14 Brunch", true);
		public static MealTicket Day_02_Dinner { get; } = new MealTicket(MealTicketEnum.Day02Dinner, MealDateEnum.Day02, "2019-10-14 22:00:00", "Mon Oct 14 Dinner", true);
		public static MealTicket Day_03_Brunch { get; } = new MealTicket(MealTicketEnum.Day03Brunch, MealDateEnum.Day03, "2019-10-15 16:00:00", "Tue Oct 15 Brunch", true);
		public static MealTicket Day_03_Dinner { get; } = new MealTicket(MealTicketEnum.Day03Dinner, MealDateEnum.Day03, "2019-10-15 22:00:00", "Tue Oct 15 Dinner", true);
		public static MealTicket Day_04_Brunch { get; } = new MealTicket(MealTicketEnum.Day04Brunch, MealDateEnum.Day04, "2019-10-16 16:00:00", "Wed Oct 16 Brunch", true);
		public static MealTicket Day_04_Dinner { get; } = new MealTicket(MealTicketEnum.Day04Dinner, MealDateEnum.Day04, "2019-10-16 22:00:00", "Wed Oct 16 Dinner", true);
		public static MealTicket Day_05_Brunch { get; } = new MealTicket(MealTicketEnum.Day05Brunch, MealDateEnum.Day05, "2019-10-17 16:00:00", "Thu Oct 17 Brunch", true);
		public static MealTicket Day_05_Dinner { get; } = new MealTicket(MealTicketEnum.Day05Dinner, MealDateEnum.Day05, "2019-10-17 22:00:00", "Thu Oct 17 Dinner", true);
		public static MealTicket Day_06_Brunch { get; } = new MealTicket(MealTicketEnum.Day06Brunch, MealDateEnum.Day06, "2019-10-18 16:00:00", "Fri Oct 18 Brunch", true);
		public static MealTicket Day_06_Dinner { get; } = new MealTicket(MealTicketEnum.Day06Dinner, MealDateEnum.Day06, "2019-10-18 22:00:00", "Fri Oct 18 Dinner", true);
		public static MealTicket Day_07_Brunch { get; } = new MealTicket(MealTicketEnum.Day07Brunch, MealDateEnum.Day07, "2019-10-19 16:00:00", "Sat Oct 19 Brunch", true);
		public static MealTicket Day_07_Dinner { get; } = new MealTicket(MealTicketEnum.Day07Dinner, MealDateEnum.Day07, "2019-10-19 22:00:00", "Sat Oct 19 Dinner", true);
		public static MealTicket Day_08_Brunch { get; } = new MealTicket(MealTicketEnum.Day08Brunch, MealDateEnum.Day08, "2019-10-20 16:00:00", "Sun Oct 20 Brunch", true);
		public static MealTicket Day_08_Dinner { get; } = new MealTicket(MealTicketEnum.Day08Dinner, MealDateEnum.Day08, "2019-10-20 22:00:00", "Sun Oct 20 Dinner", false);

		public MealTicketEnum MealTicketEnum { get; private set; }
		public MealDateEnum MealDateEnum { get; private set; }
		public MealTimeEnum MealTimeEnum { get; private set; }
		public string Name { get; private set; }
		public DateTime MealTicketDateTime { get; private set; }
		public string DateHtml { get; private set; }
		public int Id { get; private set; }   // = Sukkot.MealDateTime!Id
		public bool HasTicket { get; private set; }   // = Sukkot.MealDateTime!HasTicket

		private MealTicket(MealTicketEnum mealTicketEnum, MealDateEnum mealDateEnum, string dateString, string name, bool hasTicket)  //, string sqlView
		{
			MealTicketEnum = mealTicketEnum;
			Id = (int)mealTicketEnum;
			MealDateEnum = mealDateEnum;
			HasTicket = hasTicket;

			//Name = dateString;
			Name = name;

			MealTicketDateTime = DateTime.Parse(dateString);
			DateHtml = name;
			//SqlView = sqlView;

			if (MealTicketEnum == MealTicketEnum.Day01Brunch ||
					MealTicketEnum == MealTicketEnum.Day02Brunch ||
					MealTicketEnum == MealTicketEnum.Day03Brunch ||
					MealTicketEnum == MealTicketEnum.Day04Brunch ||
					MealTicketEnum == MealTicketEnum.Day05Brunch ||
					MealTicketEnum == MealTicketEnum.Day06Brunch ||
					MealTicketEnum == MealTicketEnum.Day07Brunch ||
					MealTicketEnum == MealTicketEnum.Day08Brunch)
			{
				MealTimeEnum = MealTimeEnum.Brunch;
			}
			else
			{
				MealTimeEnum = MealTimeEnum.Dinner;
			}


			All.Add(this);
		}

		public static MealTicket FromString(string formatString)
		{
			return All.Single(r => String.Equals(r.Name, formatString, StringComparison.OrdinalIgnoreCase));
		}

		public static MealTicket FromEnum(MealTicketEnum enumValue)
		{
			return All.SingleOrDefault(r => r.MealTicketEnum == enumValue);
		}

		public static MealTicket FromInt(int intValue)
		{
			return All.SingleOrDefault(r => r.Id == intValue);
		}

	}
}
