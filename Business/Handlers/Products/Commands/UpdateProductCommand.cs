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
    public class UpdateProductCommand:IRequest<IResult>
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductColor { get; set; }
        public string ProductSize { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }

        public class UpdateProductCommandHandler:IRequestHandler<UpdateProductCommand,IResult> 
        {
            private readonly IProductRepository _productRepository;

            public UpdateProductCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult>Handle(UpdateProductCommand request,CancellationToken cancellationToken)
            {
                var isThereAnyProduct=await _productRepository.GetAsync(p=> p.ProductId==request.ProductId);

                isThereAnyProduct.ProductName=request.ProductName;
                isThereAnyProduct.ProductColor=request.ProductColor;
                isThereAnyProduct.ProductSize=request.ProductSize;
                isThereAnyProduct.LastUpdatedDate=request.LastUpdatedDate;
                isThereAnyProduct.LastUpdatedUserId = request.LastUpdatedUserId;


                _productRepository.Update(isThereAnyProduct);
                await _productRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);


            }




        }



    }
}
