using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Features.PsalmsAndProverbs;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddPsalmsAndProverbs(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		return services;
	}
}
