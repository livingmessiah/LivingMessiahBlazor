using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace LivingMessiah.Web.Features.Admin.Wirecast.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddAdminWirecast(this IServiceCollection services)
	{
		services
		.AddTransient<Data.IRepository, Data.Repository>();
		return services;
	}
}
