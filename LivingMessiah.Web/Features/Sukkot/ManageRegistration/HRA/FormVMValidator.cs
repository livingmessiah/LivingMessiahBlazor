﻿using FluentValidation;

namespace LivingMessiah.Web.Features.Sukkot.ManageRegistration.HRA;

public class FormVMValidator : AbstractValidator<FormVM>
{
	public FormVMValidator()
	{

		RuleFor(p => p.EMail)
			.NotEmpty().WithMessage("You must enter an email")
			.EmailAddress();
	}
}
