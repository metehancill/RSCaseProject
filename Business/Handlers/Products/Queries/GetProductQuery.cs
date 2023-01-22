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

namespace Business.Handlers.Products.Queries
{
    public class GetProductQuery:IRequest<IDataResult<Product>>
    {
        public int ProductId { get; set; }

        public class GetProductQueryHandler : IRequestHandler<GetProductQuery,IDataResult<Product>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;

            public GetProductQueryHandler(IProductRepository productRepository, IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Product>>Handle(GetProductQuery request,CancellationToken cancellationToken)
            {
                var product=await _productRepository.GetAsync(p=>p.ProductId==request.ProductId);
                return new SuccessDataResult<Product>(product);
            }

        }
    }
}
