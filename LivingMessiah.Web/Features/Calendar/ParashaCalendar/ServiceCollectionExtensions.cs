
//using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Features.Calendar.ParashaCalendar;


public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddParashaCalendar(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		//.AddTransient<IValidator<FormVM>, FormVMValidator>();
		return services;
	}
}


 