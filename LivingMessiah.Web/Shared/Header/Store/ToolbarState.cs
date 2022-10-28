using LivingMessiah.Web.Enums;
using LivingMessiah.Web.Shared.Header.Enums;

namespace LivingMessiah.Web.Shared.Header.Store;

[FeatureState]
public class ToolbarState
{
	public BibleBook BibleBook { get; }
	public BibleWebsite BibleWebsite { get; }
	public ToolbarState() { }

	public ToolbarState(BibleBook BibleBook, BibleWebsite BibleWebsite)
	{
		this.BibleBook = BibleBook;
		this.BibleWebsite = BibleWebsite;
	}
}

