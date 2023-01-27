

using Business.Handlers.Customers.Queries;
using Business.Handlers.Products.Commands;
using Business.Handlers.Products.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        /// <summary>
        /// Add Product.
        /// </summary>
        /// <param name="createProduct"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProductCommand createProduct)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createProduct));
        }

        /// <summary>
        /// Update Product.
        /// </summary>
        /// <param name="updateProduct"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand updateProduct)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(updateProduct));
        }
        /// <summary>
        /// Delete Product.
        /// </summary>
        /// <param name="deleteProduct"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProductCommand deleteProduct)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(deleteProduct));
        }

        /// <summary>
        /// List Products
        /// </summary>
        /// <remarks>bla bla bla Products</remarks>
        /// <return>Products List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetProductsQuery()));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>Product List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int productsId)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetProductQuery { ProductId = productsId }));
        }

        /// <summary>
        ///  Product Lookup
        /// </summary>
        /// <remarks>bla bla bla Products</remarks>
        /// <return>Products List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getproductlookup")]
        public async Task<IActionResult> GetProductLookup()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetProductLookupQuery()));
        }

    }
}
