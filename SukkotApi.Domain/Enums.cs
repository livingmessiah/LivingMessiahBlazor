using System;
using System.ComponentModel.DataAnnotations;

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

	public enum CampTypeNoMoreRvHookups
	{
		[Display(Name = "OffSite [e.g. Hotel]")]
		OffSite = 0,

		Tent = 1,

		[Display(Name = "Indoor Facility")]
		CabinOrBunkhouse = 3,

		[Display(Name = "RV Dry Camp Only, NO HOOKUPs")]
		RvDryCampOnly = 4
	}


	public enum Status
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

	#region Attendance
	/*
	No Display Attributes
		[Flags]
	public enum SukkotAttendanceDays
	{
		Oct_01_Thu = 1,
		Oct_02_Fri = 2,
		Oct_03_Sat = 4,
		Oct_04_Sun = 8,
		Oct_05_Mon = 16,
		Oct_06_Tue = 32,
		Oct_07_Wed = 64,
		Oct_08_Thu = 128,
		Oct_09_Fri = 256 //, 		Oct_10_Sat = 512
	}

	*/
	[Flags]
	public enum SukkotAttendanceDays
	{
		[Display(Name = "Oct 01, Thu")]
		Oct_01_Thu = 1,
		
		[Display(Name = "Oct 02, Fri")]
		Oct_02_Fri = 2,
		
		[Display(Name = "Oct 03, Sat")]
		Oct_03_Sat = 4,
		
		[Display(Name = "Oct 04, Sun")]
		Oct_04_Sun = 8,
		
		[Display(Name = "Oct 05, Mon")]
		Oct_05_Mon = 16,
		
		[Display(Name = "Oct 06, Tue")]
		Oct_06_Tue = 32,
		
		[Display(Name = "Oct 07, Wed")]
		Oct_07_Wed = 64,
		
		[Display(Name = "Oct 08, Thu")]
		Oct_08_Thu = 128,
		
		[Display(Name = "Oct 09, Fri")]
		Oct_09_Fri = 256 //, 		Oct_10_Sat = 512
	}

	public static class AttendanceDays
	{
		public const string TableHeadingHtml = @"
<th>Th<br />01</th>
<th>Fr<br />02</th>
<th>Sa<br />03</th>
<th>Su<br />04</th>
<th>Mo<br />05</th>
<th>Tu<br />06</th>
<th>We<br />07</th>
<th>Th<br />08</th>
<th>Fr<br />09</th>
";
	}
	//<th>Sa<br />10</th>

	#endregion

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
