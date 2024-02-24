using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using HtmlAgilityPack;
using System.Linq;

namespace LivingMessiah.Web.Components.ShabbatWeek;

public partial class PsalmAndProverbCurrent
{
	[Inject] public ICacheService? svc { get; set; }
	[Inject] public ILogger<PsalmAndProverbCurrent>? Logger { get; set; }
	[Parameter] public bool PerformHtmlValidation { get; set; } = true;

	protected PsalmAndProverbCurrentVM? VM;
	protected string? ShabbatDateYMD;

	protected string? PsalmsHeading;
	protected string? PsalmsVerses;
	protected string? ProverbsHeading;
	protected string? ProverbsVerses;

	protected bool LoadFailed;

	protected override async Task OnInitializedAsync()
	{
		try
		{
			//Logger.LogDebug($"Inside {nameof(PsalmAndProverbCurrent)}!{nameof(OnInitializedAsync)}");
			VM = await svc!.GetCurrentPsalmAndProverb(UseCache: true)!;
			ShabbatDateYMD = VM.ShabbatDate.ToString("MMM dd");
			PsalmsHeading = BuildHeading(VM.PsalmsBCV, VM.PsalmsTitle);
			PsalmsVerses = ValidateHtml(VM.PsalmsKJVHtmlConcat, hasNestedTags: true);

			ProverbsHeading = BuildHeading(VM.ProverbsBCV, "");
			ProverbsVerses = ValidateHtml(VM.ProverbsKJVHtmlConcat, hasNestedTags: true);
			//Logger.LogDebug($"...{nameof(PsalmsHeading)}: {PsalmsHeading}; {nameof(ProverbsHeading)}: {ProverbsHeading}");
		}
		catch (Exception ex)
		{
			LoadFailed = true;
			Logger!.LogWarning(ex, $"Failed to load page {nameof(PsalmAndProverbCurrent)}, PerformHtmlValidation: {PerformHtmlValidation}");
		}
	}

	private string BuildHeading(string? bookChapterVerse, string? Title)
	{
		//string?  s = $"{bookChapterVerse} <span class='float-end'><small><i>{ValidateHtml(Title)}</i></small></span>";
		string?  s = $"{bookChapterVerse}";
		return ValidateHtml(s, hasNestedTags: true);
	}

	private string ValidateHtml(string? html, bool hasNestedTags = false)
	{
		if (!PerformHtmlValidation)
		{
			return html!;
		}
		else
		{
			var htmlDoc = new HtmlDocument();
			htmlDoc.OptionFixNestedTags = hasNestedTags;
			htmlDoc.LoadHtml(html);
			if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
			{
				return $"<b>Html not well formed; inside {nameof(PsalmAndProverbCurrent)}!{nameof(ValidateHtml)}, hasNestedTags={hasNestedTags}</b>";
			}
			else
			{
				return html!;
			}
		}
	}

}
