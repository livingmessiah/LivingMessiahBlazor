using Microsoft.AspNetCore.Components;
using MediaQueryEnum = LivingMessiah.Web.Enums.MediaQuery;

namespace LivingMessiah.Web.Features.WindmillRanch.Components;

public partial class NewsLetterCarousel
{
	[Parameter, EditorRequired] public MediaQueryEnum? mq { get; set; }

	protected string width = "";
	protected string height = "";

	protected override void OnInitialized()
	{
		mq!
			.When(MediaQueryEnum.Xs).Then(() =>
			{
				width = "400px";
				height = "240px";
			})
			.When(MediaQueryEnum.Sm).Then(() =>
			{
				width = "500px";
				height = "300px";
			})
			.When(MediaQueryEnum.Md).Then(() =>
			 {
				 width = "700px";
				 height = "420px";
			 })
			.When(MediaQueryEnum.Lg).Then(() =>
			{
				width = "900px";
				height = "540px";
			})
			.When(MediaQueryEnum.Xl).Then(() =>
			{
				width = "1100px";
				height = "660px";
			});

	}
}
