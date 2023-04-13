using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzApp.Domain.Abstraction
{
    public interface IPizzaEntity
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}
