using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Orders.Commands
{
    public class CreateOrderCommand:IRequest<IResult>
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Piece { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool isDeleted { get; set; }

        public class CreateOrderCommandHandler:IRequestHandler<CreateOrderCommand,IResult> 
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IStorageRepository _storageRepository;

            public CreateOrderCommandHandler(IOrderRepository orderRepository,IStorageRepository storageRepository )
            {
                _orderRepository = orderRepository;
                _storageRepository = storageRepository;
               
            }
            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult>Handle(CreateOrderCommand request,CancellationToken cancellationToken)
            {
                var isThereAnyOrder=await _orderRepository.GetAsync(o=>o.OrderId==request.OrderId);
                var isThereAnyStorage = await _storageRepository.GetAsync(p => p.ProductId == request.ProductId);

                if(isThereAnyOrder !=null)
                {
                    return new ErrorResult(Messages.AlreadyExist);
                }

                if(Convert.ToInt32(isThereAnyStorage.ProductStock) < Convert.ToInt32(request.Piece))
                {
                    return new ErrorResult(Messages.InvalidStock);

                }
                else
                {
                    isThereAnyStorage.ProductStock -= Convert.ToInt32(request.Piece);
                   
                }
               
                var order = new Order
                {
                 CustomerId=request.CustomerId,
                 ProductId=request.ProductId,
                 Piece=request.Piece,
                 CreatedDate=request.CreatedDate,
                 CreatedUserId = request.CreatedUserId,
                 isDeleted = false
                };

                _orderRepository.Add(order);
                await _orderRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);





            }
        }


    }
}
