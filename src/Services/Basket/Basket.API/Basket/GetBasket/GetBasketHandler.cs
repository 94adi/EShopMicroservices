namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShoppingCart ShoppingCart);

    internal class GetBasketQueryHandler(IShoppingCartRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var shoppingCart = await repository.GetShoppingCartAsync(query.UserName, cancellationToken);

            if(shoppingCart is null)
            {
                throw new BasketNotFoundException(query.UserName);
            }

            return new GetBasketResult(shoppingCart);
        }
    }
}