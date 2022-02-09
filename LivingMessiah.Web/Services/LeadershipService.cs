using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;

namespace LivingMessiah.Web.Services;

public class LeadershipService : ILeadershipService
{
		private string _jsonFilename;
		public LeadershipService()  //string jsonFilename
		{
				_jsonFilename = "Leadership.json";
		}

		public List<Domain.Person> LoadPeople()
		{
				List<Data.Person> people = new List<Data.Person>();
				using (StreamReader r = new StreamReader(_jsonFilename))
				{
						string json = r.ReadToEnd();
						people = JsonConvert.DeserializeObject<List<Data.Person>>(json);
				}

				var query = from j in people
										select new Domain.Person()
										{
												RowCount = j.RowCount,
												Id = j.Id,
												OfficeEnumString = j.OfficeEnumString,
												Name = j.Name,
												ImageFile = j.ImageFile,
												IconFile = j.IconFile,
												Email = j.Email,
												Title = j.Title,
												BioFile = j.BioFile
										};
				return query.ToList();
		}
}
