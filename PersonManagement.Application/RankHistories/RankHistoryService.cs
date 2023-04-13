using Mapster;
using PersonManagement.Infrastructure.Orders;
using PizzApp.Application.RankHistories.Requests;
using PizzApp.Application.RankHistories.Responses;
using PizzApp.Domain.Orders;
using PizzApp.Domain.RankHistories;
using PizzApp.Infrastructure.Pizzas;
using PizzApp.Infrastructure.RankHistories;
using PizzApp.Infrastructure.Users;

namespace PizzApp.Application.RankHistories
{
    public class RankHistoryService : IRankHistoryService
    {
        private IRankHistoryRepository _repo;
        private IOrderRepository _orderRepo;
        private IPizzaRepository _pizzaRepo;
        private IUserRepository _userRepo;

        public RankHistoryService(IRankHistoryRepository repo, IOrderRepository orderRepo, IPizzaRepository pizzaRepo, IUserRepository userRepo)
        {
            _repo = repo;
            _orderRepo = orderRepo;
            _pizzaRepo = pizzaRepo;
            _userRepo = userRepo;
        }

        public async Task<RankHistoryResponseModel> GetAsync(CancellationToken cancellationToken,int id)
        {
            var result = await _repo.GetByIdAsync(cancellationToken,id);

            if (result == null)
                throw new Exception("Not Found"); 
                                                  
            else
                return result.Adapt<RankHistoryResponseModel>();
        }

        public async Task<List<RankHistoryResponseModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllAsync(cancellationToken);

            return result.Adapt<List<RankHistoryResponseModel>>();
        }


        public async Task CreateAsync(CancellationToken cancellationToken,RankHistoryRequestModel rankHistory)
        {
            if(!await _pizzaRepo.Exists(cancellationToken, rankHistory.PizzaId))
            {
                throw new Exception("There was conflict while providing PizzaId");
            }
            if(!await _userRepo.Exists(cancellationToken, rankHistory.UserId))
            {
                throw new Exception("There was conflict while providing UserId");
            }

            if (!await _orderRepo.Exists(cancellationToken, rankHistory.UserId,rankHistory.PizzaId))
            {
                throw new Exception("There was conflict while providing userId and PizzaId"); 
            }
            
            var rankHistoryToInsert = rankHistory.Adapt<RankHistory>();

            await _repo.CreateAsync(cancellationToken, rankHistoryToInsert);
        }

        public async Task<int> AverageRank(CancellationToken cancellationToken, int pizzaId)
        {
            if(!await _pizzaRepo.Exists(cancellationToken, pizzaId))
            {
                throw new Exception("Entered pizza doesn't exist"); 
            }
            var average = await _repo.AverageRankCalculator(cancellationToken, pizzaId);
            return average;
        }

  
    }
}
