
using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct
{
    public class DeleteProductCommand : ICommand<DeleteProductResult>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProductResult
    {
        public bool IsSuccess { get; set; }
    }

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
        }
    }

    internal class DeleteProductCommandHandler(IDocumentSession session) 
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id);

            if(product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }

            session.Delete(product);

            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult { IsSuccess = true };
        }
    }
}
