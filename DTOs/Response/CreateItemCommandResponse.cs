using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Response
{
    public class CreateItemCommandResponse
    {
        public int id { get; set; }
        public string ItemName { get; set; } = "";
        public string ItemCategory { get; set; } = "";
    }
}
