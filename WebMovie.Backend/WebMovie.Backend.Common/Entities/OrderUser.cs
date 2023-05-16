using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMovie.Backend.Common.Entities
{
    public class OrderUser
    {
        [Key]
        public Guid OrderId { get; set; }

        public string OrderName { get; set; }

        public decimal? Recharge { get; set; }

        [NoParam]
        public string? Username { get; set; }

        [NoParam]
        public string? Email { get; set; }

        [NoParam]
        public string? FullName { get; set; }

        [NoParam]
        public decimal? AmountMoney { get; set; }

        public Guid? UserId { get; set; }

        public Guid? MovieId { get; set; }

        public int Enable { get; set; }

        public DateTime? CreatedDate { get; set; }

    }
}
