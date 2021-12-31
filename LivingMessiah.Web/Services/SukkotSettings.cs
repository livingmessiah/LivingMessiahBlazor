//using Microsoft.Extensions.Configuration;

//namespace LivingMessiah.Web.Services
//{
//	public interface ISukkotSettings
//	{
//		Settings GetSettings();
//	}


//	public class SukkotSettings : ISukkotSettings
//	{
//		//const string configationConnectionKey = "ConnectionStrings:LivingMessiah";
//		private readonly IConfiguration _config;
//		public SukkotSettings(IConfiguration config)
//		{
//			_config = config;
//		}

//		public Settings GetSettings()
//		{
//			//string connectionString = _config[configationConnectionKey];
//			_config.GetSection("").Bind(op)
//			throw new System.NotImplementedException();
//		}
//	}

//	public class Settings
//	{
//		public bool IsMealsAvailable { get; set; }
//		public bool SukkotIsOpen { get; set; }
//		public bool IsRegistrationClosed { get; set; }
//	}
//}

