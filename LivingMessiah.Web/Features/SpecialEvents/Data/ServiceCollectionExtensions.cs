using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Features.SpecialEvents.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSpecialEvents(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>()
		.AddSingleton<ICommands, Commands>()
		.AddTransient<IValidator<FormVM>, FormVMValidator>();
		return services;
	}
}

