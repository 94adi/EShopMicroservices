namespace Catalog.API.Products.GetProductById
{

    public class GetProductByIdQuery : IQuery<GetProductByIdResult> { public Guid Id { get; set; } }

    public class GetProductByIdResult
    {
        public Product Product { get; set; }
    }

    internal class GetProductByIdHandler(IQuerySession session) 
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if(result is null)
            {
                throw new ProductNotFoundException(query.Id);
            }

            return new GetProductByIdResult { Product = result };
        }
    }
}
