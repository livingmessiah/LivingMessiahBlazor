using LivingMessiah.Web.Pages.Contacts.Data;
using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Pages.Contacts.DI;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddAdminContacts(this IServiceCollection services)
	{
		services
		.AddTransient<IContactRepository, ContactRepository>();
		return services;
	}
}
