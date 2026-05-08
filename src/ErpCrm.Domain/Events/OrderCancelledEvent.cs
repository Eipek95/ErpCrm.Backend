using ErpCrm.Domain.Common;

namespace ErpCrm.Domain.Events;

public class OrderCancelledEvent : DomainEvent
{
    public int OrderId { get; }
    public string OrderNumber { get; }
    public int UserId { get; }

    public OrderCancelledEvent(int orderId, string orderNumber, int userId)
    {
        OrderId = orderId;
        OrderNumber = orderNumber;
        UserId = userId;
    }
}