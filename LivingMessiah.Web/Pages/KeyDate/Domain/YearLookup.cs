
namespace LivingMessiah.Web.Pages.KeyDate.Domain
{
	public class YearLookup
	{
		public string ID { get; set; }
		public string Text { get; set; }
		
		public override string ToString()
		{
			return $@" ID:{ID}; Text:{Text}";
		}
	}
}


/*
Used by 
  LivingMessiah.Web\Pages\KeyDate\CalendarGrid.razor(39)
  LivingMessiah.Web\Pages\KeyDate\Data\KeyDateRepository.
  LivingMessiah.Web\Pages\KeyDate\Services\KeyDateService.
*/