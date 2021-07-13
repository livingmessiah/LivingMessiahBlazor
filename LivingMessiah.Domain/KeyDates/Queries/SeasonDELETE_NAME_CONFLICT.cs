namespace LivingMessiah.Domain.KeyDates.Queries
{
	public class SeasonDELETE_NAME_CONFLICT
	{
		public int Id { get; set; }
		public int YearId { get; set; }
		public int DateId { get; set; }
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
