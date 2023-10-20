using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
namespace LivingMessiah.Web.Pages.Sukkot.ManageRegistration.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddManageRegistration(this IServiceCollection services)
	{
		services
			.AddSingleton<IRepository, Repository>()
			.AddTransient<IRepositoryHierarchicalQuery, RepositoryHierarchicalQuery>()
			.AddTransient<IValidator<HRA.FormVM>, HRA.FormVMValidator>()
			.AddTransient<IValidator<Donations.FormVM>, Donations.FormVMValidator>()
			.AddTransient<IValidator<Registrant.FormVM>, Registrant.FormVMValidator>();
		return services;
	}
}
