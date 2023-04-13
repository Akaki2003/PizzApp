using Microsoft.Extensions.Options;
using PersonManagement.Infrastructure.Addresses;
using PizzApp.Domain.Addresses;
using PizzApp.Persistence;
using System.Data.Common;
using System.Data.SqlClient;

namespace PizzApp.Infrastructure.Addresses
{
    public class AddressRepository : IAddressRepository
    {

        #region Private Members

        private readonly string _connection;

        #endregion

        public AddressRepository(IOptions<ConnectionStrings> options)
        {
            _connection = options.Value.DefaultConnection;
        }

        public async Task<Address> GetByIdAsync(CancellationToken cancellationToken, int id)
        {
            if (!await Exists(cancellationToken, id))
            {
                 throw new Exception("Item with that Id doesn't exist");
            }
            string selectQuery = "select * from Addresses where id = @id";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("id", id);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

                Address address = null;

                while (await reader.ReadAsync(cancellationToken))
                {
                    address = new Address
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        City = reader.GetString(2),
                        Country = reader.GetString(3),
                        Region = reader.GetString(4),
                        Description = reader.SafeGetString(5),
                        IsDeleted = reader.GetBoolean(6),
                        CreatedOn = reader.GetDateTime(7),
                        ModifiedOn = reader.GetDateTime(8),
                    };
                }


                return address;
            }
        }

        
        public async Task<List<Address>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<Address> addresses = new List<Address>();

            string selectQuery = "select * from Addresses";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

                while (await reader.ReadAsync(cancellationToken))
                {
                    addresses.Add(new Address
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        City = reader.GetString(2),
                        Country = reader.GetString(3),
                        Region = reader.GetString(4),
                        Description = reader.SafeGetString(5),
                        IsDeleted = reader.GetBoolean(6),
                        CreatedOn = reader.GetDateTime(7),
                        ModifiedOn = reader.GetDateTime(8),
                    });
                }

                reader.Close();

                return addresses;
            }
        }

        public async Task<List<Address>> GetAllByUserIdAsync(CancellationToken cancellationToken, int userId)
        {
            List<Address> addresses = new List<Address>();
            string selectQuery = "SELECT * FROM Addresses WHERE UserId = @UserId ";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("UserId", userId);
                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

                while (await reader.ReadAsync(cancellationToken))
                {
                    if (!reader.GetBoolean(8))
                    {
                        addresses.Add(new Address
                        {
                            Id = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            City = reader.GetString(2),
                            Country = reader.GetString(3),
                            Region = reader.GetString(4),
                            Description = reader.GetString(5), ///////
                            CreatedOn = reader.GetDateTime(6),
                            ModifiedOn = reader.GetDateTime(7)
                        });
                    }
                }
                // reader.Close();
                return addresses;
            }

        }


        public async Task CreateAsync(CancellationToken cancellationToken, Address address)
        {
            string selectQuery = "insert into Addresses values( @City, @Country, @Region,@Description,@IsDeleted,@CreatedOn,@ModifiedOn)";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                //command.Parameters.AddWithValue("Id", address.Id);
                command.Parameters.AddWithValue("City", address.City);
                command.Parameters.AddWithValue("Country", address.Country);
                command.Parameters.AddWithValue("Region", address.Region);
                command.Parameters.AddWithValue("Description", address.Description);
                command.Parameters.AddWithValue("IsDeleted", 0);
                command.Parameters.AddWithValue("CreatedOn", DateTime.Now);
                command.Parameters.AddWithValue("ModifiedOn", DateTime.Now);

                connection.Open();

                await command.ExecuteScalarAsync(cancellationToken);
            }
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, Address address)
        {
            if (!await Exists(cancellationToken, address.Id))
            {
                return;
            }
            string selectQuery = "update Addresses set UserId=@UserId, City=@City, Country=@Country, Region=@Region, Description=@Description,ModifiedOn = @ModifiedOn where id = @id";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("Id", address.Id);
                command.Parameters.AddWithValue("UserId", address.UserId);
                command.Parameters.AddWithValue("City", address.City);
                command.Parameters.AddWithValue("Country", address.Country);
                command.Parameters.AddWithValue("Region", address.Region);
                command.Parameters.AddWithValue("Description", address.Description);
                command.Parameters.AddWithValue("ModifiedOn", DateTime.Now);

                connection.Open();

                await command.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(CancellationToken cancellationToken, int id)
        {
            if (!await Exists(cancellationToken, id))
            {
                return;
            }
            string selectQuery = "update Addresses set IsDeleted=1 where id = @id;";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("Id", id);

                connection.Open();

                await command.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, int id)
        {
            string selectQuery = "select Count(*) from Addresses where (id = @id) and IsDeleted!=1 ;";


            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("Id", id);

                connection.Open();

                int count = (int)await command.ExecuteScalarAsync(cancellationToken);

                return count>0;
            }
        }
    }
}
