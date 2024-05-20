using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Features.InDepthStudy.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInDepthStudy(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		return services;
	}
}
