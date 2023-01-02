using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace DataAccess.Concrete.Configurations
{
    public class ConsumerEntityConfiguration : IEntityTypeConfiguration<Consumer>
    {
        public virtual void Configure(EntityTypeBuilder<Consumer> builder)
        {
            builder.HasKey(x => x.ConsumerId);
        }
    }
}

