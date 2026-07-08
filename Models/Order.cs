using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Pending";


        public ICollection<OrderItem> OrderItems { get; set; }
            = new List<OrderItem>();
    }
}