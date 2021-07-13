namespace LivingMessiah.Domain.KeyDates.Queries
{
	public class SeasonOrEquinox
	{
		public int Id { get; set; }
		public int YearId { get; set; }
		public int DateId { get; set; }

		// ToDo: Duplicate in KeyDates.Enums.SeasonEnum
		public string BadgeColor { get; set; }
		public string Icon { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }

		public override string ToString()
		{
			return $@"Id: {Id}, YearId: {YearId}, DateId: {DateId}, Type: {Type}, Name: {Name}";
		}
	}
}
