using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

using LivingMessiah.Data;
using LivingMessiah.Web.Pages.Admin.AudioVisual;
using LivingMessiah.Web.Pages.Admin.AudioVisual.Services;
using LivingMessiah.Web.Pages.Contacts.Data;
using LivingMessiah.Web.Pages.KeyDates.Data;
using LivingMessiah.Web.Pages.Sukkot.Data;
using LivingMessiah.Web.Pages.Sukkot.Services;
using LivingMessiah.Web.Pages.SukkotAdmin.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Services;
using LivingMessiah.Web.Pages.UpcomingEvents.Data;
using LivingMessiah.Web.Services;
using FluentValidation;
using LivingMessiah.Web.Pages.Sukkot.Components;
using LivingMessiah.Web.Pages.Parasha.Services;
using LivingMessiah.Web.Pages.Parasha.Data;
using LivingMessiah.Web.Pages.UpcomingEventsAdmin.Edit;
using LivingMessiah.Web.Links;
//using LivingMessiah.Web.Pages.Sukkot.RegistrationEntry;

namespace LivingMessiah.Web;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDataStores(this IServiceCollection services)
	{
		services
			.AddSingleton<ILinkService, LinkService>()

			.AddSingleton<IShabbatWeekService, ShabbatWeekService>()
			.AddSingleton<IShabbatWeekCacheService, ShabbatWeekCacheService>()
			.AddSingleton<IShabbatWeekRepository, ShabbatWeekRepository>()

			.AddTransient<IUpcomingEventsRepository, UpcomingEventsRepository>()
			.AddTransient<IGridDataRepository, GridDataRepository>()
			.AddTransient<ISpecialEventGridDataAdaptor, SpecialEventGridDataAdaptor>()

			.AddSingleton<IKeyDateRepository, KeyDateRepository>()
			.AddTransient<IWeeklyVideosRepository, WeeklyVideosRepository>()
			.AddTransient<ISecurityClaimsService, SecurityClaimsService>()

			//.AddSingleton<ISukkotSettings, SukkotSettings>()
			.AddTransient<ISukkotService, SukkotService>()

			.AddTransient<Pages.Sukkot.RegistrationEntry.IService, Pages.Sukkot.RegistrationEntry.Service>()
			.AddTransient<Pages.Sukkot.RegistrationEntry.IRepository, Pages.Sukkot.RegistrationEntry.Repository>()
			.AddTransient<IValidator<Pages.Sukkot.RegistrationEntry.ViewModel>, Pages.Sukkot.RegistrationEntry.ViewModelValidator>()

			.AddTransient<IDonationRepository, DonationRepository>()
			.AddTransient<ISukkotAdminService, SukkotAdminService>()
			.AddTransient<IContactRepository, ContactRepository>()

			.AddTransient<ISukkotRepository, SukkotRepository>()
			.AddTransient<ISukkotAdminRepository, SukkotAdminRepository>()
			.AddSingleton<ISmartEnumServiceForSfDropDownList, SmartEnumServiceForSfDropDownList>()
			.AddScoped<AppState>()
			.AddSingleton<IParashaRepository, ParashaRepository>()
			.AddSingleton<IParashaService, ParashaService>()
			.AddSingleton<IYouTubeFeedService, YouTubeFeedService>();
		return services;
	}

	public static IServiceCollection AddCustomAuthentication(
			this IServiceCollection services,
			 Microsoft.Extensions.Configuration.IConfiguration Configuration)
	{
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		})
				.AddCookie()
				.AddOpenIdConnect(Auth0.SchemeName, options =>
				{
					options.Authority = $"https://{Configuration[Auth0.Configuration.Domain]}";

					options.ClientId = Configuration[Auth0.Configuration.ClientId];
					options.ClientSecret = Configuration[Auth0.Configuration.ClientSecret];

					options.ResponseType = OpenIdConnectResponseType.Code;

							// Configure the scope
							options.Scope.Clear();
					options.Scope.Add("openid");
					options.Scope.Add("profile");
					options.Scope.Add("email");

							// Set the callback path, so Auth0 will call back to http://localhost:5000/signin-auth0 
							// Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard 
							options.CallbackPath = new PathString(Auth0.CallbackPath);
					options.ClaimsIssuer = Auth0.SchemeName;
					options.SaveTokens = true;

					/* 
					https://andrewlock.net/adding-authentication-to-a-blazor-server-app-using-auth0/
					https://stackoverflow.com/questions/71472048/blazor-server-get-user-from-controller-auth0
					options.TokenValidationParameters = new()
					{
						NameClaimType = "name",
					};
					
				 */
					options.TokenValidationParameters = new TokenValidationParameters
					{
						NameClaimType = "name",
						RoleClaimType = Auth0.SchemaNameSpace
					};

					// Add handling of logout
					options.Events = new OpenIdConnectEvents
					{
						OnRedirectToIdentityProviderForSignOut = (context) =>
						{
							var logoutUri = $"https://{Configuration[Auth0.Configuration.Domain]}/v2/logout?client_id={Configuration[Auth0.Configuration.ClientId]}";

							var postLogoutUri = context.Properties.RedirectUri;
							if (!string.IsNullOrEmpty(postLogoutUri))
							{
								if (postLogoutUri.StartsWith("/"))
								{
									var request = context.Request;
									postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
								}
								logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
							}

							context.Response.Redirect(logoutUri);
							context.HandleResponse();

							return Task.CompletedTask;
						}
					};
				}
		);

		return services;

	}

}
