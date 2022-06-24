namespace LivingMessiah.Web.Domain;

LinkBasic:			class LinkBasic Index, Title, Icon  {should be can abstract class and called BaseLink}

Link : BaseLink 
	class 
		bool SitemapUsage, bool HomeSidebarUsage, string HomeFloatRightHebrew, string HomeTitleSuffix
		Pages.KeyDates.Enums, enum FeastDayEnum 
			{	Hanukkah = 1,	Purim = 2, Passover = 3,	Weeks = 4, Trumpets = 5,	YomKippur = 6, Tabernacles = 7, HanukkahEOY = 8}
    

		List<Link> GetLinks() is used by LinkService...
			- GetHomeSidebarLinks {}
				- If SukkotIsOpen
						return links.GetLinks()
							.Where(x => x.HomeSidebarUsage == true)
							.Union(links.GetFeastLinks().Where(z => z.FeastDay == LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum.Tabernacles)).ToList();
					Else 
						return new LinksFactory().GetLinks().Where(x => x.HomeSidebarUsage == true).ToList();
			
			- GetSitemapLinks
				return links.GetLinks().Where(x => x.SitemapUsage == true).ToList();

		List<Link> GetFeastLinks()  is used by LinkService...
			public List<Link> GetFeastLinks()
			{
					return LinksFactory.GetFeastLinks().ToList();
			}

		List<LinkBasic> GetDashboardLinks();  // NOT USED
		List<LinkBasic> GetVideoProductionLinks();
		List<LinkBasic> GetEldersLinks();

1 only used by DetailKeyDate.razor (Pages\UpcomingEvents\DetailKeyDate.razor)
2 LinkBasic Used by LinkFacotry, LinkService, Home!Admin.razor 
3 Link			Used by LinkFacotry, LinkService, Home!SidebarButtons.razor.cs, Home!Sitemap.razor.cs, Parasha!Index.razor.cs


# Feast!Index code backup

## Razor
```razor
<LoadingComponent IsLoading="Feasts==null">
	<div class="list-group">
		@foreach (var item in Feasts)
		{
			<a href="@item.Page" title="@item.Hebrew.TitleSuffix @item.Hebrew.Strongs"
			 class="list-group-item list-group-item-warning ">
				<i class="@item.Icon fa-fw fa-3x" aria-hidden="true"></i>
				<span class="home-page-xs"><b>@item.Title</b></span>
				<span class="float-right text-info">
					<span class="hebrew">@item.Hebrew.FloatRightHebrew</span>
				</span>
			</a>
		}

	</div>
</LoadingComponent>

@if (DatabaseError)
{
	<p class="text-danger"><em>@DatabaseErrorMsg</em></p>
}
else
{
	if (DatabaseWarning)
	{
		<p class="text-warning">@DatabaseWarningMsg</p>
	}
}

```

## Code Behind
```csharp
namespace LivingMessiah.Web.Pages.Feasts;

using LivingMessiah.Web.LinkSmartEnums;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Page = Links.Feast;

public partial class Index
{
	[Inject]
	public ILogger<Index> Logger { get; set; }

	protected List<Feast> Feasts { get; set; }

	protected override void OnInitialized()
	{
		Logger.LogDebug(string.Format("Inside Page: {0}, Class!Method: {1}", Page.Index, nameof(Index) + "!" + nameof(OnInitializedAsync)));
		try
		{
			Feasts = Feast.List.OrderBy(o => o.Value).ToList();
			if (Feasts is null)
			{
				DatabaseWarning = true;
				DatabaseWarningMsg = "Feasts NOT FOUND";
			}
		}
		catch (Exception ex)
		{
			DatabaseError = true;
			DatabaseErrorMsg = $"Error reading database";
			Logger.LogError(ex, $"...{DatabaseErrorMsg}");
		}

	}

	// protected override async Task OnInitializedAsync() 	{ }

	#region ErrorHandling
	//protected bool DatabaseInformation = false;
	//protected string DatabaseInformationMsg { get; set; }
	protected bool DatabaseError { get; set; } = false;
	protected string DatabaseErrorMsg { get; set; }
	protected bool DatabaseWarning = false;
	protected string DatabaseWarningMsg { get; set; }

	#endregion

}



```



---

public static class Feast
{
	public const string Index = "/feast/";
	public const string Title = "Feasts";
	public const string Icon = "fas fa-pizza-slice"; // <i class="fas fa-drumstick-bite"></i> <i class="fas fa-pizza-slice"></i>
	public const string Descr = "Landing page for Feasts of YHVH";

	public static class Hanukkah
	{
		public const string Page = "/feast/Hanukkah";
		public const string Title = "Hanukkah";
	}
}







public static class Shavuot
{
	public const LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum FeastDay = LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum.Weeks;
	public const string Index = "/Shavuot";
	public const string Title = "Shavuot";
	public const string Icon = "fab fa-creative-commons-zero";
}

public static class Sukkot
{
	public const LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum FeastDay = LivingMessiah.Web.Pages.KeyDates.Enums.FeastDayEnum.Tabernacles;
	public const string Index = "/Sukkot";
	public const string Title = "Sukkot";
	public const string Title2 = "Sukkot 2021";
	public const string Icon = "fas fa-campground";
	public const string RegistrationStep = "/Sukkot/RegistrationStep"; // See Startup.cs options.Conventions.AddPageRoute("/Sukkot/RegistrationShell", "/Sukkot/Registration");
}

















@using LivingMessiah.Web.SmartEnums

@*<div class="px-0 mt-4 mb-2 mx-0">
	<div class="row">
		<div class="col-12">
			<div class="mx-auto" style="width: 300px;">
				<span class="hebrew tiny bg-light text-muted align-content-center">
					@((MarkupString)alephbet)
				</span>
			</div>	<!-- mx-auto -->
		</div>		<!-- col-12 -->
	</div>			<!-- row -->
</div>				<!-- px-0 mt-4 mb-2 mx-0  -->*@

<div class="@MediaQuery.Xs.DivClass">
	<div class="row">
		<div class="col-12">
			<div class="px-0 mt-4 mb-2 mx-0">
				<div class="mx-auto" style="width: 300px;">
					<span class="hebrew tiny bg-light text-muted align-content-center">
						@((MarkupString)alephbet)
					</span>
				</div>	<!-- mx-auto -->
			</div>		<!-- px-0 mt-4 mb-2 mx-0 -->
		</div>			<!-- col-12 -->
	</div>				<!-- row  -->
</div>					<!-- Xs -->

<div class="@MediaQuery.Sm.DivClass">
	<div class="row ">
		<div class="col-12">
			<div class="px-0 mt-4 mb-2 mx-0">
				<div class="mx-auto" style="width: 600px;">
					<span class="hebrew16 bg-light text-muted align-content-center">
						@((MarkupString)alephbet)
					</span>
				</div>	<!-- mx-auto -->
			</div>		<!-- px-0 mt-4 mb-2 mx-0 -->
		</div>			<!-- col-12 -->
	</div>				<!-- row  .... -->
</div>				<!-- Sm -->

<div class="@MediaQuery.Md.DivClass">
	<div class="px-0 mt-4 mb-2 mx-0">
		<div class="row ">
			<div class="col-12">
				<div class="mx-auto" style="width: 800px;">
				<span class="hebrew bg-light text-muted align-content-center">
					@((MarkupString)alephbet)
				</span>
			</div>	<!-- mx-auto -->
			</div>	<!-- col-12 -->
		</div>		<!-- row   -->
	</div>			<!-- px-0 mt-4 mb-2 mx-0 -->
</div>				<!-- Md -->

@*

<div class="@MediaQuery.Lg.DivClass">
	<div class="row col-12 px-0 mt-4 mb-2 mx-0">
		<div class="mx-auto" style="width: 1000px;">
			<span class="hebrew30 bg-light text-muted align-content-center">
				@((MarkupString)alephbet)
			</span>
		</div>	<!-- mx-auto -->
	</div>		<!-- row col-12 .... -->
</div>			<!-- Lg -->

<div class="@MediaQuery.Xl.DivClass">
	<div class="row col-12 px-0 mt-4 mb-2 mx-0">
		<div class="mx-auto" style="width: 1250px;">
			<span class="hebrew36 bg-light text-muted align-content-center">
				@((MarkupString)alephbet)
			</span>
		</div>	<!-- mx-auto -->
	</div>		<!-- row col-12 .... -->
</div>			<!-- Xl -->
*@

<div class="px-0 mt-4 mb-2 mx-0">
	<div class="row">
		<div class="col-12">
			<div class="mx-auto" style="width: 600px;">
				<span class="hebrew bg-warning text-danger align-content-center">
					@((MarkupString)alephbet)
				</span>
			</div>
		</div>
	</div>
</div>

@code {

	protected string alephbet = @"
	<a title='Aleph'>א</a>
	<a title='Bet'>ב</a>
	<a title='Gimel'>ג</a>
	<a title='Dalet'>ד</a>
	<a title='He'>ה</a>
	<a title='Vav'>ו</a>
	<a title='Zayin'>ז</a>
	<a title='Cheth'>ח</a>
	<a title='Teth'>ט</a>
	<a title='Yodh'>י</a>
	<a title='Kaph'>כ</a>
	<a title='Lamed'>ל</a>
	<a title='Mem'>מ</a>
	<a title='Nun'>נ</a>
	<a title='Samek'>ס</a>
	<a title='Ayin'>ע</a>
	<a title='Pe'>פ</a>
	<a title='Tsade'>צ</a>
	<a title='Qoph'>ק</a>
	<a title='Resh'>ר</a>
	<a title='Shin'>ש</a>
	<a title='Tav'>ת</a>
	&nbsp;&nbsp;-&nbsp;&nbsp;
	<a title='Kaph Sofit'>ך</a>
	<a title='Mem Sofit'>ם</a>
	<a title='Nun Sofit'>ן</a>
	<a title='Pe Sofit'>ף</a>
	<a title='Tsade Sofit'>ץ</a>
";
}

https://blazor.syncfusion.com/documentation/tooltip/getting-started
<SfTooltip ID="Tooltip" Target="#btn" Content="@Content">
    <SfButton ID="btn" Content="Show Tooltip"></SfButton>
</SfTooltip>


https://blazor.syncfusion.com/documentation/tooltip/template


@code
{
    string Content = "Lets go green & Save Earth !!";
}





	protected string greek-alephbet = @"
Α α	A a
Β β	B b
Γ γ	G g
Δ δ	D d
Ε ε	E e
Ζ ζ	Z z
Η η	Ē ē
Θ θ	Th th
Ι ι	I i
Κ κ	C c, K k
Λ λ	L l
Μ μ	M m
Ν ν	N n
Ξ ξ	X x
Ο ο	O o
Π π	P p
Ρ ρ	R r, Rh rh
Σ σ/ς	S s
Τ τ	T t
Υ υ	Y y, U u
Φ φ	Ph ph
Χ χ	Ch ch, Kh kh
Ψ ψ	Ps ps
Ω ω	Ō ō
";











@*
	ToDo: check out LivingMessiah.Web\Pages\SmartEnums\FeastDays.razor

			public bool LoadQuickly { get; set; }

		protected override void OnInitialized()
		{
				LoadQuickly = AppSettings.Value.ShabbatServiceLoadQuickly;
				SetPrintButton();
		}

<Liturgy Section="@Section.FromEnum(SectionEnum.CommunityPrayer)"
				 LoadQuickly="@LoadQuickly"
				 IsPrinterFriendly="_isPrinterFriendly">

@if (!LoadQuickly)
{
	<CascadingValue Value="@IsPrinterFriendly">
		<ImageDisplayOption>
			<img src='@Blobs.UrlShabbatService(Section.GraphicUrl)' alt="@Section.Title" class="img-fluid rounded mt-4" />
		</ImageDisplayOption>
	</CascadingValue>
}

@if (!IsPrinterFriendly)
{
	@ChildContent
}

@code {
	[Parameter] public RenderFragment ChildContent { get; set; }
	[CascadingParameter] bool IsPrinterFriendly { get; set; }
}


*@
