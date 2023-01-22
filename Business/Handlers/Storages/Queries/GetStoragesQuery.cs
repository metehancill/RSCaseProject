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
    public class GetStoragesQuery:IRequest<IDataResult<IEnumerable<Storage>>>
    {
        public class GetStoragesQueryHandler : IRequestHandler<GetStoragesQuery, IDataResult<IEnumerable<Storage>>>
        {
            private readonly IStorageRepository _storageRepository;
            private readonly IMediator _mediator;

            public GetStoragesQueryHandler(IStorageRepository storageRepository, IMediator mediator)
            {
                _storageRepository = storageRepository;
                _mediator = mediator;
            }


            [SecuredOperation(Priority=1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Storage>>>Handle(GetStoragesQuery request,CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Storage>>(await _storageRepository.GetListAsync());
            }
        }
    }
}
