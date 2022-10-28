using LivingMessiah.Web.Shared.Header.Enums;

namespace LivingMessiah.Web.Stores;

public class SetBibleWebsiteAction
{
	public BibleWebsite BibleWebsite { get; } 
	public SetBibleWebsiteAction() { }

	public SetBibleWebsiteAction(BibleWebsite BibleWebsite)
	{
		this.BibleWebsite = BibleWebsite;
	}
}
