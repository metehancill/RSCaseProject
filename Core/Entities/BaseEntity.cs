using System;

namespace Core.Entities
{
    public class BaseEntity : IEntity
    {
       public virtual int Id { get; set; }
       public int CreatedConsumerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedConsumerId { get; set; }

        public bool isDeleted { get; set; }
    }
}
