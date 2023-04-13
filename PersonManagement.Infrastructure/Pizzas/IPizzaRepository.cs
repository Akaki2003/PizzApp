
using PizzApp.Domain.Pizzas;

namespace PizzApp.Infrastructure.Pizzas
{
  public  interface  IPizzaRepository
    {
        Task<List<Pizza>> GetAllAsync(CancellationToken cancellationToken);
        Task<Pizza> GetByIdAsync(CancellationToken cancellationToken, int id);
        Task CreateAsync(CancellationToken cancellationToken, Pizza pizza);
        Task UpdateAsync(CancellationToken cancellationToken, Pizza pizza, int id);
        Task DeleteAsync(CancellationToken cancellationToken, int id);
        Task<bool> Exists(CancellationToken cancellationToken, int id);
    }
}
