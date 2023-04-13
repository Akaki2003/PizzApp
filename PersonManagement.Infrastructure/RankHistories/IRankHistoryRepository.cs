
using PizzApp.Domain.Pizzas;
using PizzApp.Domain.RankHistories;

namespace PizzApp.Infrastructure.RankHistories
{
  public  interface  IRankHistoryRepository
    {
        Task<List<RankHistory>> GetAllAsync(CancellationToken cancellationToken);
        Task<RankHistory> GetByIdAsync(CancellationToken cancellationToken, int id);
        Task CreateAsync(CancellationToken cancellationToken, RankHistory rankHistory);
        Task<int> AverageRankCalculator(CancellationToken cancellationToken, int pizzaId);


        //Task<int> AverageRankCalculator(CancellationToken cancellationToken, int pizzaId);


    }
}
