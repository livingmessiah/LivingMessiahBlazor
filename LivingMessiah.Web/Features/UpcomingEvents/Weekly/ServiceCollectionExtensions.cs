using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Features.UpcomingEvents.Weekly;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUpcomingEvents(this IServiceCollection services)
	{
		services
		.AddSingleton<ICacheService, CacheService>()
		.AddSingleton<IRepository, Repository>();
		return services;
	}
}
