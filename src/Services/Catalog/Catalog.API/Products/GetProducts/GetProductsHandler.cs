namespace Catalog.API.Products.GetProducts
{

    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult> { }

    public class GetProductsResult
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }

    internal class GetProductsQueryHandler(IDocumentSession session) 
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);   
            
            return new GetProductsResult {   Products = products };
        }
    }
}
