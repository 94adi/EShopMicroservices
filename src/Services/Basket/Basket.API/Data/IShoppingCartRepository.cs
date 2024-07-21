namespace Basket.API.Data
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartAsync(string userName, CancellationToken cancellationToken = default);

        Task<ShoppingCart> StoreShoppingCartAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default);

        Task<bool> DeleteShoppingCartAsync(string userName, CancellationToken cancellationToken = default);
    }
}
