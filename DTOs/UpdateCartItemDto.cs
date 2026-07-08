using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.DTOs
{
    public class UpdateCartItemDto
    {
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}