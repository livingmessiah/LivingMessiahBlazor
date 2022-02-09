using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace LivingMessiah.Web.Pages.BlazorExamples.ToDoSort;

public interface IFileService
{
		string ReadFromFile();
		void SaveToFile(List<ToDoItem> toDoItems);
}

public class FileService : IFileService
{
		private readonly IConfiguration _configuration;

		public FileService(IConfiguration configuration)
		{
				_configuration = configuration;
		}

		public string ReadFromFile()
		{
				return File.ReadAllText(_configuration["SampleDataFile"]);
		}

		public void SaveToFile(List<ToDoItem> toDoItems)
		{
				string json = JsonConvert.SerializeObject(toDoItems);
				System.IO.File.WriteAllText(_configuration["SampleDataFile"], json);
		}
}
