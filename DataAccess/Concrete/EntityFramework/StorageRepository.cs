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
    public class StorageRepository : EfEntityRepositoryBase<Storage, ProjectDbContext>,IStorageRepository
    {
        public StorageRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<StorageDto>> GetStorageDto()
        {
            var list = await(from str in Context.Storages

                             join prd in Context.Products on str.ProductId equals prd.ProductId

                             select new StorageDto()
                             {
                                 storageId = str.StorageId,
                                 productName = prd.ProductName,
                                 ProductStock = str.ProductStock,
                                 IsReady = str.IsReady,
                                 isDeleted=str.isDeleted
                             }).ToListAsync();

            return list;
        }

        public async Task<List<SelectionItem>> GetStorageLookUp()
        {

            var lookUp = await(from entity in Context.Storages
                               join prd in Context.Products on entity.ProductId equals prd.ProductId
                               where !(entity.isDeleted)
                               select new SelectionItem()
                               {
                                   Id = entity.StorageId,
                                   Label = prd.ProductName
                               }).ToListAsync();
            return lookUp;
        }
    }
}
