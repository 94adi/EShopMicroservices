using BuildingBlocks.CQRS;
using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    //request
    public record CreateProductCommand(string Name, 
        List<string> Category, 
        string Description, 
        string ImageFile, 
        decimal Price) : ICommand<CreateProductResult>;

    //response
    public class CreateProductResult
    {
        public Guid Id { get; set; }
    }

    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            //TODO: save to DB
            Guid g = Guid.NewGuid();
            return new CreateProductResult { Id = g };

        }
    }
}
