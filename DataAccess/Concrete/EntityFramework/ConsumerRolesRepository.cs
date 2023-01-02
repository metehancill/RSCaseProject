using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class ConsumerRolesRepository : EfEntityRepositoryBase<ConsumerRoles, ProjectDbContext>
    {
        public ConsumerRolesRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
