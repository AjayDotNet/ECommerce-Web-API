using ECommerceApi.DTOs;
using ECommerceApi.Interfaces;
using ECommerceApi.Models;

namespace ECommerceApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public OrderService(
            IOrderRepository orderRepository,
            ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }


        public async Task<OrderDto?> CreateOrderAsync(int userId)
        {
            var cart = await _cartRepository
                .GetCartByUserIdAsync(userId);

            if (cart == null || !cart.CartItems.Any())
                return null;


            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalAmount = 0
            };


            foreach (var item in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };


                order.TotalAmount +=
                    item.Product.Price * item.Quantity;


                order.OrderItems.Add(orderItem);
            }


            await _orderRepository.AddOrderAsync(order);

            await _orderRepository.SaveChangesAsync();


            return MapToDto(order);
        }



        public async Task<OrderDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository
                .GetOrderByIdAsync(orderId);

            if (order == null)
                return null;


            return MapToDto(order);
        }



        public async Task<List<OrderDto>> GetUserOrdersAsync(int userId)
        {
            var orders = await _orderRepository
                .GetOrdersByUserIdAsync(userId);


            return orders
                .Select(MapToDto)
                .ToList();
        }



        private OrderDto MapToDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,

                Items = order.OrderItems
                    .Select(item => new OrderItemDto
                    {
                        ProductId = item.ProductId,
                        ProductName = item.Product.Name,
                        Quantity = item.Quantity,
                        Price = item.Price
                    })
                    .ToList()
            };
        }
    }
}