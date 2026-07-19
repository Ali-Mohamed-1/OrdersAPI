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

        [HttpGet("{id}")]
        public IActionResult GetOrderById([FromRoute] int id)
        {
            var order = _orders.FirstOrDefault(order => ((dynamic)order).OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
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
