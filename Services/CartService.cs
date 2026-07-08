using ECommerceApi.DTOs;
using ECommerceApi.Interfaces;
using ECommerceApi.Models;

namespace ECommerceApi.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<bool> AddToCartAsync(int userId, AddToCartDto dto)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId
                };

                await _cartRepository.AddCartAsync(cart);
                await _cartRepository.SaveChangesAsync();
            }

            var existingItem = await _cartRepository
                .GetCartItemAsync(cart.Id, dto.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;

                await _cartRepository.UpdateCartItemAsync(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                };

                await _cartRepository.AddCartItemAsync(cartItem);
            }

            await _cartRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ClearCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
                return false;


            await _cartRepository.ClearCartAsync(cart.Id);

            await _cartRepository.SaveChangesAsync();


            return true;
        }

        public async Task<CartDto> GetCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                return new CartDto
                {
                    UserId = userId,
                    Items = new List<CartItemDto>()
                };
            }

            return new CartDto
            {
                CartId = cart.Id,
                UserId = cart.UserId,
                Items = cart.CartItems.Select(item => new CartItemDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Price = item.Product.Price,
                    Quantity = item.Quantity
                }).ToList()
            };
        }

        public async Task<bool> RemoveCartItemAsync(
    int userId,
    int cartItemId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
                return false;


            var cartItem = cart.CartItems
                .FirstOrDefault(x => x.Id == cartItemId);


            if (cartItem == null)
                return false;


            await _cartRepository.RemoveCartItemAsync(cartItem);

            await _cartRepository.SaveChangesAsync();


            return true;
        }

        public async Task<bool> UpdateCartItemAsync(
    int userId,
    int cartItemId,
    UpdateCartItemDto dto)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
                return false;


            var cartItem = cart.CartItems
                .FirstOrDefault(x => x.Id == cartItemId);


            if (cartItem == null)
                return false;


            cartItem.Quantity = dto.Quantity;


            await _cartRepository.UpdateCartItemAsync(cartItem);

            await _cartRepository.SaveChangesAsync();


            return true;
        }
    }

}