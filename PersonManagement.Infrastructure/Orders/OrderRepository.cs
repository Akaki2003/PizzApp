using Microsoft.Extensions.Options;
using PersonManagement.Infrastructure.Orders;
using PizzApp.Domain.Addresses;
using PizzApp.Domain.Orders;
using PizzApp.Domain.Pizzas;
using PizzApp.Domain.Users;
using PizzApp.Persistence;
using System.Data.SqlClient;

namespace PizzApp.Infrastructure.Orders
{
    public class OrderRepository : IOrderRepository
    {

        #region Private Members

        private readonly string _connection;

        #endregion

        public OrderRepository(IOptions<ConnectionStrings> options)
        {
            _connection = options.Value.DefaultConnection;
        }

        public async Task<Order> GetByIdAsync(CancellationToken cancellationToken, int id)
        {
            
            string selectQuery = "select * from Orders where id = @id";


            using (SqlConnection connection = new SqlConnection(_connection))
            {
                


                //orders
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("id", id);

                connection.Open();

                var reader = await command.ExecuteReaderAsync(cancellationToken);

                Order order = null;
                int pizzaId = 0;

                while (await reader.ReadAsync(cancellationToken))
                {
                    order = new Order
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        AddressId = reader.GetInt32(2),
                        PizzaId = reader.GetInt32(3),
                        CreatedOn = DateTime.Now,
                    };
                    pizzaId = reader.GetInt32(3);
                }

                reader.Close();
                connection.Close();

                return order;
            }
        }

        public async Task<List<Order>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<Order> orders = new List<Order>();

            //orders
            string selectQuery = "select * from Orders";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

                while (await reader.ReadAsync(cancellationToken))
                {
                    orders.Add(new Order
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        AddressId = reader.GetInt32(2),
                        PizzaId = reader.GetInt32(3),
                        CreatedOn = DateTime.Now,
                    }); ;
                }

                reader.Close();

            }
              return orders;
        }

        public async Task CreateAsync(CancellationToken cancellationToken, Order order) //orders ro qmni picac unda sheqmna? mierorebs es picas ar awodebo(null momdis ordershi)
        {
            string selectQuery = "insert into Orders values(@UserId, @AddressId,@PizzaId ,@CreatedOn)";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("UserId", order.UserId);
                command.Parameters.AddWithValue("AddressId", order.AddressId);
                command.Parameters.AddWithValue("PizzaId", order.PizzaId);
                command.Parameters.AddWithValue("CreatedOn", DateTime.Now);

                connection.Open();

                await command.ExecuteScalarAsync(cancellationToken);
            }
        }

        


        public async Task<bool> Exists(CancellationToken cancellationToken, int userId,int pizzaId)
        {
            string selectQuery = "select Count(*) from Orders where UserId = @UserId and PizzaId = @PizzaID ;"; //check if user-pizza exists



            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("UserId", userId);
                command.Parameters.AddWithValue("PizzaId", pizzaId);

                connection.Open();

                int count = (int)await command.ExecuteScalarAsync(cancellationToken);

                return count>0;
            }
        }
    }
}
