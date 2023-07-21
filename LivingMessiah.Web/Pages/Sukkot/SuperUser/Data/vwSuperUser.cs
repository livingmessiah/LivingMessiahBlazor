﻿using LivingMessiah.Web.Pages.Sukkot.Constants;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;
using System;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.Data;

public class vwSuperUser
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
			return $"{RegistrationSteps.Enums.Status.FromValue(StatusId).StepNumber}. {RegistrationSteps.Enums.Status.FromValue(StatusId).Text}";
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
					return $"+{String.Format("{0:C0}", TotalDonation - RegistrationMeta.RegistrationFee)}";
				}
				else
				{
					return $"-{String.Format("{0:C0}", RegistrationMeta.RegistrationFee-TotalDonation)}";
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


	public string? Phone { get; set; }
	public string? Notes { get; set; }
	public int IdHra { get; set; }

}
