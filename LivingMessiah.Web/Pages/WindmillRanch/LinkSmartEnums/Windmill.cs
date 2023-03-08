using Ardalis.SmartEnum;
using PageLink = LivingMessiah.Web.Pages.WindmillRanch.LinkSmartEnums.WindmillRanchLinks;

namespace LivingMessiah.Web.Pages.WindmillRanch.LinkSmartEnums;


/*
public static class WindmillRanch
{
	public const string Index = "/windmillranch/";
	public const string Title = "Windmill Ranch";
	public const string Icon = "fas fa-dharmachakra";  //fas fa-tractor
	public const string Descr = "Landing page for the Windmill Ranch project";
*/

public abstract class Windmill : SmartEnum<Windmill>
{
	#region Id's
	private static class Id
	{
		internal const int Audit = 1;
		internal const int Permaculture = 2;
		internal const int Projects = 3;
		internal const int Album = 4;
		internal const int Support = 5;
		internal const int Archive = 6;
		internal const int CodeOfConduct = 7;
		internal const int Swales = 8;  // really part of Permaculture, but such a big thing, it get's its on URL
		internal const int Bulldozer = 9;
		internal const int Garden = 10;
		internal const int Backhoe = 11;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Windmill Audit = new AuditSE();
	public static readonly Windmill Permaculture = new PermacultureSE();
	public static readonly Windmill Projects = new ProjectsSE();
	public static readonly Windmill Album = new AlbumSE();
	public static readonly Windmill Support = new SupportSE();
	public static readonly Windmill Archive = new ArchiveSE();
	public static readonly Windmill CodeOfConduct = new CodeOfConductSE();
	public static readonly Windmill Swales = new SwalesSE();
	public static readonly Windmill Bulldozer = new BulldozerSE();
	public static readonly Windmill Garden = new GardenSE();
	public static readonly Windmill Backhoe = new BackhoeSE();

	// SE=SmartEnum
	#endregion

	private Windmill(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Color { get; }  // badge bg-dark
	public abstract string Page { get; }
	public abstract string Title { get; }
	#endregion

	#region Private Instantiation
	private sealed class AuditSE : Windmill
	{
		public AuditSE() : base($"{nameof(Id.Audit)}", Id.Audit) { }
		public override string Color => "dark";
		public override string Page => PageLink.Audit.Page;
		public override string Title => PageLink.Audit.Title;
	}

	private sealed class PermacultureSE : Windmill
	{
		public PermacultureSE() : base($"{nameof(Id.Permaculture)}", Id.Permaculture) { }
		public override string Color => "success";
		public override string Page => PageLink.Permaculture.Page;
		public override string Title => PageLink.Permaculture.Title;
	}

	private sealed class ProjectsSE : Windmill
	{
		public ProjectsSE() : base($"{nameof(Id.Projects)}", Id.Projects) { }
		public override string Color => "warning";
		public override string Page => PageLink.Projects.Page;
		public override string Title => PageLink.Projects.Title;
	}

	private sealed class AlbumSE : Windmill
	{
		public AlbumSE() : base($"{nameof(Id.Album)}", Id.Album) { }
		public override string Color => "info";
		public override string Page => PageLink.Album.Page;
		public override string Title => PageLink.Album.Title;
	}

	private sealed class SupportSE : Windmill
	{
		public SupportSE() : base($"{nameof(Id.Support)}", Id.Support) { }
		public override string Color => "success";
		public override string Page => PageLink.Support.Page;
		public override string Title => PageLink.Support.Title;
	}

	private sealed class ArchiveSE : Windmill
	{
		public ArchiveSE() : base($"{nameof(Id.Archive)}", Id.Archive) { }
		public override string Color => "danger";
		public override string Page => PageLink.Archive.Page;
		public override string Title => PageLink.Archive.Title;
	}

	private sealed class CodeOfConductSE : Windmill
	{
		public CodeOfConductSE() : base($"{nameof(Id.CodeOfConduct)}", Id.CodeOfConduct) { }
		public override string Color => "secondary";
		public override string Page => PageLink.CodeOfConduct.Page;
		public override string Title => PageLink.CodeOfConduct.Title;
	}

	private sealed class SwalesSE : Windmill
	{
		public SwalesSE() : base($"{nameof(Id.Swales)}", Id.Swales) { }
		public override string Color => "primary";
		public override string Page => PageLink.Swales.Page;
		public override string Title => PageLink.Swales.Title;
	}

	private sealed class BulldozerSE : Windmill
	{
		public BulldozerSE() : base($"{nameof(Id.Bulldozer)}", Id.Bulldozer) { }
		public override string Color => "primary";
		public override string Page => PageLink.Bulldozer.Page;
		public override string Title => PageLink.Bulldozer.Title;
	}

	private sealed class GardenSE : Windmill
	{
		public GardenSE() : base($"{nameof(Id.Garden)}", Id.Garden) { }
		public override string Color => "warning";
		public override string Page => PageLink.Garden.Page;
		public override string Title => PageLink.Garden.Title;
	}

	private sealed class BackhoeSE : Windmill
	{
		public BackhoeSE() : base($"{nameof(Id.Backhoe)}", Id.Backhoe) { }
		public override string Color => "success";
		public override string Page => PageLink.Backhoe.Page;
		public override string Title => PageLink.Backhoe.Title;
	}

	#endregion

}
