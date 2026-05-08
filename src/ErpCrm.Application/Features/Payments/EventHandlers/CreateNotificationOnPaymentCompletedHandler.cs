using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Entities;
using ErpCrm.Domain.Events;
using MediatR;

namespace ErpCrm.Application.Features.Payments.EventHandlers;

public class CreateNotificationOnPaymentCompletedHandler
    : INotificationHandler<PaymentCompletedEvent>
{
    private readonly IAppDbContext _context;

    public CreateNotificationOnPaymentCompletedHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        PaymentCompletedEvent notification,
        CancellationToken cancellationToken)
    {
        await _context.Notifications.AddAsync(new Notification
        {
            UserId = notification.UserId,
            Title = "Payment completed",
            Message = $"Payment completed successfully. Amount: {notification.Amount}",
            IsRead = false,
            CreatedDate = DateTime.UtcNow
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}