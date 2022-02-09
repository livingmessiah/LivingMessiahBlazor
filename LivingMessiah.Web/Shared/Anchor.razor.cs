using Microsoft.AspNetCore.Components;
using System.Text;
using LivingMessiah.Web.Infrastructure;

namespace LivingMessiah.Web.Shared;

public enum AnchorSuffixIcon
{
		None = 0,
		Internal = 1,
		External = 2
}

public partial class Anchor
{
		[Parameter]
		public bool IsPrinterFriendly { get; set; } = false;

		[Parameter]
		public string Descr { get; set; } = "Click Here";

		[Parameter]
		public string ButtonCss { get; set; } = ""; // e.g. btn btn-outline-primary btn-lg

		[Parameter]
		public string Href { get; set; } = ""; // e.g. https://MyHebrewBible.com

		[Parameter]
		public bool TargetIsBlank { get; set; } = true;

		[Parameter]
		public string Prefix { get; set; }

		[Parameter]
		public string Suffix { get; set; }

		[Parameter]
		public bool UsePrefixIcon { get; set; } = true;

		[Parameter]
		public AnchorSuffixIcon SuffixIcon { get; set; } = AnchorSuffixIcon.External;

		public string BuildAnchor
		{
				get
				{
						string s = new StringBuilder()
						.AppendWhen("<i class='fas fa-link'></i> ", UsePrefixIcon == true)
						.AppendWhen($"{Prefix} ", !string.IsNullOrWhiteSpace(Prefix))
						.Append($"<a href='{Href}'")
						.AppendWhen(" target='_blank'", TargetIsBlank == true)
						.Append($"><u>{Descr}</u></a>")
						.AppendWhen(" <i class='fas fa-arrow-circle-right'></i>", SuffixIcon == AnchorSuffixIcon.Internal)
						.AppendWhen(" <i class='fas fa-external-link-square-alt'></i>", SuffixIcon == AnchorSuffixIcon.External)
						.AppendWhen($" {Suffix} ", !string.IsNullOrWhiteSpace(Suffix))
						.ToString();
						return s;
				}
		}

		public string BuildButton
		{
				get
				{
						string s = new StringBuilder()
						.AppendWhen($"{Prefix} ", !string.IsNullOrWhiteSpace(Prefix))
						.Append($"<a href='{Href}'")
						.Append($" class='{ButtonCss}'")
						.AppendWhen(" target='_blank'", TargetIsBlank == true)
						.AppendWhen($">{Descr} <i class='fas fa-arrow-circle-right'></i></a>", SuffixIcon == AnchorSuffixIcon.Internal)
						.AppendWhen($">{Descr} <i class='fas fa-external-link-square-alt'></i></a>", SuffixIcon == AnchorSuffixIcon.External)
						.AppendWhen($" {Suffix} ", !string.IsNullOrWhiteSpace(Suffix))
						.ToString();
						return s;
				}
		}

		/*
			<li>
				<Anchor IsPrinterFriendly="@IsPrinterFriendly"
								UsePrefixIcon="false"
								Href="https://www.cinemark.com/TicketSeatMap/?TheaterId=366&ShowtimeId=385114&CinemarkMovieId=80808&Showtime=2020-02-18T19:00:00"
								Suffix="to order your ticket">
				</Anchor>
			</li>
		*/
}
