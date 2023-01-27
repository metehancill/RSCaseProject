using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class CustomerRepository : EfEntityRepositoryBase<Customer, ProjectDbContext>,ICustomerRepository
    {
        public CustomerRepository(ProjectDbContext context) : base(context)
        {

        }

        public async Task<List<SelectionItem>> GetCustomersLookUp()
        {
            var lookUp = await (from entity in Context.Customers
                                where !(entity.isDeleted)
                                select new SelectionItem()
                                {
                                    Id = entity.customerId,
                                    Label = entity.CustomerName
                                }).ToListAsync();
            return lookUp;
        }
    }
}
