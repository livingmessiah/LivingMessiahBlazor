﻿using FluentValidation;

namespace LivingMessiah.Web.Features.Admin.Video.AddEdit;

public class FormVMValidator : AbstractValidator<FormVM>
{
	public FormVMValidator()
	{
		{
			RuleFor(p => p.WeeklyVideoTypeId)
				.NotEmpty().WithMessage("You must select a Weekly Video Type");

			RuleFor(p => p.ShabbatWeekId)
				.NotEmpty().WithMessage("You must select a Shabbat Week Id");

			RuleFor(p => p.YouTubeId)
				.MinimumLength(11).WithMessage("YouTube Id must be 11 characters")
				.MaximumLength(11).WithMessage("YouTube Id must be 11 characters");

			RuleFor(p => p.Title)
				.MaximumLength(100).WithMessage("Title cannot be longer than 100 characters");

			RuleFor(p => p.Book).NotEmpty().WithMessage("You must select a Book").When(w => w.WeeklyVideoTypeId > 2);

			RuleFor(p => p.Chapter).NotEmpty().WithMessage("You must select a Chapter").When(w => w.WeeklyVideoTypeId > 2);
		}

	}
}

