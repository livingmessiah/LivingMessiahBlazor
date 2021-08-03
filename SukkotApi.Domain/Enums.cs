using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SukkotApi.Domain.Enums
{
	public enum CampType
	{
		[Display(Name = "OffSite [e.g. Hotel]")]
		OffSite = 0,

		Tent = 1,

		[Display(Name = "RV or Camp Trailer")]
		RvOrCampTrailer = 2,

		[Display(Name = "Indoor Facility")]
		CabinOrBunkhouse = 3,

		[Display(Name = "RV Dry Camp Only, NO HOOKUPs")]
		RvDryCampOnly = 4
	}


	public enum LocationSimpleEnum
	{
		GreenHouseTrolleyHobbyFarm,
		WildernessRanch,
		WindmillRanch
	}

	//[Display(Name = "Green House Trolley Hobby Farm (Near Sierra Vista")]
	//[Display(Name = "Wilderness Ranch (Near Show Low)")]
	//[Display(Name = "Windmill Ranch (Near Bisbee)")]
	public enum LocationEnum
	{
		GreenHouseTrolleyHobbyFarm = 1,
		WildernessRanch = 2,
		WindmillRanch = 3
	}

	public class Location
	{
		public static List<Location> All { get; } = new List<Location>();
		public static Location GreenHouseTrolleyHobbyFarm { get; } = new Location(LocationEnum.GreenHouseTrolleyHobbyFarm, "Green House Trolley Hobby Farm (Near Sierra Vista)");  
		public static Location WildernessRanch { get; } = new Location(LocationEnum.WildernessRanch, "Wilderness Ranch (Near Show Low)");
		public static Location WindmillRanch { get; } = new Location(LocationEnum.WildernessRanch, "Windmill Ranch (Near Bisbee)");

		public LocationEnum LocationEnum { get; private set; }
		public int Id { get; private set; }
		public string Name { get; private set; }

		private Location(LocationEnum locationsEnum, string name)
		{
			LocationEnum = locationsEnum;
			Id = (int)locationsEnum;
			Name = name;
			All.Add(this);
		}

		public static Location FromEnum(LocationEnum enumValue)
		{
			return All.SingleOrDefault(r => r.LocationEnum == enumValue);
		}

		public static Location FromString(string formatString)
		{
			return All.SingleOrDefault(r => String.Equals(r.Name, formatString, StringComparison.OrdinalIgnoreCase));
		}
	}


	public enum StatusEnum
	{
		[Display(Name = "eMail Not Confirmed (Step 1)")]
		EmailNotConfirmed = 0,

		[Display(Name = "eMail Confirmation (Step 2)")]
		EmailConfirmation = 1,

		[Display(Name = "Registration Form Completed (Step 3)")]
		RegistrationFormCompleted = 2,

		[Display(Name = "Meals Form Completed (Step 4)")]
		MealsFormCompleted = 3,

		[Display(Name = "Partially Paid (Step 5)")]
		PartiallyPaid = 4,

		[Display(Name = "Fully Paid (Step 6)")]
		FullyPaid = 5,

		[Display(Name = "Canceled")]
		Canceled = 6

	}

	/*
	SELECT * FROM Sukkot.Status

	Id	Code		Descr
	--	-------	-----------------------------
	0		0. NoEC	Email Not Confirmed
	1		1. EC		Email Confirmation
	2		2. RFC	Registration Form Completed
	3		3. MFC	Meal Form Completed
	4		4. PP		Partially Paid
	5		5. FP		Fully Paid
	6		6. Can	Canceled
	*/
	public enum KitchenWorkType
	{
		Cook1 = 1,
		Cook2 = 2,
		Prep1 = 3,
		Prep2 = 4,
		Cleaner1 = 5,
		Cleaner2 = 6
	}

	public enum PreferredLodging
	{
		[Display(Name = "No Preference")]
		NoPreference = 0,
		[Display(Name = "Full RV Hookup Near Playground")]
		FullRvHookupNearPlayground = 1,
		[Display(Name = "Big House")]
		BigHouse = 2,
		[Display(Name = "Bunk House")]
		BunkHouse = 3,
		[Display(Name = "Stage Coach Inn [Womans Cabin]")]
		StageCoachInnWomansCabin = 4,
		[Display(Name = "Waystop Inn or Jail")]
		WaystopInnOrJail = 5,
		[Display(Name = "Hitchin Post")]
		HitchinPost = 6
	}

	// ToDo, Task 512: Merge AgeEnum with MealAges I don't need both 
	public enum MealAges
	{
		Adults = 1,
		ChildBig = 2,
		ChildSmall = 3
	}

	public enum RegistrationSort
	{
		Id = 1,
		FamilyName = 2
	}

	[Flags]
	public enum SukkotKitchenWorkCook1
	{
		Oct_13_Brunch = 1, // 1 
		Oct_14_Brunch = 2, // 2 
		Oct_15_Brunch = 4, // 3 
		Oct_16_Brunch = 8, // 4 
		Oct_17_Brunch = 16, // 5 
		Oct_18_Brunch = 32, // 6 
		Oct_19_Brunch = 64, // 7 
		Oct_20_Brunch = 128, // 8 
		Oct_13_Dinner = 256, // 9 
		Oct_14_Dinner = 512, // 10
		Oct_15_Dinner = 1024, // 11
		Oct_16_Dinner = 2048, // 12
		Oct_17_Dinner = 4096, // 13
		Oct_18_Dinner = 8192, // 14
		Oct_19_Dinner = 16384, // 15
		Oct_20_Dinner = 32768, // 16
	}

	/*
	SELECT N, 'Oct_' + CAST(N+12 AS CHAR(2)) + '_Brunch = ' + TRIM(CAST(POWER(2, n-1) AS CHAR(12))) + ', // ' + CAST(N AS CHAR(2))
	FROM Number 
	WHERE N <= 8

	SELECT N, 'Oct_' + CAST(N+4 AS CHAR(2)) + '_Dinner = ' + TRIM(CAST(POWER(2, n-1) AS CHAR(12))) + ', // ' + CAST(N AS CHAR(2))
	FROM Number 
	WHERE N BETWEEN 9 AND 16
	*/

}
