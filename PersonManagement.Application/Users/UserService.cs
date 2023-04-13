using Mapster;
using PersonManagement.Infrastructure.Addresses;
using PizzApp.Application.Addresses.Responses;
using PizzApp.Application.Users;
using PizzApp.Application.Users.Requests;
using PizzApp.Application.Users.Responses;
using PizzApp.Domain.Users;
using PizzApp.Infrastructure.Users;
using System.Threading;

namespace PizzApp.Application.Users
{
    public class UserService : IUserService
    {
        private IUserRepository _repo;
        private IAddressRepository _addressRepo;

        public UserService(IUserRepository repo,IAddressRepository addressRepo)
        {
            _repo = repo;
            _addressRepo = addressRepo;
        }

        public async Task<UserResponseModel> GetAsync(CancellationToken cancellationToken,int id)
        {
            var result = await _repo.GetByIdAsync(cancellationToken,id);

            if (result == null)
                throw new Exception("Not Found"); 
                                                  
            
            var userResponseModel =  result.Adapt<UserResponseModel>();
            var addrresses = await _addressRepo.GetAllByUserIdAsync(cancellationToken, userResponseModel.Id);


            if (addrresses.Count > 0)
            {
                userResponseModel.Addresses = new List<AddressResponseModel>();
                foreach (var item in addrresses)
                {
                    userResponseModel.Addresses.Add(item.Adapt<AddressResponseModel>());
                }
            }
            return userResponseModel;

        }

        public async Task<List<UserResponseModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllAsync(cancellationToken);
            if (result == null)
            {
                throw new Exception("Item was not found");
            }
            var users = result.Adapt<List<UserResponseModel>>();

            foreach (var user in users)
            {
                var addresses = await _addressRepo.GetAllByUserIdAsync(cancellationToken, user.Id);
                if (addresses.Count > 0)
                {
                    user.Addresses = new List<AddressResponseModel>();
                    user.Addresses.AddRange(addresses.Adapt<List<AddressResponseModel>>());
                }
            }
            return result.Adapt<List<UserResponseModel>>();


        }

        public async Task CreateAsync(CancellationToken cancellationToken,UserRequestModel user)
        {
            var userToInsert = user.Adapt<User>();

            await _repo.CreateAsync(cancellationToken, userToInsert);
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, UserRequestModel user)
        {
            if (!await _repo.Exists(cancellationToken, user.Id))
                throw new Exception("Not Found"); 
                                                  

            var userToUpdate = user.Adapt<User>();

            await _repo.UpdateAsync(cancellationToken, userToUpdate);
        }

        public async Task DeleteAsync(CancellationToken cancellationToken, int id)
        {
            if (!await _repo.Exists(cancellationToken, id))
                throw new Exception("Not Found");

            var addressList = await _addressRepo.GetAllByUserIdAsync(cancellationToken, id);

            await _repo.DeleteAsync(cancellationToken, id);

            if (addressList.Count > 0)
            {
                foreach (var address in addressList)
                {
                    await _addressRepo.DeleteAsync(cancellationToken, address.Id);
                }
            }
                                                 

            await _repo.DeleteAsync(cancellationToken, id);
        
        }

      
    }
}
