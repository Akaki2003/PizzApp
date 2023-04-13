using PizzApp.Domain.Abstraction;
using PizzApp.Domain.Addresses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzApp.Domain.Pizzas
{
    public class Pizza:IPizzaEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; } 
        public decimal CaloryCount { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
