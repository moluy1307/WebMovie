using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMovie.Backend.Common.Entities
{
    public class Comment
    {
        [Key]
        public Guid CommentId { get; set; }

        public string? CommentContent { get; set; }

        public Guid? UserId { get; set; }

        public Guid? MovieId { get; set; }

        public int? Rating { get; set; }

        [NoParam]
        public string? Username { get; set; }

        public DateTime? CreatedDate { get; set;}

    }
}
