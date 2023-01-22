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
    public class GetProductsQuery : IRequest<IDataResult<IEnumerable<Product>>>
    {
        public class GetProducstQueryHandler:IRequestHandler<GetProductsQuery,IDataResult<IEnumerable<Product>>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;

            public GetProducstQueryHandler(IProductRepository productRepository, IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;

            }

            [SecuredOperation(Priority=1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Product>>>Handle(GetProductsQuery request,CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Product>>(await _productRepository.GetListAsync());
            }
        }
    }
}
