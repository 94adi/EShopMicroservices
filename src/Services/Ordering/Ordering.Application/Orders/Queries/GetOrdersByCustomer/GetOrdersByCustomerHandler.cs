namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerHandler(IApplicationDbContext context)
        : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerByCustomerResult>
    {
        public async Task<GetOrdersByCustomerByCustomerResult> Handle(GetOrdersByCustomerQuery query, 
            CancellationToken cancellationToken)
        {
            var orders = await context.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.CustomerId.Value == query.CustomerId.Value)
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);

            var ordersDto = orders.ConvertToOrderDtoList();

            return new GetOrdersByCustomerByCustomerResult(ordersDto);
        }
    }
}