using PizzApp.Application.Pizzas.Requests;
using PizzApp.Domain.Pizzas;

namespace PizzApp.Application.Orders.Requests
{
    public class OrderRequestModel
    {
        //public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int PizzaId { get; set; }
    }
}
