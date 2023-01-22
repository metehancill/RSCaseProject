using Business.BusinessAspects;
using Business.Constants;
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

namespace Business.Handlers.Storages.Commands
{
    public class CreateStorageCommand:IRequest<IResult>
    {
        public int StorageId { get; set; }
        public int ProductId { get; set; }
        public int ProductStock { get; set; }
        public bool IsReady { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public class CreateStorageCommandHandler:IRequestHandler<CreateStorageCommand,IResult> 
        {
            private readonly IStorageRepository _storageRepository;

            public CreateStorageCommandHandler(IStorageRepository storageRepository)
            {
                _storageRepository = storageRepository;
            }
            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult>Handle(CreateStorageCommand request,CancellationToken cancellationToken)
            {
                var isThereAnyStorage=await _storageRepository.GetAsync(s=>s.ProductId==request.ProductId);
                
                if (isThereAnyStorage != null)
                {
                    return new ErrorResult(Messages.AlreadyExist);
                }
                var storage = new Storage
                {
                    StorageId = request.StorageId,
                    ProductId = request.ProductId,
                    ProductStock=request.ProductStock,
                    CreatedDate = request.CreatedDate,
                    CreatedUserId = request.CreatedUserId,
                    IsReady= request.IsReady,

                };

                _storageRepository.Add(storage);
                await _storageRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);


            }

        }
    }
}
