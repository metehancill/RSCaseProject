using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Dtos
{
    public class OrderDto:IDto
    {
        public int orderId { get; set; }
        public string productName { get; set; }
        public string customerName { get; set; }
        public string piece { get; set; }
        public bool isDeleted { get; set; }
    }
}
