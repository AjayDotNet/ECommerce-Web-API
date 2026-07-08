using ECommerceApi.DTOs;
using ECommerceApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var cart = await _cartService.GetCartAsync(userId);

            return Ok(cart);
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var result = await _cartService.AddToCartAsync(userId, dto);

            if (!result)
                return BadRequest();

            return Ok("Product added to cart");
        }
        [HttpPut("update/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(
    int cartItemId,
    UpdateCartItemDto dto)
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var result = await _cartService
                .UpdateCartItemAsync(userId, cartItemId, dto);

            if (!result)
                return NotFound();

            return Ok("Cart item updated");
        }


        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var result = await _cartService
                .RemoveCartItemAsync(userId, cartItemId);

            if (!result)
                return NotFound();

            return Ok("Cart item removed");
        }


        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var result = await _cartService
                .ClearCartAsync(userId);

            if (!result)
                return NotFound();

            return Ok("Cart cleared");
        }
    }
}