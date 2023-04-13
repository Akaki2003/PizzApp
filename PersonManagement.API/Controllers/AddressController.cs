using Microsoft.AspNetCore.Mvc;
using PizzApp.Application.Addresses;
using PizzApp.Application.Addresses.Requests;
using PizzApp.Application.Addresses.Responses;
using PizzApp.Application.Pizzas;
using PizzApp.Application.Pizzas.Requests;
using PizzApp.Application.Pizzas.Responses;

namespace PizzApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(CancellationToken cancellationToken, int id)
        {
            return Ok(await _service.GetAsync(cancellationToken,id));
        }

        [HttpGet]
        public async Task<List<AddressResponseModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }

        [HttpPost]
        public async Task Post(CancellationToken cancellationToken,AddressRequestModel request)
        {
            await _service.CreateAsync(cancellationToken,request);
        }

        [HttpPut]
        public async Task Put(CancellationToken cancellationToken, AddressRequestModel request,int id)
        {
            await _service.UpdateAsync(cancellationToken, request,id);
        }

        [HttpDelete("{id}")]
        public async Task Delete(CancellationToken cancellationToken, int id)
        {
            await _service.DeleteAsync(cancellationToken, id);
        }
    }
}
