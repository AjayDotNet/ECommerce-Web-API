using ECommerceApi.DTOs;

namespace ECommerceApi.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(int userId);

        Task<bool> AddToCartAsync(int userId, AddToCartDto dto);

        Task<bool> UpdateCartItemAsync(int userId, int cartItemId, UpdateCartItemDto dto);

        Task<bool> RemoveCartItemAsync(int userId, int cartItemId);

        Task<bool> ClearCartAsync(int userId);
    }
}