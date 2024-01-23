using LivingMessiah.Web.Enums;

namespace LivingMessiah.Web.Features.SampleCode.BibleBooks;

public partial class Index
{

	BibleBook myNumbersSmartEnum = BibleBook.FromName("Numbers");

	string FluentResult = "";

	protected override void OnInitialized()
	{
		myNumbersSmartEnum
			.When(BibleBook.Genesis).Then(() => FluentResult = "Book.Genesis")
			.When(BibleBook.Numbers).Then(() => FluentResult = "Book.Numbers");

	}
}
