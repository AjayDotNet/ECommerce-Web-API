using ECommerceApi.DTOs;

namespace ECommerceApi.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto?> CreateOrderAsync(int userId);

        Task<OrderDto?> GetOrderByIdAsync(int orderId);

        Task<List<OrderDto>> GetUserOrdersAsync(int userId);
    }
}