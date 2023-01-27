using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Dtos
{
    public class StorageDto:IDto
    {
        public int storageId { get; set; }
        public string productName { get; set; }
        public int ProductStock { get; set; }
        public bool IsReady { get; set; }
        public bool isDeleted { get; set; }
    }
}
