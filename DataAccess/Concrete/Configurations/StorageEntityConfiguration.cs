using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Configurations
{
    public class StorageEntityConfiguration: IEntityTypeConfiguration<Storage>
    {
        public virtual void Configure(EntityTypeBuilder<Storage> builder)
        {
            builder.HasKey(x => x.StorageId);
        }
    }
    
}
