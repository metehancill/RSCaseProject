using Business.BusinessAspects;

using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Business.Handlers.Products.Queries
{
    public class GetProductLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public class GetProductLookupQueryHandler : IRequestHandler<GetProductLookupQuery, IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;
            public GetProductLookupQueryHandler(IProductRepository productRepository, IMediator mediator)
            {
               _productRepository= productRepository;
                _mediator = mediator;

            }

            [SecuredOperation(Priority = 1)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]

            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetProductLookupQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SelectionItem>>(await _productRepository.GetProductsLookUp());
            }
        }
    }
}
