using FluentValidation;

namespace LivingMessiah.Web.Features.SpecialEvents;

public class FormVMValidator : AbstractValidator<FormVM>
{
	public FormVMValidator()
	{
		RuleFor(x => x.SpecialEventTypeId).NotEmpty().WithMessage("Event type is required");
		RuleFor(x => x.EventDate).NotEmpty().WithMessage("Event date is required");
		RuleFor(x => x.ShowBeginDate).NotNull().WithMessage("Show begin date is required");
		RuleFor(x => x.ShowEndDate).NotNull().WithMessage("Show end date is required");
		RuleFor(c => c.ShowEndDate).GreaterThanOrEqualTo(c => c.ShowBeginDate).WithMessage("Show end date is greater than or equal to show begin date");
		RuleFor(x => x.SpecialEventTypeId).NotNull().WithMessage("An event type is required");
		RuleFor(p => p.Title)
					.NotEmpty().WithMessage("Title is required")
					.MaximumLength(100).WithMessage("Title cannot be longer than 100 characters");
		RuleFor(p => p.SubTitle).MaximumLength(100).WithMessage("Sub title cannot be longer than 100 characters");
		RuleFor(p => p.ImageUrl)
			.MaximumLength(150).WithMessage("Image Url cannot be longer than 150 characters");
		RuleFor(p => p.WebsiteUrl).MaximumLength(150).WithMessage("Website Url cannot be longer than 150 characters");
		RuleFor(p => p.WebsiteDescr).MaximumLength(150).WithMessage("Website description cannot be longer than 150 characters");
		RuleFor(p => p.YouTubeId).MaximumLength(25).WithMessage("YouTube Id cannot be longer than 25 characters");
		RuleFor(p => p.Description).MaximumLength(5000).WithMessage("Description cannot be longer than 5,000 characters");
	}
}

