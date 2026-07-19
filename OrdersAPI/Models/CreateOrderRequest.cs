using System.ComponentModel.DataAnnotations;

namespace OrdersAPI.Models
{
    public class CreateOrderRequest
    {
        [Required]
        [MinLength(1, ErrorMessage = "At least one order item is required.")]
        public List<OrderItem> orderItems { get; set; } = new List<OrderItem>();
    }
}
