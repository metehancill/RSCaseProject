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

namespace Business.Handlers.Storages.Queries
{
    public class GetStorageQuery:IRequest<IDataResult<Storage>>
    {
        public int StorageId { get; set; }

        public class GetStorageQueryHandler : IRequestHandler<GetStorageQuery, IDataResult<Storage>>
        {
            private readonly IStorageRepository _storageRepository;
            private readonly IMediator _mediator;

            public GetStorageQueryHandler(IStorageRepository storageRepository, IMediator mediator)
            {
                _storageRepository = storageRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority=1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Storage>>Handle(GetStorageQuery request,CancellationToken cancellationToken)
            {
                var storage=await _storageRepository.GetAsync(s=>s.StorageId==request.StorageId);
                return new SuccessDataResult<Storage>(storage); 
            }
        }
    }
}
