namespace LivingMessiah.Web.Pages.KeyDates.Components
{
	public class YearLookupVM
	{
		public string ID { get; set; }
		public string Text { get; set; }

		public override string ToString()
		{
			return $@" ID:{ID}; Text:{Text}";
		}
	}
}
