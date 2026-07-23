using OrdersAPI.Models;

namespace OrdersAPI.Services.Contracts
{
    public interface IOrderService
    {
        public List<object> GetOrders();
        public object? GetOrderById(int id);
        public object? CreateOrder(CreateOrderRequest request);
    }
}
