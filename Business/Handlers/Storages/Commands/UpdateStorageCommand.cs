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
    public class UpdateStorageCommand:IRequest<IResult>
    {
        public int StorageId { get; set; }
        public int ProductId { get; set; }
        public int ProductStock { get; set; }
        public bool IsReady { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }

        public class UpdateStorageCommandHandler:IRequestHandler<UpdateStorageCommand,IResult> 
        {
            private readonly IStorageRepository _storageRepository;

            public UpdateStorageCommandHandler(IStorageRepository storageRepository)
            {
                _storageRepository = storageRepository;
            }
            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult>Handle(UpdateStorageCommand request,CancellationToken cancellationToken)
            {
                var isThereAnyStorage=await _storageRepository.GetAsync(s=>s.StorageId==request.StorageId);

                isThereAnyStorage.ProductId=request.ProductId;
                isThereAnyStorage.ProductStock=request.ProductStock;
                isThereAnyStorage.IsReady=request.IsReady;
                isThereAnyStorage.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereAnyStorage.LastUpdatedDate=request.LastUpdatedDate;

                _storageRepository.Update(isThereAnyStorage);
                await _storageRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);

            }
        }
    }
}
