namespace Basket.API.Data
{
    public class ShoppingCartRepository(IDocumentSession session) : IShoppingCartRepository
    {
        public async Task<bool> DeleteShoppingCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(userName);

            await session.SaveChangesAsync();

            return true;
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);

            if(basket is null)
            {
                throw new BasketNotFoundException(userName);
            }

            return basket;
        }

        public async Task<ShoppingCart> StoreShoppingCartAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            session.Store(shoppingCart);

            await session.SaveChangesAsync();

            return shoppingCart;
        }
    }
}
