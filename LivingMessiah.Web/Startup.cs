using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace LivingMessiah.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
			services.AddServerSideBlazor();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			})
					.AddCookie()
					.AddOpenIdConnect("Auth0", options =>
					{
						options.Authority = $"https://{Configuration["Auth0:Domain"]}";

						options.ClientId = Configuration["Auth0:ClientId"];
						options.ClientSecret = Configuration["Auth0:ClientSecret"];

						options.ResponseType = OpenIdConnectResponseType.Code;

						options.Scope.Clear();
						options.Scope.Add("openid");
						options.Scope.Add("profile"); // <- Optional extra
						options.Scope.Add("email");   // <- Optional extra

						options.TokenValidationParameters = new()
						{
							NameClaimType = "name",
						};

						options.CallbackPath = new PathString("/callback");
						options.ClaimsIssuer = "Auth0";
						options.SaveTokens = true;

						// Add handling of lo
						options.Events = new OpenIdConnectEvents
						{
							OnRedirectToIdentityProviderForSignOut = (context) =>
							{
								var logoutUri = $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration["Auth0:ClientId"]}";

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
					});

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
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

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
}
