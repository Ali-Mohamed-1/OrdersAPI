namespace OrdersAPI.Models
{
    public class CreateOrderRequest
    {
        public List<OrderItem> orderItems { get; set; } = new List<OrderItem>();
    }
}
