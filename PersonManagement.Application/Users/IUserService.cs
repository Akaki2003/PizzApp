
using PizzApp.Application.Users.Requests;
using PizzApp.Application.Users.Responses;

namespace PizzApp.Application.Users;

public interface IUserService
{
    Task<List<UserResponseModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<UserResponseModel> GetAsync(CancellationToken cancellationToken, int id);
    Task CreateAsync(CancellationToken cancellationToken, UserRequestModel user);
    Task UpdateAsync(CancellationToken cancellationToken, UserRequestModel user);
    Task DeleteAsync(CancellationToken cancellationToken, int id);
}
