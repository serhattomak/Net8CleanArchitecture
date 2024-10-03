using App.Domain.Events;

namespace App.Application.Contracts.ServiceBus;

public interface IServiceBus
{
	Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : IMessage, IEvent;
	Task SendAsync<T>(T message, string queueName, CancellationToken cancellationToken = default) where T : IMessage, IEvent;
}