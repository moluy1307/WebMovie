using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMovie.Backend.Common.Entities
{
    public class Movie
    {
        [Key]
        public Guid MovieId { get; set; }

        public string MovieCode { get; set; }

        public string MovieName { get; set; }

        public IFormFile? Image { get; set; }

        public byte[]? ImgByte { get; set; }

        public string? ImagePath { get; set; }

        public string? MovieInfor { get; set; }

        public string? MovieDirector { get; set; }

        public int? MovieReview { get; set; }

        public string? MovieTime { get; set; }

        public DateTime? DateOfManufactor { get; set; }

        public string? MovieArea { get; set; }

        public decimal? Amount { get; set; }

        public string? CateMovie { get; set; }

        public string? EpisodeMovie { get; set; }

        [NoParam]
        public List<MovieCategory>? Categories { get; set; }

        public string? GenreNames { get; set; }

        [NoParam]
        public List<Episode>? Episodes { get; set; }

        public string? ActorMovie { get; set; }

        [NoParam]
        public List<MovieActor>? Actors { get; set; }

        public int TypeMovie { get; set; }

        [NoParam]
        public object? NewestEpisode { get; set; }

        public DateTime? CreatedDate { get; set; }

        //public Movie()
        //{
        //    Categories = new List<MovieCategory>();
        //}
    }
}