using PizzApp.Application.Addresses.Requests;
using PizzApp.Application.Addresses.Responses;

namespace PizzApp.Application.Addresses;

public interface IAddressService
{
    Task<List<AddressResponseModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<AddressResponseModel> GetAsync(CancellationToken cancellationToken, int id);
    Task CreateAsync(CancellationToken cancellationToken, AddressRequestModel address);
    Task UpdateAsync(CancellationToken cancellationToken, AddressRequestModel address,int id);
    Task DeleteAsync(CancellationToken cancellationToken, int id);
}
