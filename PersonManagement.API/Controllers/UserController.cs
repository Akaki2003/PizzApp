using Microsoft.AspNetCore.Mvc;
using PizzApp.Application.Pizzas;
using PizzApp.Application.Pizzas.Requests;
using PizzApp.Application.Pizzas.Responses;
using PizzApp.Application.Users;
using PizzApp.Application.Users.Requests;
using PizzApp.Application.Users.Responses;

namespace PizzApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(CancellationToken cancellationToken, int id)
        {
            return Ok(await _service.GetAsync(cancellationToken,id));
        }

        [HttpGet]
        public async Task<List<UserResponseModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }

        [HttpPost]
        public async Task Post(CancellationToken cancellationToken,UserRequestModel request)
        {
            await _service.CreateAsync(cancellationToken,request);
        }

        [HttpPut]
        public async Task Put(CancellationToken cancellationToken, UserRequestModel request)
        {
            await _service.UpdateAsync(cancellationToken, request);
        }

        [HttpDelete("{id}")]
        public async Task Delete(CancellationToken cancellationToken, int id)
        {
            await _service.DeleteAsync(cancellationToken, id);
        }
    }
}
