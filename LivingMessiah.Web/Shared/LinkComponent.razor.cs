using Microsoft.AspNetCore.Components;

namespace LivingMessiah.Web.Shared;

public enum LinkComponentEnum
{
	Simple = 1,
	InternalBtnSm = 2,
	InternalHome = 3,
	Location = 4
	//, External = 5,
	//ExternalButton = 6,
}


public partial class LinkComponent
{
	[Parameter] public LinkComponentEnum LinkComponentEnum { get; set; }
	[Parameter] public string?  Index { get; set; }
	[Parameter] public string?  Title { get; set; }
	[Parameter] public string?  Icon { get; set; }
	[Parameter] public string?  TitleSuffix { get; set; }
	[Parameter] public string?  FloatRightHebrew { get; set; }  // eg  = "שָּׁמַיִם";

	/*
	public static class Fragments
	{
		public const string?  Top = "Top";
		public const string?  Leadership = "Leadership";
	}
	*/

}
