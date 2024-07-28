namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public record GetOrdersByCustomerQuery(CustomerId CustomerId) 
        : IQuery<GetOrdersByCustomerByCustomerResult>;

    public record GetOrdersByCustomerByCustomerResult(IEnumerable<OrderDto> Orders);
}
