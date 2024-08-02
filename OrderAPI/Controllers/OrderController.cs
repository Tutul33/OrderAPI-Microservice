using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using OrderAPI.Common;
using DTOs.Command;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderCommand order)
        {
            // Serialize order to JSON
            var orderJson = JsonSerializer.Serialize(order);

            // Publish message to RabbitMQ
            PublishMessages.Publish("orderQueue", orderJson);

            return Ok("Order created and message sent to inventory service.");
        }

        [HttpGet]
        [Route("GetOrder")]
        public IActionResult GetOrder()
        {
           return Ok("Get List.");
        }
        [HttpGet("{everything}")]
        public IActionResult Get(string everything)
        {
            return Ok($"Response from Service 2: {everything}");
        }
    }
}
