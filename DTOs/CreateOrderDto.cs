namespace ECommerceApi.DTOs
{
    public class CreateOrderDto
    {
        public List<int> ProductIds { get; set; } = new();
    }
}