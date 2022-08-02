using System;
using FluentValidation;
using LivingMessiah.Web.Pages.Sukkot.RegistrationSteps.Enums;

namespace LivingMessiah.Web.Pages.Sukkot.Components;

public class RegistrationVM
{
	public int Id { get; set; }
	public string FamilyName { get; set; }
	public string FirstName { get; set; }
	public string SpouseName { get; set; }
	public string OtherNames { get; set; }
	public string EMail { get; set; }
	public string Phone { get; set; }
	public int Adults { get; set; }
	public int ChildBig { get; set; }
	public int ChildSmall { get; set; }
	
	public int StatusId { get; set; }
	public Status Status { get; set; }

	/*

	public string StatusName
	{
		get
		{
			return Status.FromValue(StatusId).Name;
		}
	}
	
	Status = Status.FromValue(poco.StatusId),
	StatusId = registration.Status.Value,
	
	 */

	public int AttendanceBitwise { get; set; }
	public DateTime[] AttendanceDateList { get; set; }
	public string Notes { get; set; }
	public string Avatar { get; set; }
	public Decimal LmmDonation { get; set; }

}


public class RegistrationVMValidator : AbstractValidator<RegistrationVM>
{
	public RegistrationVMValidator()
	{

		RuleFor(p => p.FirstName)
					.NotEmpty().WithMessage("You must enter your first name")
					.MaximumLength(75).WithMessage("First name cannot be longer than 75 characters");

		RuleFor(p => p.FamilyName)
					.NotEmpty().WithMessage("You must enter your last name")
					.MaximumLength(75).WithMessage("Last name cannot be longer than 75 characters");

		RuleFor(p => p.SpouseName)
				.MaximumLength(75).WithMessage("Spouse name cannot be longer than 75 characters");

		RuleFor(p => p.OtherNames)
				.MaximumLength(255).WithMessage("OtherNames name cannot be longer than 255 characters");

		RuleFor(p => p.Phone)
				.MaximumLength(30).WithMessage("Phone number cannot be longer than 30 characters");

		RuleFor(p => p.Adults)
				.NotNull().WithMessage("You must enter the number of adults")
				.GreaterThanOrEqualTo(0).WithMessage("Number of adults must be greater than 0")
				.LessThan(20).WithMessage("Number of adults cannot be greater than 20");

		// They can't enter an email, so why do I have this here?
		//RuleFor(p => p.EMail).EmailAddress().NotEmpty().WithMessage("You must enter a email address");

		//RuleFor(p => p.EMail)
		//		.NotEmpty().WithMessage("You must enter a email address")
		//		.EmailAddress().WithMessage("You must provide a valid email address");
	}
	/*
		//				.MustAsync(async (email, _) => await IsUniqueAsync(email))

		.WithMessage("Email address must be unique").When(p => !string.IsNullOrEmpty(p.EMail));


         1                   2
123456789012345678901234567890
1+ (555) 555-1212 ext. 1234
RuleFor(p => p.Phone)


	RuleFor(p => p.Address).SetValidator(new AddressValidator());

	[Required][Key]	public int Id { get; set; }

	.NotEmpty(): Checks if a property value is null, or is the default value for the type. 
	.NotNull(): Checks if a property value is null
	
		private static async Task<bool> IsUniqueAsync(string email)
		{
			await Task.Delay(300);
			return email.ToLower() != "mail@my.com";
		}

RuleSet("Names", () =>		});		
	{
	*/
}
