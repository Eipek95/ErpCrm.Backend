using ErpCrm.Domain.Common;

namespace ErpCrm.Domain.Events;

public class OrderCreatedEvent : DomainEvent
{
    public string OrderNumber { get; }
    public int CustomerId { get; }
    public int CreatedByUserId { get; }
    public decimal TotalAmount { get; }

    public OrderCreatedEvent(
        string orderNumber,
        int customerId,
        int createdByUserId,
        decimal totalAmount)
    {
        OrderNumber = orderNumber;
        CustomerId = customerId;
        CreatedByUserId = createdByUserId;
        TotalAmount = totalAmount;
    }
}
