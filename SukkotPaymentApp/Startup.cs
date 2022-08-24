using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(SukkotPaymentApp.Startup))]

namespace SukkotPaymentApp;
public class Startup : FunctionsStartup
{
	public override void Configure(IFunctionsHostBuilder builder)
	{
		builder.Services.AddHttpClient();


		//builder.Services.AddSingleton<IMyService>((s) =>
		//{
		//	return new MyService();
		//});

		builder.Services.AddSingleton<ICommands, Commands>();
		//builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
	}
}
/*

Microsoft.NET.Sdk.Functions

### [Caveats](https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection#caveats)

A series of registration steps run **before** and **after** the runtime processes the startup class. 
  Therefore, keep in mind the following items:

1. The startup class is meant for only setup and registration. 
	Avoid using services registered at startup during the startup process. 
	For instance, **don't try to log a message in a logger that is being registered during startup**. 
	This point of the registration process is too early for your services to be available for use. 
	After the `Configure` method is run, **the Functions runtime continues to register additional dependencies**
	, which can affect how your services operate.

2. The dependency injection container only holds explicitly registered types. 
	The only services available as injectable types are what are setup in the Configure method. 
	As a result, Functions-specific types like `BindingContext` and `ExecutionContext` aren't available during setup or as injectable types.
*/