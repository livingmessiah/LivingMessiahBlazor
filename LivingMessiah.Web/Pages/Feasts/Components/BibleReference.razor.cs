namespace LivingMessiah.Web.Pages.Feasts.Components;
using LivingMessiah.Web.Pages.Feasts.LinkSmartEnums;
using Microsoft.AspNetCore.Components;

//ToDo This is being used
public partial class BibleReference
{
	[Parameter] public Feast? Feast { get; set; }

	protected string RelatedVerse = "";

	protected override void OnInitialized()
	{
		Feast!
			.When(Feast.Hanukkah).Then(() => RelatedVerse = "ToDo: Add John 20 reference")
			.When(Feast.Purim).Then(() => RelatedVerse = "No references in scripture")
			.When(Feast.Passover).Then(() => RelatedVerse = "ToDo: Add Leviticus 23 reference")
			//.When(Feast.Omer).Then(() => RelatedVerse = "ToDo: Add Leviticus 23 reference")
			.When(Feast.Weeks).Then(() => RelatedVerse = "ToDo: Add Leviticus 23 reference")
			.When(Feast.Trumpets).Then(() => RelatedVerse = "ToDo: Add Leviticus 23 reference")
			.When(Feast.YomKippur).Then(() => RelatedVerse = "ToDo: Add Leviticus 23 reference")
			.When(Feast.Tabernacles).Then(() => RelatedVerse = "ToDo: Add Leviticus 23 reference");
	}

}

/*
public  string Verses
{
	get
	{
		Feast
			.When(Feast.Hanukkah).Then(() => RelatedVerse = "ToDo: Add John 20 reference")
			.When(Feast.Purim).Then(() => RelatedVerse = "No references in scripture")
			.When(Feast.Passover).Then(() => RelatedVerse = "ToDo: Add Leviticus 23 reference")
			.When(Feast.Omer).Then(() => RelatedVerse = "ToDo: Add Leviticus 23 reference")
			.When(Feast.Weeks).Then(() => RelatedVerse = "ToDo: Add Leviticus 23 reference")
			.When(Feast.Trumpets).Then(() => RelatedVerse = "ToDo: Add Leviticus 23 reference")
			.When(Feast.YomKippur).Then(() => RelatedVerse = "ToDo: Add Leviticus 23 reference")
			.When(Feast.Tabernacles).Then(() => RelatedVerse = "ToDo: Add Leviticus 23 reference");
		return RelatedVerse;
	}
}


	See LivingMessiah.Web\Shared\OnegComponentBase.cs
	public static string MessageTheme
	{
			get
			{
					return (_GetNextWeekTheme()) switch
					{
							OnegThemeEnum.Family => "Family Favorite Foods",
							OnegThemeEnum.Mexican => "Mexican Foods",
							OnegThemeEnum.Italian => "Italian Foods",
							OnegThemeEnum.CrockPot => "Crock Pots and Casseroles",
							OnegThemeEnum.ColdCuts => "Cold Cuts, Cheese and Relish Dishes",
							_ => "Family Favorite Foods",
					};
			}
	}
*/
