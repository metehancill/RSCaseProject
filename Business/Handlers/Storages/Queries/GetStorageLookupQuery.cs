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

namespace Business.Handlers.Storages.Queries
{
    public class GetStorageLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public class GetStorageLookupQueryHandler : IRequestHandler<GetStorageLookupQuery, IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IStorageRepository _storageRepository;
            private readonly IMediator _mediator;
            public GetStorageLookupQueryHandler(IStorageRepository storageRepository, IMediator mediator)
            {
                _storageRepository = storageRepository;
                _mediator = mediator;

            }

            [SecuredOperation(Priority = 1)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]

            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetStorageLookupQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SelectionItem>>(await _storageRepository.GetStorageLookUp());
            }
        }
    }
}