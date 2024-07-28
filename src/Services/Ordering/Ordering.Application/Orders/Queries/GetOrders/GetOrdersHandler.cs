
using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext context)
        : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            int pageIndex = query.PaginationRequest.PageIndex;

            int pageSize = query.PaginationRequest.PageSize;

            int itemsToSkip = pageIndex * pageSize;

            long count = await context.Orders.LongCountAsync(cancellationToken);

            var orders = await context.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .OrderBy(o => o.OrderItems)
                .Skip(itemsToSkip)
                .Take(query.PaginationRequest.PageSize)
                .ToListAsync();

            var ordersDto = orders.ConvertToOrderDtoList();

            var result = new PaginatedResult<OrderDto>(pageIndex,
                pageSize,
                count,
                ordersDto);

            return new GetOrdersResult(result);
        }
    }
}