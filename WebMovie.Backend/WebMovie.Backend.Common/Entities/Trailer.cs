using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMovie.Backend.Common.Entities
{
    public class Trailer
    {
        public Guid TrailerId { get; set; }

        public string TrailerName { get; set;}

        public string TrailerUrl { get; set;}

        public string TrailerTime { get; set;}

        [ForeignKey]
        public Guid? MovieId { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
