//using Newtonsoft.Json.Converters;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace LivingMessiah.Web.Data;

public class Person
{
		[J("rowcount")]
		public int RowCount { get; set; }

		[J("id")]
		public string Id { get; set; }

		/*
		[J("officeenum"]
		public string OfficeEnum { get; set; }

		[J(JsonConverter = typeof(StringEnumConverter))]
		public string OfficeEnum { get; set; }

		//using Newtonsoft.Json.Converters;
		[J("officeenum", ItemConverterType = typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
		public string OfficeEnum { get; set; }
		
		*/

		[J("name")]
		public string Name { get; set; }

		[J("officeenum")]
		public string OfficeEnumString { get; set; }

		[J("imagefile")]
		public string ImageFile { get; set; }

		[J("iconfile")]
		public string IconFile { get; set; }

		[J("email")]
		public string Email { get; set; }

		[J("title")]
		public string Title { get; set; }

		[J("partialview")]
		public string PartialView { get; set; }

		[J("biofile")]
		public string BioFile { get; set; }

}
