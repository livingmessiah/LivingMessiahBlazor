﻿//using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Features.Calendar.ManageParashaCalendar;


public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddManageParashaCalendar(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		//.AddTransient<IValidator<FormVM>, FormVMValidator>();
		return services;
	}
}


 