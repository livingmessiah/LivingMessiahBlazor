using Microsoft.AspNetCore.Components;
using LivingMessiah.Web.Infrastructure;

namespace LivingMessiah.Web.Shared
{
	public enum OnegCleanupEnum
	{
		Children = 1,       // 1st Shabbat of the Month - Youth and Young Adults
		YoungLadies = 2,    // 2nd Shabbat of the Month - Ladies 25 – 50
		Men = 3,            // 3rd Shabbat of the Month - Men
		Ladies = 4,         // 4th Shabbat of the Month - Ladies 50 and Wiser
		Everyone = 5        // 5th Shabbat of the Month – Everyone	 
	}

	public enum OnegThemeEnum
	{
		Family = 1,       // 1st Shabbat of the Month - Youth and Young Adults
		Mexican = 2,    // 2nd 
		Italian = 3,            // 3rd 
		CrockPot = 4,         // 4th  Casseroles
		ColdCuts = 5        // 5th Cheese, Relish Dishes
	}


	public static class Oneg
	{
		private static OnegCleanupEnum _getNextWeekCleanup()
		{
			int i = DateUtil.GetNextShabbatWeek();
			return i switch
			{
				1 => OnegCleanupEnum.Children,
				2 => OnegCleanupEnum.YoungLadies,
				3 => OnegCleanupEnum.Men,
				4 => OnegCleanupEnum.Ladies,
				_ => OnegCleanupEnum.Everyone,
			};
		}

		public static string FontAwesomeCleanup => (_getNextWeekCleanup()) switch
		{
			OnegCleanupEnum.Men => "text-primary fas fa-male fa-2x",
			OnegCleanupEnum.Ladies => "text-danger fas fa-female fa-2x",
			OnegCleanupEnum.Children => "text-success fas fa-child fa-2x",
			OnegCleanupEnum.YoungLadies => "text-danger fas fa-female fa-2x",
			OnegCleanupEnum.Everyone => "text-default fas fa-users fa-2x",
			_ => "text-default fas fa-users fa-2x",
		};

		public static string MessageCleanup
		{
			get
			{
				return (_getNextWeekCleanup()) switch
				{
					OnegCleanupEnum.Men => "Men",
					OnegCleanupEnum.Ladies => "Ladies",
					OnegCleanupEnum.Children => "Youth and Young Adults",
					OnegCleanupEnum.YoungLadies => "Ladies 25 to 50",
					OnegCleanupEnum.Everyone => "Everyone",
					_ => "Everyone",
				};
			}
		}

		private static OnegThemeEnum _GetNextWeekTheme()
		{
			int i = DateUtil.GetNextShabbatWeek();
			return i switch
			{
				1 => OnegThemeEnum.Family,
				2 => OnegThemeEnum.Mexican,
				3 => OnegThemeEnum.Italian,
				4 => OnegThemeEnum.CrockPot,
				_ => OnegThemeEnum.ColdCuts,
			};
		}

		public static string MessageTheme
		{
			get
			{
				return (_GetNextWeekTheme()) switch
				{
					OnegThemeEnum.Family => "Family Favorite Foods",
					OnegThemeEnum.Mexican => "Mexican Foods",
					OnegThemeEnum.Italian => "Italian Foods",
					OnegThemeEnum.CrockPot => "Crock Pots and Casseroles",
					OnegThemeEnum.ColdCuts => "Cold Cuts, Cheese and Relish Dishes",
					_ => "Family Favorite Foods",
				};
			}
		}

	}

	public class OnegComponentBase : ComponentBase
	{
		[Parameter]
		public bool OnegCanceled { get; set; }

		protected override void OnInitialized()
		{
			
		}
	}
}
