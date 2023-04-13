using PizzApp.Application.Addresses.Requests;
using PizzApp.Application.Addresses.Responses;
using PizzApp.Application.Pizzas.Requests;
using PizzApp.Application.Pizzas.Responses;

namespace PizzApp.Application.Pizzas;

public interface IPizzaService
{
    Task<List<PizzaResponseModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<PizzaResponseModel> GetAsync(CancellationToken cancellationToken, int id);
    Task CreateAsync(CancellationToken cancellationToken, PizzaRequestModel pizza);
    Task UpdateAsync(CancellationToken cancellationToken, PizzaRequestModel pizza, int id);
    Task DeleteAsync(CancellationToken cancellationToken, int id);
}
