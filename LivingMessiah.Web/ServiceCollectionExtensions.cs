using LivingMessiah.Web.Services;
using LivingMessiah.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Sukkot.Web.Service;
using SukkotApi.Data;
using LivingMessiah.Data.Commands;
using LivingMessiah.Web.Pages.SukkotAdmin.Donations.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Data;
using LivingMessiah.Web.Pages.SukkotAdmin.Registration.Services;
using LivingMessiah.Web.Pages.Contacts.Data;
using LivingMessiah.Web.Pages.KeyDate.Data;

//using Markdig;
//using Markdig.Extensions.AutoIdentifiers;

namespace LivingMessiah.Web
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDataStores(this IServiceCollection services)
		{
			services
				.AddSingleton<ILeadershipService, LeadershipService>()
				.AddSingleton<IAddressService, AddressService>()
				.AddSingleton<ILinkService, LinkService>()

				.AddSingleton<IShabbatWeekService, ShabbatWeekService>()
				.AddSingleton<IShabbatWeekCacheService, ShabbatWeekCacheService>()
				.AddSingleton<IShabbatWeekRepository, ShabbatWeekRepository>()
				.AddSingleton<IUpcomingEventsRepository, UpcomingEventsRepository>()
				.AddSingleton<IUpcomingEvents, UpcomingEvents>()
				.AddSingleton<IKeyDateRepository, KeyDateRepository>()

				.AddTransient<ISecurityClaimsService, SecurityClaimsService>()
				.AddTransient<ISukkotService, SukkotService>()
				.AddTransient<IRegistrationService, RegistrationService>()

				.AddTransient<IRegistrationRepository, RegistrationRepository>()
				.AddTransient<IDonationRepository, DonationRepository>()
				.AddTransient<ISukkotAdminService, SukkotAdminService>()
				.AddTransient<IContactRepository, ContactRepository>()

				//ToDo Remove before publishing 
				.AddTransient<Pages.BlazorExamples.ToDoSort.IFileService, Pages.BlazorExamples.ToDoSort.FileService>()
				.AddScoped<Pages.BlazorExamples.ToDoSort.IToDoService, Pages.BlazorExamples.ToDoSort.ToDoService>()

				.AddTransient<ISukkotRepository, SukkotRepository>()
				.AddTransient<ISukkotAdminRepository, SukkotAdminRepository>()
				.AddSingleton<IUpcomingEventService, UpcomingEventService>()
				.AddSingleton<ILiturgyService, LiturgyService>();

			return services;
		}
		//.AddSingleton<ILiturgyService, UpcomingEventsService>();  // ToDo: Delete?

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
}
