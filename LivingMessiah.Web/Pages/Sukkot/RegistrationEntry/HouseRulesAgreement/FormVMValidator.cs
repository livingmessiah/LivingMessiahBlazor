using FluentValidation;

namespace LivingMessiah.Web.Pages.Sukkot.RegistrationEntry.HouseRulesAgreement;

public class FormVMValidator : AbstractValidator<FormVM>
{
	public FormVMValidator()
	{
		RuleFor(p => p.EMail).EmailAddress();
	}
}
