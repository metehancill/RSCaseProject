using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Consumer:IEntity
    {
        public int ConsumerId { get; set; }
        public string FullName { get; set; }
        public string MobilePhones { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string Password { get; set; }
        public int RolId { get; set; }
    }
}
