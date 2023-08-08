using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace LivingMessiah.Web.Pages.Admin.Video.DI;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddAdminVideo(this IServiceCollection services)
	{
		services
		.AddTransient<Data.IRepository, Data.Repository>()
		.AddSingleton<Services.IYouTubeFeedService, Services.YouTubeFeedService>()
		.AddTransient<IValidator<AddEdit.FormVM>, AddEdit.FormVMValidator>();
		return services;
	}
}
