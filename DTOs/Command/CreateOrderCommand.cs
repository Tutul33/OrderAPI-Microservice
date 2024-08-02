using DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTOs.Command
{
    public class CreateOrderCommand:IRequest<CreateOrderCommandResponse>
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("productName")]
        public string ProductName { get; set; } = "";

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        // Assuming Price might not be present in your JSON
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
