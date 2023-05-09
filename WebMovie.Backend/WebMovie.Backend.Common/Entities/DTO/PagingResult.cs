namespace WebMovie.Backend.Common.Entities.DTO
{
    /// <summary>
    /// Kết quả phân trang
    /// </summary>
    public class PagingResult<T>
    {
        /// <summary>
        /// Tổng số lượng bản ghi
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Tồng số trang
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Vị trí trang hiện tại
        /// </summary>
        public int CurrentPageNumber { get; set; }

        /// <summary>
        /// Số bản ghi trên 1 trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PagingResult(int totalRecords, int currentPageNumber, int pageSize, List<T> data) {
            TotalRecords = totalRecords;
            TotalPages = Convert.ToInt32(Math.Ceiling(((double)TotalRecords / (double)pageSize)));
            CurrentPageNumber = currentPageNumber;
            PageSize = pageSize;
            Data = data;
        }
    }
}
