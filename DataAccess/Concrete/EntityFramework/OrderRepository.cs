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
    public class OrderRepository : EfEntityRepositoryBase<Order, ProjectDbContext>, IOrderRepository
    {
        public OrderRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<OrderDto>> GetOrderDto()
        {
            var list=await (from ordr in Context.Orders 

                            join prdt in Context.Products on ordr.ProductId equals prdt.ProductId
                            join cstm in Context.Customers on ordr.CustomerId equals cstm.customerId
                            select new OrderDto()
                            {   
                                orderId= ordr.OrderId,
                                productName=prdt.ProductName,
                                customerName=cstm.CustomerName,
                                piece=ordr.Piece,
                                isDeleted=ordr.isDeleted,


                            }).ToListAsync();

            return list;
   
        }
    }
}
