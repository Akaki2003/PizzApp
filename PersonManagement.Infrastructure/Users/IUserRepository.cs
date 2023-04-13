
using PizzApp.Domain.Users;

namespace PizzApp.Infrastructure.Users
{
  public  interface  IUserRepository
    {
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
        Task<User> GetByIdAsync(CancellationToken cancellationToken, int id);
        Task CreateAsync(CancellationToken cancellationToken, User user);
        Task UpdateAsync(CancellationToken cancellationToken, User user);
        Task DeleteAsync(CancellationToken cancellationToken, int id);
        Task<bool> Exists(CancellationToken cancellationToken, int id);
    }
}
