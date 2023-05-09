using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.Common.Entities.DTO;
using System.Data;

namespace WebMovie.Backend.BL.BaseBL
{
    public interface IBasaBL<T>
    {
        /// <summary>
        /// Hàm lấy danh sách đối tượng theo bộ lọc và phân trang, sắp xếp danh sách nhân viên
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (Mã, tên, số điên thoại)</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Vị trí trang hiện tại</param>
        /// <param name="sortOrderColumn">Sắp xếp danh sách thep giá trị</param>
        /// <param name="sortOrderDirection">Chiều sắp xếp tăng dần hay giảm dần</param>
        /// <param name="kindOperator">Điều kiện lọc (0-Chứa, 1-Bắt đầu với, 2-Kết thúc với, 3-Không chứa, 4-Bằng, 5-Khác</param>
        /// <param name="textDetailSearch">Từ khóa tìm kiếm chi tiết</param>
        /// <param name="filteredInfor">Thông tin được tìm kiếm</param>
        /// <returns>
        /// 200 - Lấy danh sách thành công
        /// 400 - Đầu vào không hợp lệ
        /// 500 - Có exception
        /// </returns>
        /// CreatedBy: huynq (8/2/2023)
        PagingResult<T> GetRecordByFilterPaging(
            string? keyword, 
            int pageNumber, 
            int pageSize, string? 
            sortOrderColumn, 
            string? sortOrderDirection, 
            int? kindOperator, 
            string? textDetailSearch, 
            string? filteredInfor);

        /// <summary>
        /// Hàm lấy danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: huynq (9/2/2023)
        IEnumerable<T> GetAllRecord();

        /// <summary>
        /// Hàm lấy bản ghi theo Id
        /// </summary>
        /// <param name="recordId">Khóa chính</param>
        /// <returns>
        /// 200 - Lấy thông tin bản ghi theo Id thành công
        /// 500 - Có exception
        /// </returns>
        /// CreatedBy: huynq (9/2/2023)
        T GetRecordById(Guid recordId);

        /// <summary>
        /// Hàm thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// 1 - Thêm mới thành công
        /// 0 - Thêm mới thất bại
        /// </returns>
        /// CreatedBy: huynq (9/2/2023)
        ServiceResult InsertRecord(T record, Guid? parent);

        /// <summary>
        /// Hàm Sửa thông tin bản ghi
        /// </summary>
        /// <param name="recordId">Khóa chính</param>
        /// <param name="record">Bản ghi có Id trên</param>
        /// <returns>
        /// 200 - Sửa thông tin bản ghi thành công
        /// 400 - Đầu vào không hợp lệ
        /// 500 - Có exception
        /// </returns>
        /// CreatedBy: huynq (9/2/2023)
        ServiceResult UpdateRecord(Guid recordId, T? record);

        /// <summary>
        /// Hàm xóa 1 bản ghi
        /// </summary>
        /// <param name="recordId">Khoá chính</param>
        /// <returns>
        /// 200 - Xóa thành công
        /// 500 - Có exception
        /// </returns>
        /// CreatedBy: huynq (9/2/2023)
        ServiceResult DeleteRecord(Guid recordId);

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="recordIds">Danh sách khóa chính muốn xóa</param>
        /// <returns>
        /// 200 - Xóa thành công
        /// 500 - Có exception
        /// </returns>
        /// CreatedBy: huynq (16/1/2023)
        /// Last Modified: huynq (2/2/2023)
        int DeleteMultipleRecord(List<Guid> recordIds);

        /// <summary>
        /// Hàm xuất dữ liệu ra file excel
        /// </summary>
        /// <returns>
        /// 200 - Xuất thành công
        /// 500 - Có Exception
        /// </returns>
        /// CreatedBy: huynq (15/2/2023)
        Byte[] ExportAllRecord();

        /// <summary>
        /// Hàm xuất dữ liệu ra file excel theo Id
        /// </summary>
        /// <returns>
        /// 200 - Xuất thành công
        /// 500 - Có Exception
        /// </returns>
        /// CreatedBy: huynq (20/2/2023)
        Byte[] ExportRecordById(List<Guid> recordIds);

        /// <summary>
        /// Kiểm tra mã bản ghi có bị trùng hay không
        /// </summary>
        /// <param name="recordCode">Mã bản ghi</param>
        /// <returns>
        /// true - Đã bị trùng
        /// false - Không bị trùng
        /// </returns>
        /// CreatedBy: huynq (8/2/2023)
        bool CheckRecordCode(string recordCode);

        /// <summary>
        /// Lấy mã bản ghi theo Id
        /// </summary>
        /// <param name="employeeId">Khóa chính</param>
        /// <returns>
        /// Trả về 1 mã bản ghi
        /// </returns>
        /// CreatedBy: huynq (8/2/2023)
        string GetRecordCodeById(Guid recordId);
    }
}
