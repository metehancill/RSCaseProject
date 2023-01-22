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

namespace Business.Handlers.Products.Commands
{
    public class DeleteProductCommand:IRequest<IResult>

    {
        public int ProductId { get; set;}
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }

        public class DeleteProductCommandHandler :IRequestHandler<DeleteProductCommand,IResult> 
        {
            private readonly IProductRepository _productRepository;
            
            public DeleteProductCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult>Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var productToDelete=_productRepository.Get(p=>p.ProductId==request.ProductId);

                productToDelete.isDeleted = true;
                productToDelete.LastUpdatedUserId = request.LastUpdatedUserId;
                productToDelete.LastUpdatedDate= request.LastUpdatedDate;
                _productRepository.Update(productToDelete);
                await _productRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        
        
        
        
        
        
        
        
        
        }
    }
}
