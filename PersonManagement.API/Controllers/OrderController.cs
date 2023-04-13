using Microsoft.AspNetCore.Mvc;
using PizzApp.Application.Orders;
using PizzApp.Application.Orders.Requests;
using PizzApp.Application.Orders.Responses;
using PizzApp.Application.Pizzas;
using PizzApp.Application.Pizzas.Requests;
using PizzApp.Application.Pizzas.Responses;

namespace PizzApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(CancellationToken cancellationToken, int id)
        {
            return Ok(await _service.GetAsync(cancellationToken,id));
        }

        [HttpGet]
        public async Task<List<OrderResponseModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }

        [HttpPost]
        public async Task Post(CancellationToken cancellationToken,OrderRequestModel request)
        {
            await _service.CreateAsync(cancellationToken,request);
        }
    }
}
