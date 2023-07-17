using FluentValidation;

namespace LivingMessiah.Web.Pages.Sukkot.HouseRulesAgreement;

public class FormVMValidator : AbstractValidator<FormVM>
{
	public FormVMValidator()
	{

		RuleFor(p => p.EMail)
			.NotEmpty().WithMessage("You must enter an email")
			.EmailAddress();
	}
}
