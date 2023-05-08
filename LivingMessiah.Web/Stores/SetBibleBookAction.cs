using LivingMessiah.Web.Enums;

namespace LivingMessiah.Web.Stores;

public class SetBibleBookAction
{
	public BibleBook? BibleBook { get; }
	public SetBibleBookAction() { }

	public SetBibleBookAction(BibleBook BibleBook)
	{
		this.BibleBook = BibleBook;
	}

}
