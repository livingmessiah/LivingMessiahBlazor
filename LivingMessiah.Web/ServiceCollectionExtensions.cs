using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

using SukkotData = LivingMessiah.Web.Features.Sukkot.Data;

using LivingMessiah.Web.Features.Sukkot.Services;
using LivingMessiah.Web.Features.SukkotAdmin.Data;
using LivingMessiah.Web.Features.SukkotAdmin.Donations.Data;
using LivingMessiah.Web.Services;
using FluentValidation;
using LivingMessiah.Web.Links;
using LivingMessiah.Web.Features.SampleCode.Syncfusion_SfDropDownList;

namespace LivingMessiah.Web;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDataStores(this IServiceCollection services)
	{
		services
			.AddSingleton<ILinkService, LinkService>()
			.AddTransient<ISecurityClaimsService, SecurityClaimsService>()
			.AddTransient<ISukkotService, SukkotService>()

			.AddTransient<IService, Service>()        //Pages.Sukkot.Services;
			.AddTransient<SukkotData.IRepository, SukkotData.Repository>()

			.AddTransient<IValidator<Features.Sukkot.NormalUser.EntryFormVM>, Features.Sukkot.NormalUser.EntryFormVMValidator>()

			.AddTransient<IDonationRepository, DonationRepository>()

			.AddTransient<SukkotData.ISukkotRepositoryUsedBySukkotService, SukkotData.SukkotRepositoryUsedBySukkotService>()
			.AddTransient<ISukkotAdminRepository, SukkotAdminRepository>()
			.AddSingleton<ISmartEnumServiceForSfDropDownList, SmartEnumServiceForSfDropDownList>()
			.AddScoped<AppState>();

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
