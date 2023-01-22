using Business.BusinessAspects;
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

namespace Business.Handlers.Orders.Queries
{
    public class GetOrderQuery:IRequest<IDataResult<Order>>
    {
        public int OrderId { get; set; }

        public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery,IDataResult<Order>>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMediator _mediator;

            public GetOrderQueryHandler(IOrderRepository orderRepository, IMediator mediator)
            {
                _orderRepository = orderRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Order>>Handle(GetOrderQuery request, CancellationToken cancellationToken)
            {
                var order=await _orderRepository.GetAsync(o=>o.OrderId==request.OrderId);
                return new SuccessDataResult<Order>(order);
            }

        }
    }
}
