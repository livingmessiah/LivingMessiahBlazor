
namespace LivingMessiah.Domain.Parasha.Queries;

public class PrevNext
{
		public int PrevId { get; set; }
		//public int CurrentId { get; set; }
		public int NextId { get; set; }
		public PrevNext(int x, int y) => (PrevId, NextId) = (x, y);
}

