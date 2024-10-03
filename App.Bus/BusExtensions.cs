using App.Domain.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Bus;

public static class BusExtensions
{
	public static void AddBusExt(this IServiceCollection services, IConfiguration configuration)
	{
		var serviceBusOption = configuration.GetSection(nameof(ServiceBusOption)).Get<ServiceBusOption>();

		services.AddMassTransit(x =>
		{
			x.UsingRabbitMq((context, cfg) =>
			{
				cfg.Host(new Uri(serviceBusOption!.Url),
					h => { });
			});
		});
	}
}