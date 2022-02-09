namespace LivingMessiah.Web.Pages.Contacts.Domain;

public class ContactVM
{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string OtherNames { get; set; }
		public string EMail { get; set; }
		public string Phone { get; set; }
		public string SukkotAttendanceCSV { get; set; }
		public string Notes { get; set; }

		public string Name
		{
				get
				{
						if (!string.IsNullOrEmpty(MiddleName))
						{
								return $"{FirstName} {LastName}";
						}
						else
						{
								return $"{FirstName} {MiddleName} {LastName}";
						}
				}
		}


		/*
		public DateTime SukkotInviteDate { get; set; }
		public bool NotInvited { get; set; }
		public bool SendWeeklyNewsletter { get; set; }

		public string SukkotInviteDate2
		{
			get
			{
				return SukkotInviteDate.ToString("yyyy-MM-dd");
			}
		}

		 */

}
