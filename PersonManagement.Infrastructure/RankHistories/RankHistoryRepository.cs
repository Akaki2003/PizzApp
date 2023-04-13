using Microsoft.Extensions.Options;
using PizzApp.Domain.Pizzas;
using PizzApp.Domain.RankHistories;
using PizzApp.Persistence;
using System.Data.SqlClient;

namespace PizzApp.Infrastructure.RankHistories
{
    public class RankHistoryRepository:IRankHistoryRepository
    {

        #region Private Members

        private readonly string _connection;

        #endregion

        public RankHistoryRepository(IOptions<ConnectionStrings> options)
        {
            _connection = options.Value.DefaultConnection;
        }

        public async Task<RankHistory> GetByIdAsync(CancellationToken cancellationToken, int PizzaId)
        {
            string selectQuery = "select * from RankHistories where id = @id";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("id", PizzaId);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

                RankHistory rankHistory = null;

                while (await reader.ReadAsync(cancellationToken))
                {
                    rankHistory = new RankHistory
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        PizzaId = reader.GetInt32(2),
                        Rank = reader.GetInt32(3),
                        CreatedOn = DateTime.Now,
                    };
                }

                reader.Close();

                return rankHistory;
            }
        }

        public async Task<List<RankHistory>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<RankHistory> rankHistories = new List<RankHistory>();

            string selectQuery = "select * from RankHistories";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                connection.Open();

                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

                while (await reader.ReadAsync(cancellationToken))
                {
                    rankHistories.Add(new RankHistory
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        PizzaId = reader.GetInt32(2),
                        Rank = reader.GetInt32(3),
                        CreatedOn = DateTime.Now,
                    });
                }

                reader.Close();

                return rankHistories;
            }
        }

        public async Task CreateAsync(CancellationToken cancellationToken, RankHistory rankHistory)
        {
            string selectQuery = "insert into RankHistories values(@UserId, @PizzaId, @Rank,@CreatedOn)";

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("UserId", rankHistory.UserId);
                command.Parameters.AddWithValue("PizzaId", rankHistory.PizzaId);
                command.Parameters.AddWithValue("Rank", rankHistory.Rank);
                command.Parameters.AddWithValue("CreatedOn", DateTime.Now);

                connection.Open();

                await command.ExecuteScalarAsync(cancellationToken);
            }
        }







        public async Task<int> AverageRankCalculator(CancellationToken cancellationToken, int pizzaId)
        {
            string selectQuery = "select ISNULL(avg([Rank]),-1) as avgRank from RankHistories where PizzaId = @id) ;";


            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("id", pizzaId);

                connection.Open();

                int rank = (int)await command.ExecuteScalarAsync(cancellationToken);

                return rank;
            }


        }


    }
}
