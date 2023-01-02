using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Configurations
{
    public class ConsumerRolesEntityConfiguration:IEntityTypeConfiguration<ConsumerRoles>
    {
        public virtual void Configure(EntityTypeBuilder<ConsumerRoles> builder)
        {
            builder.HasKey(x => x.ConsumerRolesId);
        }
    }
}
