namespace Catalog.API.Products.GetProductById
{
    //public class GetProductByIdRequest { public Guid Id { get; set; } }

    public class GetProductByIdResponse
    {
        public Product Product { get; set; }
    }

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/product/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery { Id = id });
                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by id")
            .WithDescription("Get Product by id");
        }
    }
}
