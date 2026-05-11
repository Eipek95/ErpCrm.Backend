namespace ErpCrm.Application.Common.Interfaces;

public interface IMessageBusService
{
    Task PublishAsync<T>(
        string queueName,
        T message,
        CancellationToken cancellationToken = default);
}