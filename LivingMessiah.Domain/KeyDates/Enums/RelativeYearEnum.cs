namespace LivingMessiah.Domain.KeyDates.Enums
{
	public enum RelativeYearEnum
	{
		None = 0,
		Previous = 1,
		Current = 2,
		Next = 3
	}
}
/*
 - None = 0,  // Used this for testing scenarios where not data is gotten
 - LivingMessiah.Web\Components\Pages\KeyDate\Index.razor
   <HebrewYear RelativeYear="RelativeYear.None"></HebrewYear>
*/