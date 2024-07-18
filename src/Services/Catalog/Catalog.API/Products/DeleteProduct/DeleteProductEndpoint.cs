
using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct
{

    public class DeleteProductResponse
    {
        public bool IsSuccess { get; set; }
    }

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand { Id = id });

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);

            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
        }
    }
}
