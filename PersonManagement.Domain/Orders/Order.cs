using PizzApp.Domain.Abstraction;
using PizzApp.Domain.Addresses;
using PizzApp.Domain.Pizzas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzApp.Domain.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int PizzaId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
