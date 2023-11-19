//using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Features.Calendar.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddCalendar(this IServiceCollection services)
	{
		services
		.AddSingleton<IService, Service>()
		.AddSingleton<IRepository, Repository>();
		//.AddTransient<IValidator<FormVM>, FormVMValidator>();
		return services;
	}
}

