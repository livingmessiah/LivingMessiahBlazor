﻿using LivingMessiah.Web.SmartEnums;
using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Pages.WindmillRanch.Components;

public partial class SwaleRainEventCarousel
{
	[Parameter, EditorRequired] public MediaQuery mq { get; set; }

	protected string width = "";
	protected string height = "";

	protected override void OnInitialized()
	{
		mq
			.When(MediaQuery.Xs).Then(() =>
			{
				width = "400px";
				height = "240px";
			})
			.When(MediaQuery.Sm).Then(() =>
			{
				width = "500px";
				height = "300px";
			})
			.When(MediaQuery.Md).Then(() =>
			 {
				 width = "700px";
				 height = "420px";
			 })
			.When(MediaQuery.Lg).Then(() =>
			{
				width = "900px";
				height = "540px";
			})
			.When(MediaQuery.Xl).Then(() =>
			{
				width = "1100px";
				height = "660px";
			});

	}
}