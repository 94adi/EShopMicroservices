using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IShoppingCartRepository repository,
        IDistributedCache cache) : IShoppingCartRepository
    {
        public async Task<bool> DeleteShoppingCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            await cache.RemoveAsync(userName, cancellationToken);

            return await repository.DeleteShoppingCartAsync(userName, cancellationToken);
        }

        public async Task<ShoppingCart> GetShoppingCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cachedShoppingCart = await cache.GetStringAsync(userName, cancellationToken);
            ShoppingCart shoppingCart = null;

            if (!string.IsNullOrEmpty(cachedShoppingCart))
            {
                shoppingCart = JsonSerializer.Deserialize<ShoppingCart>(cachedShoppingCart);
            }

            if(shoppingCart is not null)
            {
                return shoppingCart;
            }

            shoppingCart = await repository.GetShoppingCartAsync(userName, cancellationToken);

            await cache.SetStringAsync(userName, JsonSerializer.Serialize(shoppingCart), cancellationToken);

            return shoppingCart;
        }

        public async Task<ShoppingCart> StoreShoppingCartAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            var shoppingCartResult = await repository.StoreShoppingCartAsync(shoppingCart, cancellationToken);

            await cache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart), cancellationToken);

            return shoppingCartResult;
        }
    }
}
