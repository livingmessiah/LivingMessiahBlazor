using LivingMessiah.Domain.KeyDates.Enums;

namespace LivingMessiah.Domain.KeyDates.Queries
{
	public class FeastDayDetail
	{ 
		public int Id { get; set; }  // See Note 1
		public int FeastDayId { get; set; }
		public int Detail { get; set; }
		public string Name { get; set; }
		public string Transliteration { get; set; } // optional
		public string Hebrew { get; set; }         // optional
		public string Note { get; set; }

		public override string ToString()
		{
			return $@"Id: {Id}, FeastDayId: {FeastDayId}, Detail: {Detail}, Name: {Name}";
		}
	}
}

/*
# Note 1 
 I don't need an Enum (see next line) because I can get it with the SmartEnum (see next line +1)

### Definition
```csharp
	public FeastDayDetailEnum FeastDayDetailEnum { get; set; } 
```

### Usage
```csharp
	@Date.AddDays(FDD.FromInt(fdd.Id).AddDays).ToString(@DateFormat.ddd_mm_dd)
```



 */

