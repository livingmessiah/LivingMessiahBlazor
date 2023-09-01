using LivingMessiah.Web.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace LivingMessiah.Web.Features.Donations;

public partial class Index
{
	[Inject] public IOptions<DonationSettings>? DonationSettings { get; set; }

	private string? StripeBuyButtonId;
	private string? StripePublishableKey;
	const string? QRC_Image = "qr_donation_14kfZN4jYc0r1cQ4gh.jpg";

	protected override void OnInitialized()
	{
		base.OnInitialized();
		StripeBuyButtonId = DonationSettings!.Value.StripeBuyButtonId;
		StripePublishableKey = DonationSettings!.Value.StripePublishableKey;
	}
}
