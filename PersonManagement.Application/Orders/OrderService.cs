using Mapster;
using PersonManagement.Infrastructure.Orders;
using PizzApp.Application.Orders;
using PizzApp.Application.Orders.Requests;
using PizzApp.Application.Orders.Responses;
using PizzApp.Domain.Orders;
using PizzApp.Infrastructure.Pizzas;
using PizzApp.Infrastructure.Users;
using System.Threading;

namespace PizzApp.Application.Orders
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _repo;
        private IPizzaRepository _pizzaRepo;
        private IUserRepository _userRepo;

        public OrderService(IOrderRepository repo, IPizzaRepository pizzaRepo, IUserRepository userRepo )
        {
            _repo = repo;
            _pizzaRepo = pizzaRepo;
            _userRepo = userRepo;
        }

        public async Task<OrderResponseModel> GetAsync(CancellationToken cancellationToken,int id)
        {
            var result = await _repo.GetByIdAsync(cancellationToken,id);
            var res = result.Adapt<OrderResponseModel>();

            if (result == null)
                throw new Exception("Not Found");

            else
                return result.Adapt<OrderResponseModel>();
        }

        public async Task<List<OrderResponseModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllAsync(cancellationToken);

            return result.Adapt<List<OrderResponseModel>>();
        }

        public async Task CreateAsync(CancellationToken cancellationToken,OrderRequestModel order) //PizzaId instead of PIzza i guess
        {
            if(!await _userRepo.Exists(cancellationToken, order.UserId))
            {
                throw new Exception(); //localisation
            }
            if (!await _pizzaRepo.Exists(cancellationToken, order.PizzaId))
            {
                throw new Exception(); //localisation
            }

            var orderToInsert = order.Adapt<Order>();

            await _repo.CreateAsync(cancellationToken,orderToInsert);
        }

        

    }
}
