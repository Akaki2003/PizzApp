
using PizzApp.Domain.Addresses;

namespace PersonManagement.Infrastructure.Addresses
{
  public  interface  IAddressRepository
    {
        Task<List<Address>> GetAllAsync(CancellationToken cancellationToken);
        Task<Address> GetByIdAsync(CancellationToken cancellationToken, int id);
        Task<List<Address>> GetAllByUserIdAsync(CancellationToken cancellationToken, int userId);

        Task CreateAsync(CancellationToken cancellationToken, Address address);
        Task UpdateAsync(CancellationToken cancellationToken, Address address);
        Task DeleteAsync(CancellationToken cancellationToken, int id);
        Task<bool> Exists(CancellationToken cancellationToken, int id);
    }
}
