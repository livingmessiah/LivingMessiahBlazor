using System;

namespace LivingMessiah.Domain;

public class Contact
{
		public bool Selected { get; set; }
		public int ZeroBasedRowCnt { get; set; }
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string EMail { get; set; }
		public string Phone { get; set; }
		public DateTime SukkotInviteDate { get; set; }
		public bool NotInvited { get; set; }
		public bool SendWeeklyNewsletter { get; set; }
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

		public string SukkotInviteDate2
		{
				get
				{
						return SukkotInviteDate.ToString("yyyy-MM-dd");
				}
		}


}
