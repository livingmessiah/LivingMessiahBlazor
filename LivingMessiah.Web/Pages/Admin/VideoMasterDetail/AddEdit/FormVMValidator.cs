using FluentValidation;

namespace LivingMessiah.Web.Pages.Admin.VideoMasterDetail.AddEdit;

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
		}

	}
}

