using PizzApp.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzApp.Domain.RankHistories
{
    public class RankHistory 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PizzaId { get; set; }
        public int Rank { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
