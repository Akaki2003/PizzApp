using Microsoft.Extensions.DependencyInjection;
using PersonManagement.Infrastructure.Addresses;
using PersonManagement.Infrastructure.Orders;
using PizzApp.Application.Addresses;
using PizzApp.Application.Orders;
using PizzApp.Application.Pizzas;
using PizzApp.Application.RankHistories;
using PizzApp.Application.Users;
using PizzApp.Infrastructure.Addresses;
using PizzApp.Infrastructure.Orders;
using PizzApp.Infrastructure.Pizzas;
using PizzApp.Infrastructure.RankHistories;
using PizzApp.Infrastructure.Users;

namespace PizzApp.API.Infrastructure.Extensions;
public static class ServiceExtensions
{

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IAddressRepository, AddressRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderRepository, OrderRepository>();


        services.AddScoped<IPizzaService, PizzaService>();
        services.AddScoped<IPizzaRepository, PizzaRepository>();

        services.AddScoped<IRankHistoryService, RankHistoryService>();
        services.AddScoped<IRankHistoryRepository, RankHistoryRepository>();

    }
}

