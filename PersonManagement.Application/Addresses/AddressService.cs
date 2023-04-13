using Mapster;
using PersonManagement.Infrastructure.Addresses;
using PizzApp.Application.Addresses.Requests;
using PizzApp.Application.Addresses.Responses;
using PizzApp.Domain.Addresses;

namespace PizzApp.Application.Addresses
{
    public class AddressService : IAddressService
    {
        private IAddressRepository _repo;

        public AddressService(IAddressRepository repo)
        {
            _repo = repo;
        }


        public async Task<List<AddressResponseModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllAsync(cancellationToken);

            return result.Adapt<List<AddressResponseModel>>();
        }

        public async Task CreateAsync(CancellationToken cancellationToken, AddressRequestModel address)
        {
            var addressToInsert = address.Adapt<Address>();

            await _repo.CreateAsync(cancellationToken, addressToInsert);
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, AddressRequestModel address,int id)
        {
            if (!await _repo.Exists(cancellationToken, id))
                throw new Exception("Not Found");
                                                 

            var addressToUptdate = address.Adapt<Address>();

            await _repo.UpdateAsync(cancellationToken, addressToUptdate);
        }

        public async Task DeleteAsync(CancellationToken cancellationToken, int id)
        {
            if (!await _repo.Exists(cancellationToken, id))
                throw new Exception("Not Found"); 

            await _repo.DeleteAsync(cancellationToken, id);
        
        }

        public async Task<AddressResponseModel> GetAsync(CancellationToken cancellationToken, int id)
        {
            var address =  await _repo.GetByIdAsync(cancellationToken, id);
            var addressResponse = address.Adapt<AddressResponseModel>();
            return addressResponse;
        }

    }
}
