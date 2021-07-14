
namespace LivingMessiah.Web.Services
{
	public static class Auth0
	{
		public const string SchemeName = "Auth0";
		public const string CallbackPath = "/callback";
		public const string SchemaNameSpace = "https://schemas.livingmessiah.com/roles";

		public static class Configuration
		{
			public const string Domain = "Auth0:Domain";
			public const string ClientId = "Auth0:ClientId";
			public const string ClientSecret = "Auth0:ClientSecret";
		}
		
		public static class Tokens
		{
			public const string Access = "access_token";
			public const string Id = "id_token";
		}

		public static class Roles
		{
			public const string Admin = "admin";
			public const string User = "user";
			public const string SuperUser = "superuser";
			public const string Sukkot = "sukkot";
			public const string Elder = "elder";
			public const string AudioVisual = "audiovisual";
			public const string AdminOrElder = "admin, elder";
			public const string Announcements = "announcements";
			public const string AdminOrSukkot = "admin, sukkot";
			public const string AdminOrAnnouncements = "admin, announcements";
			public const string AdminOrSukkotOrElder = "admin, sukkot, elder";
			public const string AdminOrAudiovisual = "admin, audiovisual";
			
		}

	}
}
