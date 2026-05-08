using ErpCrm.Domain.Common;

namespace ErpCrm.Application.Common.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(
        IEnumerable<IDomainEvent> domainEvents,
        CancellationToken cancellationToken = default);
}
