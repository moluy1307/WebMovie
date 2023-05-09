using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMovie.Backend.Common.Entities
{
    public class Category
    {
        public Guid Value { get; set; } 

        public string CategoryName { get; set; }    

        public DateTime CreatedDate { get; set; }
    }
}
