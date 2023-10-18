using LivingMessiah.Web.Pages.SukkotAdmin.Data;
using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Pages.SukkotAdmin.RegistrationNotes.Data;

public static class ServiceCollectionExtensions
{	
	public static IServiceCollection AddRegistrationNotes(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		return services;
	}
}
