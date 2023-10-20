﻿using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Pages.Sukkot.ManageNotes.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddManageNotes(this IServiceCollection services)
	{
		services
			.AddSingleton<IRepository, Repository>();
		
		return services;
	}
}
