using DTOs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Command
{
    public class CreateItemCommand : IRequest<CreateItemCommandResponse>
    {
        public int id { get; set; }
        public required string ItemName { get; set; }
        public required string ItemCategory { get; set; }
    }
}
