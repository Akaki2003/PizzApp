
using PizzApp.Domain.Orders;

namespace PersonManagement.Infrastructure.Orders
{
  public  interface  IOrderRepository
    {
        Task<List<Order>> GetAllAsync(CancellationToken cancellationToken);
        Task<Order> GetByIdAsync(CancellationToken cancellationToken, int id);
        Task CreateAsync(CancellationToken cancellationToken, Order order);
        Task<bool> Exists(CancellationToken cancellationToken, int userId,int pizzaId);
    }
}
