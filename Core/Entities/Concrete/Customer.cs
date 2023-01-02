using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Customer:IEntity
    {
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public string MobilePhones { get; set; }
        public string Email { get; set; }
    }
}
