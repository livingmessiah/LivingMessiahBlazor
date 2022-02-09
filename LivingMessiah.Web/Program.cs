using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace LivingMessiah.Web;

public class Program
{
		public static void Main(string[] args)
		{
				string appSettingJson;
				if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
				{
						appSettingJson = "appsettings.Development.json";
				}
				else
				{
						appSettingJson = "appsettings.Production.json";
				}

				var configuration = new ConfigurationBuilder()
					.AddJsonFile(appSettingJson)  // "appsettings.json"
					.Build();

				Log.Logger = new LoggerConfiguration()
					.ReadFrom.Configuration(configuration)
					.CreateLogger();
				Log.Warning($"Inside {nameof(Program)}; testing that this message gets saved to the Serilog console and file sinks. ASPNETCORE_ENVIRONMENT: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
				try
				{
						Log.Information("Application Starting Up"); // Note 1
						CreateHostBuilder(args).Build().Run();
				}
				catch (Exception ex)
				{
						Log.Fatal(ex, "The application failed to start correctly"); // Total fale
				}
				finally
				{
						Log.CloseAndFlush(); // Note 2
				}
		}
		/*
		Note 1: because we are in static void Main, we have to use the static keyword Log.Information not LogInformation
		        i.e. we can't use the ILogger right now we must use the Serilog logger
		Note 2: If you have any log messages that are pending, then this will make sure they are written.
	 */

		public static IHostBuilder CreateHostBuilder(string[] args) =>
				Host.CreateDefaultBuilder(args)
						.UseSerilog()
						.ConfigureWebHostDefaults(webBuilder =>
						{
								webBuilder.UseStartup<Startup>();
						});
}
