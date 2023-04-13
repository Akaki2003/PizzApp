using Microsoft.Extensions.Options;
using PizzApp.Domain.Pizzas;
using PizzApp.Persistence;
using System.Data;
using System.Data.SqlClient;

namespace PizzApp.Infrastructure.Pizzas
{
    public class PizzaRepository:IPizzaRepository
    {

        #region Private Members

        private readonly string _connection;

        #endregion

        public PizzaRepository(IOptions<ConnectionStrings> options)
        {
            _connection = options.Value.DefaultConnection;
        }

        public async Task<Pizza> GetByIdAsync(CancellationToken cancellationToken, int id)
        {
            if (!await Exists(cancellationToken, id))
            {
                 throw new Exception("Item with that Id doesn't exist");
            }
            string selectQuery = "select * from Pizzas where id = @id";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("id", id);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

                Pizza pizza = null;

                while (await reader.ReadAsync(cancellationToken))
                {
                    pizza = new Pizza
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        CaloryCount = reader.GetDecimal(3),
                        Description = reader.GetString(4),
                        IsDeleted = reader.GetBoolean(5),
                        CreatedOn = reader.GetDateTime(6),
                        ModifiedOn = reader.GetDateTime(7),
                    };
                }

                reader.Close();

                return pizza;
            }
        }

        public async Task<List<Pizza>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<Pizza> pizzas = new List<Pizza>();

            string selectQuery = "select * from Pizzas";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

                while (await reader.ReadAsync(cancellationToken))
                {
                    pizzas.Add(new Pizza
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        CaloryCount = reader.GetDecimal(3),
                        Description = reader.GetString(4),
                        IsDeleted = reader.GetBoolean(5),
                        CreatedOn = reader.GetDateTime(6),
                        ModifiedOn = reader.GetDateTime(7),
                    });
                }

                reader.Close();

                return pizzas;
            }
        }

        public async Task CreateAsync(CancellationToken cancellationToken, Pizza pizza)
        {
            string selectQuery = "insert into Pizzas values (@Name, @Price, @Description, @CaloryCount,@IsDeleted,@CreatedOn,@ModifiedOn)";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("Name", pizza.Name);
                command.Parameters.AddWithValue("Price", pizza.Price);
                command.Parameters.AddWithValue("CaloryCount", pizza.CaloryCount);
                command.Parameters.AddWithValue("Description", pizza.Description);
                command.Parameters.AddWithValue("IsDeleted", 0);
                command.Parameters.AddWithValue("CreatedOn", DateTime.Now);
                command.Parameters.AddWithValue("ModifiedOn", DateTime.Now);

                connection.Open();

                await command.ExecuteScalarAsync(cancellationToken); ////"Error converting data type nvarchar to numeric.",
            }
        }

        public async Task UpdateAsync(CancellationToken cancellationToken, Pizza pizza, int id)
        {
            if (!await Exists(cancellationToken, id))
            {
                 throw new Exception("Item with that Id doesn't exist");
            }
            string selectQuery = "update  Pizzas set Name=@Name, Price=@Price, Description=@Description, CaloryCount=@CaloryCount, ModifiedOn=@ModifiedOn where id = @id";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("Name", pizza.Name);
                command.Parameters.AddWithValue("Price", pizza.Price);
                command.Parameters.AddWithValue("Description", pizza.Description);
                command.Parameters.AddWithValue("CaloryCount", pizza.CaloryCount);
                command.Parameters.AddWithValue("ModifiedOn", DateTime.Now);

                connection.Open();

                await command.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(CancellationToken cancellationToken, int id)
        {
            if (!await Exists(cancellationToken, id))
            {
                 throw new Exception("Item with that Id doesn't exist");
            }
            string selectQuery = "update Pizzas set IsDeleted = 1 where id = @id;";


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
            string selectQuery = "select Count(*) from Pizzas where (id = @id) and IsDeleted!=1 ;";




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
