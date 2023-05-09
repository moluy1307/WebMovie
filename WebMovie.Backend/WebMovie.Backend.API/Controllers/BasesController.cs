using Microsoft.AspNetCore.Mvc;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.Common;

namespace MISA.Amis.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field

        private IBasaBL<T> _baseBL;

        #endregion

        #region Constructor

        public BasesController(IBasaBL<T> baseaBL)
        {
            _baseBL = baseaBL;
        }

        #endregion

        #region Method

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
        [HttpGet]
        [Route("filter")]
        public IActionResult GetRecordByFilterPaging
            ([FromQuery] string? keyword,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] string? sortOrderColumn,
            [FromQuery] string? sortOrderDirection,
            [FromQuery] int? kindOperator,
            [FromQuery] string? textDetailSearch,
            [FromQuery] string? filteredInfor
            )
        {
            try
            {
                var result = _baseBL.GetRecordByFilterPaging(keyword, pageNumber, pageSize, sortOrderColumn, sortOrderDirection, kindOperator, textDetailSearch, filteredInfor);

                //Xử lý kết quả trả về
                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = WebMovie.Backend.Common.Enums.ErrorCode.GetFailed,
                        DevMsg = ResourceVI.Error_DatabaseQuery,
                        UserMsg = ResourceVI.Error_Exception
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Hàm lấy danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: huynq (9/2/2023)
        [HttpGet]
        public IActionResult GetAllRecord()
        {
            try
            {
                var listRecords = _baseBL.GetAllRecord();

                //Xử lý kết quả trả về
                if (listRecords != null)
                {
                    return StatusCode(StatusCodes.Status200OK, listRecords);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = WebMovie.Backend.Common.Enums.ErrorCode.GetFailed,
                        DevMsg = ResourceVI.Error_DatabaseQuery,
                        UserMsg = ResourceVI.Error_Exception
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Hàm lấy bản ghi theo Id
        /// </summary>
        /// <param name="recordId">Khóa chính</param>
        /// <returns>
        /// 200 - Lấy thông tin bản ghi theo Id thành công
        /// 500 - Có exception
        /// </returns>
        /// CreatedBy: huynq (9/2/2023)
        [HttpGet]
        [Route("recordId")]
        public IActionResult GetRecordById(Guid recordId)
        {
            try
            {
                var recordById = _baseBL.GetRecordById(recordId);

                //Xử lý kết quả trả về
                if (recordById != null)
                {
                    return StatusCode(StatusCodes.Status200OK, recordById);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = WebMovie.Backend.Common.Enums.ErrorCode.GetFailed,
                        DevMsg = ResourceVI.Error_DatabaseQuery,
                        UserMsg = ResourceVI.Error_Exception
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Hàm thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// 1 - Thêm mới thành công
        /// 0 - Thêm mới thất bại
        /// </returns>
        /// CreatedBy: huynq (9/2/2023)
        [HttpPost]
        public IActionResult InsertRecord(T record, Guid? parentId)
        {
            try
            {
                var result = _baseBL.InsertRecord(record, parentId);

                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (!result.IsSuccess && result.ErrorCode == WebMovie.Backend.Common.Enums.ErrorCode.InvalidData)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        MoreInfo = result.Data,
                        TradeId = HttpContext.TraceIdentifier
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        MoreInfo = result.Data,
                        TradeId = HttpContext.TraceIdentifier
                    });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

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
        [HttpPut]
        [Route("{recordId}")]
        public IActionResult UpdateRecord(Guid recordId, T? record)
        {
            try
            {
                var result = _baseBL.UpdateRecord(recordId, record);

                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else if (!result.IsSuccess && result.ErrorCode == WebMovie.Backend.Common.Enums.ErrorCode.InvalidData)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        MoreInfo = result.Data,
                        TradeId = HttpContext.TraceIdentifier
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        MoreInfo = result.Data,
                        TradeId = HttpContext.TraceIdentifier
                    });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Hàm xóa thông tin bản ghi
        /// </summary>
        /// <param name="recordId">Khóa chính</param>
        /// <param name="record">Bản ghi có Id trên</param>
        /// <returns>
        /// 200 - Sửa thông tin bản ghi thành công
        /// 400 - Đầu vào không hợp lệ
        /// 500 - Có exception
        /// </returns>
        /// CreatedBy: huynq (9/2/2023)
        [HttpDelete]
        [Route("{recordId}")]
        public IActionResult DeleteRecord(Guid recordId)
        {
            try
            {
                //var result = _baseBL.DeleteRecord(recordId);
                ////Xử lý kết quả trả về
                //if (result > 0)
                //{
                //    return StatusCode(StatusCodes.Status200OK);
                //}
                //else
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                //    {
                //        ErrorCode = Common.Enums.ErrorCode.DeleteFailed,
                //        DevMsg = ResourceVI.Invalid_InputData,
                //        UserMsg = ResourceVI.Error_Exception
                //    });
                //}
                var result = _baseBL.DeleteRecord(recordId);

                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else if (!result.IsSuccess && result.ErrorCode == WebMovie.Backend.Common.Enums.ErrorCode.InvalidData)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        MoreInfo = result.Data,
                        TradeId = HttpContext.TraceIdentifier
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        MoreInfo = result.Data,
                        TradeId = HttpContext.TraceIdentifier
                    });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

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
        [HttpPost]
        [Route("deleteMultiple")]
        public IActionResult DeleteMultipleRecord([FromBody] List<Guid> recordIds)
        {
            try
            {
                var numberOfAffectedRows = _baseBL.DeleteMultipleRecord(recordIds);

                //Xử lý kết quả trả về
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = WebMovie.Backend.Common.Enums.ErrorCode.DeleteFailed,
                        DevMsg = ResourceVI.Error_DatabaseQuery,
                        UserMsg = ResourceVI.Error_Exception
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Bắt sự kiện exception try/catch
        /// </summary>
        /// <param name="exception">Lỗi</param>
        /// <returns>
        /// 500 - có exception
        /// </returns>
        /// CreatedBy: huynq (2/2/2023)
        protected IActionResult HandleException(Exception exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
            {
                ErrorCode = WebMovie.Backend.Common.Enums.ErrorCode.Exception,
                DevMsg = exception.Message,
                UserMsg = ResourceVI.Error_Exception
            });
        }

        /// <summary>
        /// Hàm xuất dữ liệu ra file excel
        /// </summary>
        /// <returns>
        /// 200 - Xuất thành công
        /// 500 - Có Exception
        /// </returns>
        /// CreatedBy: huynq (15/2/2023)
        [HttpGet("ExportToExcel")]
        public IActionResult ExportAllRecord()
        {

            try
            {
                var bin = _baseBL.ExportAllRecord();
                return File(bin, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_nhan_vien.xlsx");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Hàm xuất dữ liệu ra file excel theo Id
        /// </summary>
        /// <returns>
        /// 200 - Xuất thành công
        /// 500 - Có Exception
        /// </returns>
        /// CreatedBy: huynq (20/2/2023)
        [HttpPost("ExportById")]
        public IActionResult ExportRecordById([FromBody] List<Guid> recordIds)
        {

            try
            {
                var bin = _baseBL.ExportRecordById(recordIds);
                return File(bin, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_nhan_vien.xlsx");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        #endregion
    }
}
