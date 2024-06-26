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

using LivingMessiah.Web.Components.ShabbatWeek;

using LivingMessiah.Web.Features.Admin.Database;
using LivingMessiah.Web.Features.Admin.Video.Data;
using LivingMessiah.Web.Features.ArchivedVideos;
using LivingMessiah.Web.Features.PsalmsAndProverbs;
using LivingMessiah.Web.Features.Calendar.Data;
using LivingMessiah.Web.Features.Calendar.ManageKeyDates.Data;
using LivingMessiah.Web.Features.Calendar.ManageParashaCalendar;
using LivingMessiah.Web.Features.InDepthStudy.Data;
using LivingMessiah.Web.Features.Calendar.HealthChecks.Data;
using LivingMessiah.Web.Features.FeastDayPlanner.Data;
using LivingMessiah.Web.Features.SpecialEvents.Data;
using LivingMessiah.Web.Features.UpcomingEvents.Weekly;


using LivingMessiah.Web.Features.Sukkot.ManageNotes.Data;
using LivingMessiah.Web.Features.Sukkot.ManageRegistration.Data;
using LivingMessiah.Web.Features.Contacts.Data;
using LivingMessiah.Web.Features.Admin.Wirecast.Data;
using LivingMessiah.Web.Features.Parasha.Data;

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
		services.AddAdminWirecast();
		services.AddArchivedVideo();

		services.AddCalendar();
		services.AddCalendarHealthChecks();
		services.AddFeastDayPlanner();
		services.AddManageKeyDates();
		services.AddInDepthStudy();
		services.AddManageParashaCalendar();
		services.AddPsalmsAndProverbs();
		services.AddParasha();
		services.AddSpecialEvents();
		services.AddUpcomingEvents();

		services.AddShabbatWeek();
		services.AddAdminContacts();

		services.AddManageNotes();
		services.AddManageRegistration();

		services.AddSession();
		services.AddBlazoredToast();
		services.AddBlazoredModal();
		services.AddCustomAuthentication(Configuration);
		services.Configure<AppSettings>(options => Configuration.GetSection("AppSettings").Bind(options));
		services.Configure<SukkotSettings>(options => Configuration.GetSection("SukkotSettings").Bind(options));
		services.Configure<DonationSettings>(options => Configuration.GetSection("DonationSettings").Bind(options));
		services.Configure<BlobSettings>(options => Configuration.GetSection("BlobSettings").Bind(options));


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

			//endpoints.MapFallbackToPage("/BlazorApp");
			////https://stackoverflow.com/questions/63071255/blazor-webassembly-load-different-scripts-for-specific-environment

		});
	}
}

// Ignore Spelling: env