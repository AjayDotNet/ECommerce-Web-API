using ECommerceApi.Models;

namespace ECommerceApi.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(int userId);

        Task AddCartAsync(Cart cart);

        Task AddCartItemAsync(CartItem cartItem);

        Task<CartItem?> GetCartItemAsync(int cartId, int productId);

        Task UpdateCartItemAsync(CartItem cartItem);

        Task RemoveCartItemAsync(CartItem cartItem);

        Task ClearCartAsync(int cartId);

        Task SaveChangesAsync();
    }
}