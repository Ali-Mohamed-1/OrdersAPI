using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrdersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = new List<object>
            {
                new { OrderId = 1, TotalAmount = 100.50 },
                new { OrderId = 2, TotalAmount = 200.75 },
                new { OrderId = 3, TotalAmount = 300.00 }
            };

            return Ok(orders);
        }
    }
}
