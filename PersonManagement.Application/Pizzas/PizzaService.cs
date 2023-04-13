using Mapster;
using PizzApp.Application.Pizzas.Requests;
using PizzApp.Application.Pizzas.Responses;
using PizzApp.Domain.Pizzas;
using PizzApp.Infrastructure.Pizzas;
using PizzApp.Infrastructure.RankHistories;
using System.Threading;

namespace PizzApp.Application.Pizzas
{
    public class PizzaService : IPizzaService
    {
        private IPizzaRepository _repo;
        private IRankHistoryRepository _rankRepo;

        public PizzaService(IPizzaRepository repo, IRankHistoryRepository rankRepo)
        {
            _repo = repo;
            _rankRepo = rankRepo;
        }

        public async Task<PizzaResponseModel> GetAsync(CancellationToken cancellationToken,int id)
        {
            var result = await _repo.GetByIdAsync(cancellationToken,id);

            if (result == null)
                throw new Exception("Not Found"); 
                                                  
            
            var  pizza =   result.Adapt<PizzaResponseModel>();
            pizza.Rank = await _rankRepo.AverageRankCalculator(cancellationToken, id);
            return pizza;
    }

    public async Task<List<PizzaResponseModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllAsync(cancellationToken);

            if (result == null)
                throw new Exception("chemi eroir");

            var responseModel = result.Adapt<List<PizzaResponseModel>>();
            for (int i = 0; i < responseModel.Count; i++)
            {
                responseModel.ElementAt(i).Rank = await _rankRepo.AverageRankCalculator(cancellationToken, responseModel.ElementAt(i).Id);
            }
            return result.Adapt<List<PizzaResponseModel>>();
        }

        public async Task CreateAsync(CancellationToken cancellationToken,PizzaRequestModel pizza)
        {
            var personToInsert = pizza.Adapt<Pizza>();

            await _repo.CreateAsync(cancellationToken,personToInsert);
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, PizzaRequestModel pizza, int id)
        {
            if (!await _repo.Exists(cancellationToken,id))
                throw new Exception("Not Found"); 
                                                  

            var personToUpdate = pizza.Adapt<Pizza>();

            await _repo.UpdateAsync(cancellationToken,personToUpdate,id);
        }

        public async Task DeleteAsync(CancellationToken cancellationToken, int id)
        {
            if (!await _repo.Exists(cancellationToken,id))
                throw new Exception("Not Found");
                                                 

            await _repo.DeleteAsync(cancellationToken,id);
        
        }

    }
}
