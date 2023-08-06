using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Shared.Database;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDatabaseStores(this IServiceCollection services)
	{
		services
			.AddTransient<IRepositoryLivingMessiah, RepositoryLivingMessiah>();
		return services;
	}
}
