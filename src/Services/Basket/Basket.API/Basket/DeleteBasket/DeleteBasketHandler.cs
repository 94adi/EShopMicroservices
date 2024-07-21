
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketCommandHandler(IShoppingCartRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            //TODO: Delete basket from DB and cache
            var deleteResult = await repository.DeleteShoppingCartAsync(command.UserName, cancellationToken);

            return new DeleteBasketResult(deleteResult);
        }
    }
}
