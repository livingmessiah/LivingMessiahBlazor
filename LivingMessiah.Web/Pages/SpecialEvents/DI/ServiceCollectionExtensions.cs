using LivingMessiah.Web.Pages.SpecialEvents.Data;
using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Pages.SpecialEvents.DI;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSpecialEventsOLD(this IServiceCollection services)
	{
		services
		.AddTransient<IRepository, Repository>();
		return services;
	}
}
