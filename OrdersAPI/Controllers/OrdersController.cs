using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Models;

namespace OrdersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private static List<object> _orders = new List<object>(); 

        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_orders);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            var order = new
            {
                OrderId = _orders.Count + 1,
                Items = request.orderItems
            };

            _orders.Add(order);

            return Ok(order);
        }
    }
}
