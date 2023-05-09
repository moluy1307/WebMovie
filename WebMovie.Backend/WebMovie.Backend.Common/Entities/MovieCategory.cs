using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMovie.Backend.Common.Entities
{
    public class MovieCategory
    {
        public Guid? MovieId { get; set; }

        public Guid? Value { get; set; }
    }
}
