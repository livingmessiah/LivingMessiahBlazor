using Ardalis.SmartEnum;

namespace LivingMessiah.Web.Pages.SpecialEvents.Enums;

public abstract class SpecialEventType : SmartEnum<SpecialEventType>
{
		#region Id's
		private static class Id
		{
				internal const int MensCoffeeClub = 2;
				internal const int LadiesEveningFellowship = 3;
				internal const int CommunityDinner = 4;
				internal const int ErevShabbat = 5;
				internal const int Movie = 6;
				internal const int GuestSpeaker = 7;
				internal const int Other = 8;
		}
		#endregion

		#region  Declared Public Instances
		public static readonly SpecialEventType MensCoffeeClub = new MensCoffeeClubSE();
		public static readonly SpecialEventType LadiesEveningFellowship = new LadiesEveningFellowshipSE();
		public static readonly SpecialEventType CommunityDinner = new CommunityDinnerSE();
		public static readonly SpecialEventType ErevShabbat = new ErevShabbatSE();
		public static readonly SpecialEventType Movie = new MovieSE();
		public static readonly SpecialEventType GuestSpeaker = new GuestSpeakerSE();
		public static readonly SpecialEventType Other = new OtherSE();
		// SE=SmartEnum
		#endregion

		private SpecialEventType(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		public abstract string Descr { get; }
		#endregion


		#region Private Instantiation

		private sealed class MensCoffeeClubSE : SpecialEventType
		{
				public MensCoffeeClubSE() : base($"{nameof(Id.MensCoffeeClub)}", Id.MensCoffeeClub) { }
				public override string Descr => "Mens Coffee Club";
		}

		private sealed class LadiesEveningFellowshipSE : SpecialEventType
		{
				public LadiesEveningFellowshipSE() : base($"{nameof(Id.LadiesEveningFellowship)}", Id.LadiesEveningFellowship) { }
				public override string Descr => "Ladies Evening Fellowship";
		}

		private sealed class CommunityDinnerSE : SpecialEventType
		{
				public CommunityDinnerSE() : base($"{nameof(Id.CommunityDinner)}", Id.CommunityDinner) { }
				public override string Descr => "Community Dinner";
		}

		private sealed class ErevShabbatSE : SpecialEventType
		{
				public ErevShabbatSE() : base($"{nameof(Id.ErevShabbat)}", Id.ErevShabbat) { }
				public override string Descr => "Erev Shabbat";
		}

		private sealed class MovieSE : SpecialEventType
		{
				public MovieSE() : base($"{nameof(Id.Movie)}", Id.Movie) { }
				public override string Descr => "Movie";
		}

		private sealed class GuestSpeakerSE : SpecialEventType
		{
				public GuestSpeakerSE() : base($"{nameof(Id.GuestSpeaker)}", Id.GuestSpeaker) { }
				public override string Descr => "Guest Speaker";
		}

		private sealed class OtherSE : SpecialEventType
		{
				public OtherSE() : base($"{nameof(Id.Other)}", Id.Other) { }
				public override string Descr => "Other";
		}

		#endregion
}

