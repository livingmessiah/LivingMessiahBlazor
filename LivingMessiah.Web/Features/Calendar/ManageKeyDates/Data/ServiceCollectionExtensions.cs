
//using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Features.Calendar.ManageKeyDates.Data;


public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddManageKeyDates(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		//.AddTransient<IValidator<FormVM>, FormVMValidator>();
		return services;
	}
}


 