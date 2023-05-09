using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMovie.Backend.Common.Entities
{
    public class Episode
    {
        [Key]
        public Guid EpisodeId { get; set; }

        public string? EpisodeName { get; set; }

        public int? EpisodeNumber { get; set; }

        public string? EpisodeUrl { get; set; }

        [ForeignKey]
        public Guid? MovieId { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
