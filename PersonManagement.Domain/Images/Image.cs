using PizzApp.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzApp.Domain.Images
{
    public class Image:IPizzaEntity
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public string OriginalName { get; set; }
        public string Path { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
