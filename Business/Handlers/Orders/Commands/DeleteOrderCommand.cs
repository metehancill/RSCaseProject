using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Orders.Commands
{
    public class DeleteOrderCommand:IRequest<IResult>
    {
        public int OrderId { get; set;}
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }


        public class DeleteOrderCommandHandler:IRequestHandler<DeleteOrderCommand,IResult>
        {
            private readonly IOrderRepository _orderRepository;

            public DeleteOrderCommandHandler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }
            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult>Handle(DeleteOrderCommand request,CancellationToken cancellationToken)
            {
                var orderToDelete = _orderRepository.Get(o => o.OrderId == request.OrderId);

                orderToDelete.isDeleted = true;
                orderToDelete.LastUpdatedDate = request.LastUpdatedDate;
                orderToDelete.LastUpdatedUserId = request.LastUpdatedUserId;
                _orderRepository.Update(orderToDelete);
                await _orderRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);



            }


        }

    }
}
