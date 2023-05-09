using WebMovie.Backend.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMovie.Backend.Common.Entities.DTO
{
    public class ServiceResult
    {
        /// <summary>
        /// Kết quả validate
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Dữ lệu mô tả lỗi chi tiết
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Mô tả lỗi
        /// </summary>
        public string Message { get; set; }
    }
}
