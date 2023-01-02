using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class ConsumerRoles:IEntity
    {
        public int ConsumerRolesId { get; set; }
        public string RoleName { get; set; }    
    }
}
