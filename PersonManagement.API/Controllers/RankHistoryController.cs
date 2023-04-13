using Microsoft.AspNetCore.Mvc;
using PizzApp.Application.Addresses.Requests;
using PizzApp.Application.Pizzas;
using PizzApp.Application.Pizzas.Requests;
using PizzApp.Application.Pizzas.Responses;
using PizzApp.Application.RankHistories;
using PizzApp.Application.RankHistories.Requests;
using PizzApp.Application.RankHistories.Responses;

namespace PizzApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankHistoryController : ControllerBase
    {
        private readonly IRankHistoryService _service;

        public RankHistoryController(IRankHistoryService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(CancellationToken cancellationToken, int id)
        {
            return Ok(await _service.GetAsync(cancellationToken,id));
        }


        [HttpGet]
        public async Task<List<RankHistoryResponseModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }


        [HttpPost]
        public async Task Post(CancellationToken cancellationToken, RankHistoryRequestModel request)
        {
            await _service.CreateAsync(cancellationToken, request);
        }
      

    }
}
