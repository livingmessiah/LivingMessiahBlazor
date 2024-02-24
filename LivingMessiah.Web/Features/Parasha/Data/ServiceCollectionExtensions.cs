//using FluentValidation;
using LivingMessiah.Web.Features.Parasha.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Features.Parasha.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddParasha(this IServiceCollection services)
	{
		services
			.AddSingleton<IRepository, Repository>()
			.AddSingleton<IParashaService, ParashaService>();
		//.AddTransient<IValidator<FormVM>, FormVMValidator>();
		return services;
	}
}

