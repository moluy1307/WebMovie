namespace WebMovie.Backend.Common.Enums
{
    /// <summary>
    /// Thông báo lỗi
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// Lấy dữ liệu không thành công
        /// </summary>
        GetFailed = 1,

        /// <summary>
        /// Thêm mới không thành công
        /// </summary>
        InsertFailed = 2,

        /// <summary>
        /// Sửa không thành công
        /// </summary>
        PutFailed = 3,

        /// <summary>
        /// Xóa không thành công
        /// </summary>
        DeleteFailed = 4,

        /// <summary>
        /// Lỗi Exception
        /// </summary>
        Exception = 5,

        /// <summary>
        /// Sinh mã mới không thành công
        /// </summary>
        GenericNewEmployeeCodeFailed = 6,

        /// <summary>
        /// Dữ liệu không hợp lệ
        /// </summary>
        InvalidData = 7
    }
}
