
using Catalog.API.Products.GetProductById;
using System.Collections.Generic;

namespace Catalog.API.Products.GetProductByCategory
{

    public class GetProductByCategoryQuery : IQuery<GetProductByCategoryResult>
    {
        public string Category { get; set; }
    }

    public class GetProductByCategoryResult
    {
        public List<Product> Products { get; set; } = new();
    }

        internal class GetProductByCategoryHandler(IQuerySession session) 
            : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
        {
            public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
            {
                var result = await session.Query<Product>()
                                        .Where(p => p.Category.Contains(query.Category))
                                        .ToListAsync();

                if(result is null || result.Count == 0)
                {
                    throw new Exception("Could not get product by category");
                }

                return new GetProductByCategoryResult { Products = (List < Product > )result };
            }
        }
    }
