using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Syncfusion.Blazor;
using Blazored.Toast;
using Blazored.Modal;
using LivingMessiah.Web.Settings;
using LivingMessiah.Web.Shared.Database;

using LivingMessiah.Web.Pages.Admin.Video.DI;

namespace LivingMessiah.Web;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddApplicationInsightsTelemetry();
		services.AddRazorPages();
		services.AddOptions();

		services.AddServerSideBlazor()
				.AddCircuitOptions(options =>
				{
							//can toggle detailed errors on or off from app settings
							options.DetailedErrors = System.Convert.ToBoolean(Configuration["DetailedErrors"]);
				});

		services.AddDataStores();
		services.AddDatabaseStores();

		services.AddAdminVideo();

		services.AddSession();
		services.AddBlazoredToast();
		services.AddBlazoredModal();
		services.AddCustomAuthentication(Configuration);
		services.Configure<AppSettings>(options => Configuration.GetSection("AppSettings").Bind(options));
		services.Configure<SukkotSettings>(options => Configuration.GetSection("SukkotSettings").Bind(options));

		services.AddFluxor(x => x
				.ScanAssemblies(typeof(Startup).Assembly)
		);
		services.AddSyncfusionBlazor();
	}


	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		else
		{
			app.UseExceptionHandler("/Error");
			app.UseHsts();
		}

		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Configuration["SyncfusionLicense"]);
		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseSerilogRequestLogging();
		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapRazorPages();
			endpoints.MapBlazorHub();
			endpoints.MapFallbackToPage("/_Host");
		});
	}
}
