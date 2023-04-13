using Mapster;
using PizzApp.Application.Addresses.Requests;
using PizzApp.Application.Addresses.Responses;
using PizzApp.Application.Orders.Requests;
using PizzApp.Application.Orders.Responses;
using PizzApp.Application.Pizzas.Requests;
using PizzApp.Application.Pizzas.Responses;
using PizzApp.Application.RankHistories.Responses;
using PizzApp.Application.Users.Requests;
using PizzApp.Application.Users.Responses;
using PizzApp.Domain.Addresses;
using PizzApp.Domain.Orders;
using PizzApp.Domain.Pizzas;
using PizzApp.Domain.RankHistories;
using PizzApp.Domain.Users;

namespace PizzApp.API.Infrastructure.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<Pizza, PizzaResponseModel>
               .NewConfig().TwoWays();

            TypeAdapterConfig<PizzaRequestModel, Pizza>
                 .NewConfig().TwoWays();

            TypeAdapterConfig<Address, AddressResponseModel>
                .NewConfig().TwoWays();

            TypeAdapterConfig<AddressRequestModel, Address>
                 .NewConfig().TwoWays();

            TypeAdapterConfig<User, UserResponseModel>
               .NewConfig().TwoWays();

            TypeAdapterConfig<RankHistoryResponseModel, RankHistory>
                 .NewConfig().TwoWays();

            TypeAdapterConfig<UserRequestModel, User>
                 .NewConfig().TwoWays();

            TypeAdapterConfig<Order, OrderResponseModel>
               .NewConfig().TwoWays();

            TypeAdapterConfig<OrderRequestModel, Order>
                 .NewConfig().TwoWays();

            TypeAdapterConfig<RankHistory, RankHistoryResponseModel>
               .NewConfig().TwoWays();
        }
    }
}
