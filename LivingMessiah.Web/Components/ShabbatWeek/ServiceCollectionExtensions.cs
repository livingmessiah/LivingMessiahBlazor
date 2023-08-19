using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Components.ShabbatWeek;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddShabbatWeek(this IServiceCollection services)
	{
		services
		.AddSingleton<ICacheService, CacheService>()
		.AddSingleton<IRepository, Repository>();
		return services;
	}
}
