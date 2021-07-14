using System;

namespace SukkotApi.Domain
{
	public class MealTicketPunchLog
	{
		public int RegistrationId { get; private set; }
		public int MealDateTimeId { get; private set; }
		public int MealTypeId { get; private set; }
		public int AgeId { get; private set; }
		public int PunchCount { get; private set; }


		public MealTicketPunchLog(
				int registrationId, MealTicketEnum mealTicketEnum
			, MealTypeEnum mealTypeEnum, AgeEnum ageEnum, int punchCount)
		{
			MealTicket mt = MealTicket.FromEnum(mealTicketEnum);
			RegistrationId = registrationId;
			MealDateTimeId = mt.Id;
			MealTypeId = (int)mealTypeEnum;
			AgeId = (int)ageEnum;
			PunchCount = punchCount;

		}
	}
}
