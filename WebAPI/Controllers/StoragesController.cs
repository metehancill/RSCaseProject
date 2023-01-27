using Business.Handlers.Products.Queries;
using Business.Handlers.Storages.Commands;
using Business.Handlers.Storages.Queries;
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
    public class StoragesController : BaseApiController
    {
        /// <summary>
        /// Add Storage.
        /// </summary>
        /// <param name="createStorage"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateStorageCommand createStorage)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createStorage));
        }

        /// <summary>
        /// Update Storage.
        /// </summary>
        /// <param name="updateStorage"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStorageCommand updateStorage)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(updateStorage));
        }
        /// <summary>
        /// Delete Storage.
        /// </summary>
        /// <param name="deleteStorage"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteStorageCommand deleteStorage)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(deleteStorage));
        }

        /// <summary>
        /// List Product storage
        /// </summary>
        /// <remarks>bla bla bla Product Storage</remarks>
        /// <return>Product Storage List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Storage>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetStoragesQuery()));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>Storage List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Storage))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int storagesId)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetStorageQuery { StorageId = storagesId }));
        }
        /// <summary>
        /// List Product storageDto
        /// </summary>
        /// <remarks>bla bla bla Product Storage</remarks>
        /// <return>Product Storage List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Storage>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getstoragelistdto")]
        public async Task<IActionResult> GetStorageListDto()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetStorageListDtoQuery()));
        }
        /// <summary>
        ///  Storage Lookup
        /// </summary>
        /// <remarks>bla bla bla Storage</remarks>
        /// <return>Storage List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getstoragelookup")]
        public async Task<IActionResult> GetStorageLookup()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetStorageLookupQuery()));
        }
    }
}
