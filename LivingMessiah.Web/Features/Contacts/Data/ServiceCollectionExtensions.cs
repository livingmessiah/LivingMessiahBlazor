using Microsoft.Extensions.DependencyInjection;

namespace LivingMessiah.Web.Features.Contacts.Data;

public static class ServiceCollectionExtensions
{
		public static IServiceCollection AddAdminContacts(this IServiceCollection services)
		{
				services
				.AddTransient<IContactRepository, ContactRepository>();
				return services;
		}
}
