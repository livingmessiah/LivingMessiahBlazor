﻿using LivingMessiah.Web.Infrastructure;
using LivingMessiah.Web.Features.Sukkot.Constants;
using LivingMessiah.Web.Features.Sukkot.RegistrationSteps.Enums;
using System;

namespace LivingMessiah.Web.Features.Sukkot.ManageRegistration.MasterDetail;

public class ManageRegistrationQuery
{
	public int Id { get; set; }
	public string? EMail { get; set; }
	public string? FullName { get; set; }

	public string FullNameOrNA
	{
		get
		{
			return StatusId == Status.StartRegistration ? "N/A" : FullName!;
		}
	}


	public string FullNameOrNAColor
	{
		get
		{
			//"bg-secondary text-center text-white"
			return StatusId == Status.StartRegistration ? "bg-secondary text-center text-white" : "";
		}
	}

	public int StatusId { get; set; }

	public string StatusName
	{
		get
		{
			return $"{Status.FromValue(StatusId).StepNumber}. {Status.FromValue(StatusId).Text}";
		}
	}

	public bool DidNotAttend { get; set; }
	public string NoShow
	{
		get
		{
			return DidNotAttend ? "✓" : ""; 
		}
	}


	public decimal TotalDonation { get; set; }

	public string TotalDonationNoCents
	{
		get
		{
			if (StatusId == Status.StartRegistration) return "N/A";

			if (TotalDonation == RegistrationMeta.RegistrationFee)
			{
				return "✓";
			}
			else
			{
				if (TotalDonation > RegistrationMeta.RegistrationFee)
				{
					return $"+{string.Format("{0:C0}", TotalDonation - RegistrationMeta.RegistrationFee)}";
				}
				else
				{
					return $"-{string.Format("{0:C0}", RegistrationMeta.RegistrationFee - TotalDonation)}";
				}
			}

		}
	}

	public string TotalDonationClass
	{
		get
		{
			if (StatusId == Status.StartRegistration) return "bg-secondary text-center text-white";

			if (TotalDonation == RegistrationMeta.RegistrationFee)
			{
				return "bg-success text-center text-white";
			}
			else
			{
				if (TotalDonation > RegistrationMeta.RegistrationFee)
				{
					return "bg-primary text-end text-white";
				}
				else
				{
					return "bg-danger text-end text-white";
				}
			}

		}
	}

	public string TotalDonationBadgeCSS
	{
		get
		{
			if (StatusId == Status.StartRegistration) return "badge bg-secondary text-white";

			if (TotalDonation == RegistrationMeta.RegistrationFee)
			{
				return "badge bg-success text-white";
			}
			else
			{
				if (TotalDonation > RegistrationMeta.RegistrationFee)
				{
					return "badge bg-primary text-white";
				}
				else
				{
					return "badge bg-danger text-white";
				}
			}

		}
	}

	public int DonationRowCount { get; set; }


	public string? Phone { get; set; }
	public string? Notes { get; set; } 
	public string? AdminNotes { get; set; }


	public string AdminNotesShort
	{
		get
		{
			return String.IsNullOrEmpty(AdminNotes) == true ? "" : AdminNotes!.Truncate(25); 
		}
	}

	public int IdHra { get; set; }

}
