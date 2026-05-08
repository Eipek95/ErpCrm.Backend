using ErpCrm.Domain.Common;

namespace ErpCrm.Domain.Events;

public class PaymentCompletedEvent : DomainEvent
{
    public int PaymentId { get; }
    public int OrderId { get; }
    public int UserId { get; }
    public decimal Amount { get; }

    public PaymentCompletedEvent(
        int paymentId,
        int orderId,
        int userId,
        decimal amount)
    {
        PaymentId = paymentId;
        OrderId = orderId;
        UserId = userId;
        Amount = amount;
    }
}