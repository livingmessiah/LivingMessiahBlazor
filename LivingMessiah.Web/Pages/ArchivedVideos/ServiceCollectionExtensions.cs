using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Pages.ArchivedVideos;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddArchivedVideo(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		return services;
	}
}
