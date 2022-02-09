using System.Collections.Generic;
using System.Linq;

namespace LivingMessiah.Web.Pages.KeyDates.Enums;

public enum EventTypeEnum
{
		None = 0,
		KeyDate = 1,
		MensCoffeeClub = 2, // "fas fa-coffee"
		LadiesEveningFellowship = 3,
		CommunityDinner = 4, //"fas fa-utensils"
		ErevShabbat = 5, //"fas fa-utensils"
		Movie = 6, // "fas fa-film"
		GuestSpeaker = 7,  // "fas fa-microphone"
		Other = 8
}

public class EventType
{
		public static List<EventType> All { get; } = new List<EventType>();

		public static EventType MensCoffeeClub { get; } = new EventType(
			EventTypeEnum.MensCoffeeClub, (int)EventTypeEnum.MensCoffeeClub, "Mens Coffee Club", "fas fa-coffee");  //ImgUrl = Blobs.UrlOther("mens-breakfast.jpg")

		public static EventType LadiesEveningFellowship { get; } = new EventType(
			EventTypeEnum.LadiesEveningFellowship, (int)EventTypeEnum.LadiesEveningFellowship, "Ladies Evening Fellowship", "fas fa-female");

		public static EventType CommunityDinner { get; } = new EventType(
			EventTypeEnum.CommunityDinner, (int)EventTypeEnum.CommunityDinner, "Community Dinner", "fas fa-utensils");

		public static EventType ErevShabbat { get; } = new EventType(
			EventTypeEnum.ErevShabbat, (int)EventTypeEnum.ErevShabbat, "Erev Shabbat", "fas fa-utensils"); // , ImgUrl = Blobs.UrlRoot("lmm-calendar-800-600.jpeg")

		public static EventType Movie { get; } = new EventType(
			EventTypeEnum.Movie, (int)EventTypeEnum.Movie, "Movie", "fas fa-film");

		public static EventType GuestSpeaker { get; } = new EventType(
			EventTypeEnum.GuestSpeaker, (int)EventTypeEnum.GuestSpeaker, "Guest Speaker", "fas fa-microphone");

		public static EventType Other { get; } = new EventType(
			EventTypeEnum.Other, (int)EventTypeEnum.Other, "Other", "fas fa-question");

		public EventTypeEnum EventTypeEnum { get; private set; }
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Icon { get; set; }
		public string ImageHeaderUrl { get; private set; } = "https://via.placeholder.com/175";
		public string ImageUrl { get; private set; } = "https://via.placeholder.com/175";

		private EventType(EventTypeEnum eventTypeEnum, int id, string name, string icon)
		{
				EventTypeEnum = eventTypeEnum;
				Id = id;
				Name = name;
				Icon = icon;
				All.Add(this);
		}

		public static EventType FromEnum(EventTypeEnum enumValue)
		{
				return All.SingleOrDefault(r => r.EventTypeEnum == enumValue);
		}

		public static EventType FromInt(int intValue)
		{
				return All.SingleOrDefault(r => r.Id == intValue);
		}


} // class Season



