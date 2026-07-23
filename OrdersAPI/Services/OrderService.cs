using OrdersAPI.Models;

namespace OrdersAPI.Services
{
    public class OrderService
    {
        private static List<object> _orders = new List<object>();
        
        public List<object> GetOrders()
        {
            return _orders;
        }

        public object? GetOrderById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Order ID must be a non-negative integer.");
            }
            return _orders.FirstOrDefault(order => ((dynamic)order).OrderId == id);
        }

        public object CreateOrder(List<OrderItem> orderItems)
        {
            if (orderItems.Count > 10)
            {
                throw new ArgumentException("You cannot order more than 10 items in a single order.");
            }

            decimal totalPrice = orderItems.Sum(item => item.price * item.quantity);

            var order = new
            {
                OrderId = _orders.Count + 1,
                Items = orderItems,
                TotalPrice = totalPrice
            };

            _orders.Add(order);
            return order;
        }
    }
}
