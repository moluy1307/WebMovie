using WebMovie.Backend.Common.Enums;

namespace WebMovie.Backend.Common.Entities.DTO
{
    /// <summary>
    /// Kết quả thông báo lỗi
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// Định danh của mã lỗi nội bộ
        /// </summary>
        public ErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Thông báo cho Dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Thông báo cho user
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin chi tiết hơn về vấn đề
        /// </summary>
        public object MoreInfo { get; set; }

        /// <summary>
        /// Mã để tra cứu thông tin log
        /// </summary>
        public string TradeId { get; set; }

        /// <summary>
        /// Object các lỗi trả 
        /// </summary>
        public object Data { get; set; }
    }
}
