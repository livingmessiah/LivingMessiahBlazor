//using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Features.FeastDayPlanner.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddFeastDayPlanner(this IServiceCollection services)
	{
		services
		.AddSingleton<IService, Service>();
		//.AddSingleton<IRepository, Repository>();
		//.AddTransient<IValidator<FormVM>, FormVMValidator>();
		return services;
	}
}

