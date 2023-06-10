using FluentValidation;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.HouseRulesAgreement;

public class FormVMValidator : AbstractValidator<FormVM>
{
	public FormVMValidator()
	{

		RuleFor(p => p.EMail)
			.NotEmpty().WithMessage("You must enter an email")
			.EmailAddress();
	}
}
