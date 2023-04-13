
using PizzApp.Application.Orders.Requests;
using PizzApp.Application.Orders.Responses;

namespace PizzApp.Application.Orders;

public interface IOrderService
{
    Task<List<OrderResponseModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<OrderResponseModel> GetAsync(CancellationToken cancellationToken, int id);
    Task CreateAsync(CancellationToken cancellationToken, OrderRequestModel order);
}
