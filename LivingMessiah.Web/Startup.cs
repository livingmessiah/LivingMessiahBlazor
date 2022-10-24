
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Syncfusion.Blazor;
using Blazored.Toast;

using LivingMessiah.Web.Settings;
using LivingMessiah.Web.Store;
using LivingMessiah.Web.Store.Toolbar;

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
		services.AddSession();
		services.AddBlazoredToast();
		services.AddCustomAuthentication(Configuration);
		services.Configure<AppSettings>(options => Configuration.GetSection("AppSettings").Bind(options));
		services.Configure<SukkotSettings>(options => Configuration.GetSection("SukkotSettings").Bind(options));

		/*
		// Add the following for Fluxor
		var currentAssembly = typeof(Program).Assembly;
			services.AddFluxor(options =>
				options.ScanAssemblies(currentAssembly));
		*/
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
