using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Features.Sukkot.ManageNotes.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddManageNotes(this IServiceCollection services)
	{
		services
			.AddSingleton<IRepository, Repository>();
		
		return services;
	}
}
