using OrdersAPI.Models;
using OrdersAPI.Services.Contracts;

namespace OrdersAPI.Services
{
    public class OrderService : IOrderService
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

        public object CreateOrder(CreateOrderRequest request)
        {
            if (request.orderItems.Count > 10)
            {
                throw new ArgumentException("You cannot order more than 10 items in a single order.");
            }

            decimal totalPrice = request.orderItems.Sum(item => item.price * item.quantity);

            var order = new
            {
                OrderId = _orders.Count + 1,
                Items = request.orderItems,
                TotalPrice = totalPrice
            };

            _orders.Add(order);
            return order;
        }
    }
}
