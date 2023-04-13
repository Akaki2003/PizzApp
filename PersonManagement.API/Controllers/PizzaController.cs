using Microsoft.AspNetCore.Mvc;
using PizzApp.Application.Pizzas;
using PizzApp.Application.Pizzas.Requests;
using PizzApp.Application.Pizzas.Responses;

namespace PizzApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _service;

        public PizzaController(IPizzaService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(CancellationToken cancellationToken, int id)
        {
            return Ok(await _service.GetAsync(cancellationToken,id));
        }

        [HttpGet]
        public async Task<List<PizzaResponseModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }

        [HttpPost]
        public async Task Post(CancellationToken cancellationToken,PizzaRequestModel request)
        {
            await _service.CreateAsync(cancellationToken,request);
        }

        [HttpPut]
        public async Task Put(CancellationToken cancellationToken, PizzaRequestModel request,int id)
        {
            await _service.UpdateAsync(cancellationToken, request, id);
        }

        [HttpDelete("{id}")]
        public async Task Delete(CancellationToken cancellationToken, int id)
        {
            await _service.DeleteAsync(cancellationToken, id);
        }
    }
}
