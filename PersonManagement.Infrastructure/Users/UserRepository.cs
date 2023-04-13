using Microsoft.Extensions.Options;
using PizzApp.Domain.Addresses;
using PizzApp.Domain.Users;
using PizzApp.Infrastructure.Users;
using PizzApp.Persistence;
using System.Data;
using System.Data.SqlClient;

namespace PizzApp.Infrastructure.Users
{
    public class UserRepository : IUserRepository
    {

        #region Private Members

        private readonly string _connection;

        #endregion

        public UserRepository(IOptions<ConnectionStrings> options)
        {
            _connection = options.Value.DefaultConnection;
        }

        public async Task<User> GetByIdAsync(CancellationToken cancellationToken, int id)
        {
            if (!await Exists(cancellationToken, id))
            {
                throw new Exception("Item with that id doesn't exist");
            }
            string selectQuery = "select * from Users where id = @id";
            string addressSelectQuery = "select * from Addresses where UserId = @id";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);
                SqlCommand addressCommand = new SqlCommand(addressSelectQuery, connection);


                command.Parameters.AddWithValue("id", id);
                addressCommand.Parameters.AddWithValue("id", id);

                connection.Open();

                SqlDataReader reader = await addressCommand.ExecuteReaderAsync(cancellationToken);

                User user = null;
                List<Address> addresses = new List<Address>();
                Address address = null;

                while(await reader.ReadAsync(cancellationToken))
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
                    addresses.Add(address);
                }
                connection.Close();
                connection.Open();

                reader = await command.ExecuteReaderAsync(cancellationToken);
                while (await reader.ReadAsync(cancellationToken))
                {
                    user = new User
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                        PhoneNumber = reader.GetString(4),
                        Addresses = addresses,
                        IsDeleted = reader.GetBoolean(5),
                        CreatedOn = reader.GetDateTime(6),
                        ModifiedOn = reader.GetDateTime(7),
                    };
                }

                reader.Close();

                return user;
            }
        }

        public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            //addresses
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
                connection.Close();
            }


                //users
                List<User> users = new List<User>();

                selectQuery = "select * from Users";



            
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                var command = new SqlCommand(selectQuery, connection);
                connection.Open();
                var reader = await command.ExecuteReaderAsync(cancellationToken);

                while (await reader.ReadAsync(cancellationToken))
                {
                    users.Add(new User
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                        PhoneNumber = reader.GetString(4),
                        Addresses = addresses.FindAll(address=>address.UserId==reader.GetInt32(0)),
                        IsDeleted = reader.GetBoolean(5),
                        CreatedOn = reader.GetDateTime(6),
                        ModifiedOn = reader.GetDateTime(7),

                    });
                }

                reader.Close();

                return users;
            }
        }

        public async Task CreateAsync(CancellationToken cancellationToken, User user)
        {
            string selectQuery = "insert into users values(@FirstName, @LastName, @Email, @PhoneNumber,@IsDeleted,@CreatedOn,@ModifiedOn)";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("FirstName", user.FirstName);
                command.Parameters.AddWithValue("LastName", user.LastName);
                command.Parameters.AddWithValue("Email", user.Email);
                command.Parameters.AddWithValue("PhoneNumber", user.PhoneNumber);
                command.Parameters.AddWithValue("IsDeleted", 0);
                command.Parameters.AddWithValue("CreatedOn", DateTime.Now);
                command.Parameters.AddWithValue("ModifiedOn", DateTime.Now);

                connection.Open();

                await command.ExecuteScalarAsync(cancellationToken);
            }
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, User user)
        {
            if (!await Exists(cancellationToken, user.Id))
            {
                throw new Exception("Item with that id doesn't exist");
            }
            string selectQuery = "update  Users set FirstName=@FirstName, LastName=@LastName, Email=@Email, PhoneNumber=@PhoneNumber, ModifiedOn=@ModifiedOn where id = @id";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("Id", user.Id);
                command.Parameters.AddWithValue("FirstName", user.FirstName);
                command.Parameters.AddWithValue("LastName", user.LastName);
                command.Parameters.AddWithValue("Email", user.Email);
                command.Parameters.AddWithValue("PhoneNumber", user.PhoneNumber);
                command.Parameters.AddWithValue("ModifiedOn", DateTime.Now);

                connection.Open();

                await command.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(CancellationToken cancellationToken, int id)
        {
            if ( !await Exists(cancellationToken, id))
            {
                throw new Exception("Item with that id doesn't exist");
            }
            string selectQuery = "update Users set IsDeleted=1 where id = @id;";


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

            string selectQuery = "select Count(*) from Users where (id = @id) and IsDeleted!=1 ;";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("Id", id);

                connection.Open();

                int count = (int)await command.ExecuteScalarAsync(cancellationToken);

                return count > 0;
            }
        }
    }
}
