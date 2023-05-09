using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.Common.Enums;

namespace WebMovie.Backend.Common.Entities
{
    public class Actor
    {
        [Key]
        public Guid Value { get; set; }

        [CheckIsEmpty]
        [PropertyName("Mã diễn viên")]
        [RecordCode]
        public string ActorCode { get; set; }

        public string Fullname { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        /// <summary>
        /// Giới tính
        /// <return>Trả về chuỗi (Nam/Nữ/Giới tính khác)</return>
        /// </summary>
        [PropertyName("Giới tính")]
        [NoParam]
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Gender.Male:
                        return ResourceVI.Gender_Male;
                    case Gender.FeMale:
                        return ResourceVI.Gender_Female;
                    case Gender.Other:
                        return ResourceVI.Gender_Other;
                    default:
                        return null;
                }
            }
        }

        public string? Country { get; set; }

        public string? Biography { get; set; }

        public IFormFile? Image { get; set; }

        public byte[]? ImgByte { get; set; }

        public string? ImagePath { get; set; }
    }
}
