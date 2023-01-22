using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Customer:IEntity
    {
        public Customer()
        {
            LastUpdatedDate = CreatedDate = DateTime.Now;

        }
        public string CustomerName { get; set; }
        public int customerId { get; set; }
        public string Address { get; set; }
        public string MobilePhones { get; set; }
        public string Email { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public bool isDeleted { get; set; }
    }
}
