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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Customers.Queries
{
    public class GetCustomerLookupQuery:IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public class GetCustomerLookupQueryHandler : IRequestHandler<GetCustomerLookupQuery, IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;
            public GetCustomerLookupQueryHandler(ICustomerRepository customerRepository, IMediator mediator)
            {
                _customerRepository = customerRepository;
                _mediator = mediator;

            }

            [SecuredOperation(Priority=1)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]

            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetCustomerLookupQuery request,CancellationToken cancellationToken)
            {
              return new SuccessDataResult<IEnumerable<SelectionItem>>( await _customerRepository.GetCustomersLookUp());
            }
        }
    }
}
