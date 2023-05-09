using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMovie.Backend.Common.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [CheckIsEmpty]
        [PropertyName("Tên đăng nhập")]
        [RecordCode]
        public string Username { get; set; }

        public string Password { get; set; }

        public Guid? RoleId { get; set; }

        [NoParam]
        public string? RoleName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public decimal? AmountMoney { get; set; }

        public string? ConfirmCode { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
