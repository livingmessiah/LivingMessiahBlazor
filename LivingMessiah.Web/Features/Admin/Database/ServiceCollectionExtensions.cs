using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Features.Admin.Database;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDatabaseStores(this IServiceCollection services)
	{
		services
			.AddTransient<LM.IRepository, LM.Repository>()
			.AddTransient<Sukkot.IRepository, Sukkot.Repository>();
		return services;
	}
}
