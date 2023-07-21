using FluentValidation;

namespace LivingMessiah.Web.Pages.Sukkot.SuperUser.Donations;

public class FormVMValidator : AbstractValidator<FormVM>
{
	public FormVMValidator()
	{
		{
			RuleFor(p => p.ReferenceId)
			.NotEmpty().WithMessage("You must enter a reference")
			.MaximumLength(100).WithMessage("reference cannot be longer than 100 characters");

			RuleFor(p => p.Amount)
					.NotNull().WithMessage("You must enter an amount")
					.GreaterThanOrEqualTo(1).WithMessage("Amount must be greater than 1")
					.LessThan(500).WithMessage("Amount cannot be greater than 500");
		}
	}
}

