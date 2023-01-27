
using Business.Handlers.Customers.Commands;
using Business.Handlers.Customers.Queries;
using Business.Handlers.Languages.Queries;
using Business.Handlers.Users.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseApiController
    {
        /// <summary>
        /// Add Customer.
        /// </summary>
        /// <param name="createCustomer"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCustomerCommand createCustomer)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createCustomer));
        }

        /// <summary>
        /// Update Customer.
        /// </summary>
        /// <param name="updateCustomer"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand updateCustomer)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(updateCustomer));
        }
        /// <summary>
        /// Delete Customer.
        /// </summary>
        /// <param name="deleteCustomer"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCustomerCommand deleteCustomer)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(deleteCustomer));
        }

        /// <summary>
        /// List Customers
        /// </summary>
        /// <remarks>bla bla bla Customers</remarks>
        /// <return>Customers List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Customer>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetCustomersQuery()));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>Customer List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int customerId)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetCustomerQuery { customerId = customerId }));
        }

        /// <summary>
        ///  Customer Lookup
        /// </summary>
        /// <remarks>bla bla bla Customers</remarks>
        /// <return>Users List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getcustomerlookup")]
        public async Task<IActionResult> GetCustomerLookup()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetCustomerLookupQuery()));
        }

    }
}
