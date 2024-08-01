namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(ShoppingCartDto ShoppingCartDto) : 
    ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool Success);

public class CheckoutBasketCommandValidator: 
    AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCartDto).NotNull().WithMessage("BasketCheckout must not be null.");
        RuleFor(x => x.ShoppingCartDto.UserName).NotEmpty().WithMessage("Username must not be empty.");
    }
}

public class CheckoutBasketCommandHandler
    (IShoppingCartRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.GetShoppingCartAsync(command.ShoppingCartDto.UserName, cancellationToken);

        if(basket == null)
        {
            return new CheckoutBasketResult(false);
        }

        var eventMessage = command.ShoppingCartDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteShoppingCartAsync(command.ShoppingCartDto.UserName, cancellationToken);

        return new CheckoutBasketResult(true);      
    }
}
