namespace Basket.API.Basket.StoreBasket
{

    public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.ShoppingCart).NotNull().WithMessage("Shopping Cart cannot be null");
            RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }

    internal class StoreBasketCommandHandler(IShoppingCartRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppingCart = command.ShoppingCart;

            await repository.StoreShoppingCartAsync(command.ShoppingCart, cancellationToken);
            //TODO: update cache

            return new StoreBasketResult(command.ShoppingCart.UserName);
        }
    }
}
