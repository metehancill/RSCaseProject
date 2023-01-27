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
    public class ProductRepository : EfEntityRepositoryBase<Product, ProjectDbContext>,IProductRepository
    {
        public ProductRepository(ProjectDbContext context) : base(context)
        {


        }

        public async Task<List<SelectionItem>> GetProductsLookUp()
        {
            var lookUp = await (from entity in Context.Products
                                where !(entity.isDeleted)
                                select new SelectionItem()
                                {
                                    Id = entity.ProductId,
                                    Label = entity.ProductName
                                }).ToListAsync();
            return lookUp;
        }
    }
}
