namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameQueryHandler(IApplicationDbContext context) 
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await context.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Equals(query.OrderName))
                .OrderBy(o => o.OrderName)
                .ToListAsync(cancellationToken);

            var orderDtos = orders.ConvertToOrderDtoList();

            return new GetOrdersByNameResult(orderDtos);
        }
    }
}