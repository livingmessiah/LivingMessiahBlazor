//using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Features.Calendar.HealthChecks.Data;


public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddCalendarHealthChecks(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		//.AddTransient<IValidator<FormVM>, FormVMValidator>();
		return services;
	}
}


 