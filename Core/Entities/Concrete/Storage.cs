using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Storage:IEntity
    {
        public int ProductId { get; set; }
        public int ProductStock { get; set; }
        public bool IsReady { get; set; }
    }
}
