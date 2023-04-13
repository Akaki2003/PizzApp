using PizzApp.Domain.Pizzas;

namespace PizzApp.Application.Orders.Responses
{
    public class OrderResponseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public Pizza Pizza { get; set; }
    }
}
