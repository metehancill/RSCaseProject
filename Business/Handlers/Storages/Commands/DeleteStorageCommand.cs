using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Storages.Commands
{
    public class DeleteStorageCommand:IRequest<IResult>
    {
        public int StorageId { get; set; }

        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }

        public class DeleteStorageCommandHandler:IRequestHandler<DeleteStorageCommand,IResult>
        {
            private readonly IStorageRepository _storageRepository;

            public DeleteStorageCommandHandler(IStorageRepository storageRepository)
            {
                _storageRepository = storageRepository;
            }
            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult>Handle(DeleteStorageCommand request,CancellationToken cancellationToken)
            {
                var storageToDelete = _storageRepository.Get(s => s.StorageId == request.StorageId);
                
                storageToDelete.isDeleted=true;
                storageToDelete.LastUpdatedDate= request.LastUpdatedDate;
                storageToDelete.LastUpdatedUserId = request.LastUpdatedUserId;

                _storageRepository.Update(storageToDelete);
                await _storageRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
