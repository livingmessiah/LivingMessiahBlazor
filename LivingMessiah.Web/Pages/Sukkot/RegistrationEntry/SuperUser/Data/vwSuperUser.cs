namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.SuperUser.Data;

public class vwSuperUser
{
	public int Id { get; set; }
	public string? EMail { get; set; }
	public string? FullName { get; set; }
	public int StatusId { get; set; } 
	public string StatusName
	{
		get
		{
			return RegistrationSteps.Enums.Status.FromValue(StatusId).Name;
		}
	}


	/*
	public string FullNameOrEMail
	{
		get
		{
			if (string.IsNullOrEmpty(FullName))
			{
				return EMail!;
			}
			else
			{
				return FullName!;
			}
		}
	}

	public string EMailIfNotEmpty
	{
		get
		{
			if (!string.IsNullOrEmpty(EMail))
			{
				return "; Email: " + EMail!;
			}
			else
			{
				return string.Empty;
			}
		}
	}
	*/

	public string? Phone { get; set; }
	public string? Notes { get; set; }
}
