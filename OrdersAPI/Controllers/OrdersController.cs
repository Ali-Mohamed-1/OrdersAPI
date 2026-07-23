using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Models;
using OrdersAPI.Services;

namespace OrdersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService = new OrderService();

        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_orderService.GetOrders());
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById([FromRoute] int id)
        {
            try
            {
                var order = _orderService.GetOrderById(id);

                if (order == null)
                {
                    return NotFound($"Order with ID {id} not found.");
                }

                return Ok(order);
            }
            catch (ArgumentException ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var order = _orderService.CreateOrder(request.orderItems);
                return CreatedAtAction(nameof(GetOrderById), new { id = ((dynamic)order).OrderId }, order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            };            
        }
    }
}
