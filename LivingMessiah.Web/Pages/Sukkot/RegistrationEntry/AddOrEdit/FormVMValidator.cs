using FluentValidation;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.AddOrEdit;

public class FormVMValidator : AbstractValidator<FormVM>
{
	public FormVMValidator()
	{
		{
			RuleFor(p => p.FirstName)
						.NotEmpty().WithMessage("You must enter your first name")
						.MaximumLength(50).WithMessage("First name cannot be longer than 50 characters");

			RuleFor(p => p.FamilyName)
						.NotEmpty().WithMessage("You must enter your last name")
						.MaximumLength(75).WithMessage("Last name cannot be longer than 75 characters");

			RuleFor(s => s.SpouseName).Length(1, 50).When(s => !string.IsNullOrEmpty(s.SpouseName));

			RuleFor(p => p.OtherNames)
					.MaximumLength(255).WithMessage("OtherNames name cannot be longer than 255 characters");

			RuleFor(p => p.Phone)
					.MaximumLength(15).WithMessage("Phone number cannot be longer than 15 characters");

			RuleFor(p => p.EMail).EmailAddress();

			RuleFor(p => p.Adults)
					.NotNull().WithMessage("You must enter the number of adults")
					.GreaterThanOrEqualTo(1).WithMessage("Number of adults must be greater than 1")
					.LessThan(20).WithMessage("Number of adults cannot be greater than 20");
		}

	}
}
