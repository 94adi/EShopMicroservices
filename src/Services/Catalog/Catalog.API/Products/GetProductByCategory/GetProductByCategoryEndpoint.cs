using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.GetProductByCategory
{

    //public class GetProductByCategoryRequest
    //{
    //    public string Category { get; set; }
    //}

    public class GetProductByCategoryResponse
    {
        public List<Product> Products { get; set; }
    }

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery { Category = category });

                var response = result.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by category")
            .WithDescription("Get Product by category");
        }
    }
}
