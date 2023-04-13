using PizzApp.Application.RankHistories.Requests;
using PizzApp.Application.RankHistories.Responses;

namespace PizzApp.Application.RankHistories;

public interface IRankHistoryService
{
    Task<List<RankHistoryResponseModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<RankHistoryResponseModel> GetAsync(CancellationToken cancellationToken, int id);
    Task CreateAsync(CancellationToken cancellationToken, RankHistoryRequestModel rankHistory);
}
