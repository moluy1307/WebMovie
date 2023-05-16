using Dapper;
using System.ComponentModel.DataAnnotations;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.Common;
using System.Data;
using System.Reflection;
using WebMovie.Backend.DL.BaseDL;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace WebMovie.Backend.BL.BaseBL
{
    public class BaseBL<T> : IBasaBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
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
        public PagingResult<T> GetRecordByFilterPaging(
            string? keyword,
            int pageNumber,
            int pageSize,
            string? sortOrderColumn,
            string? sortOrderDirection,
            int? kindOperator,
            string? textDetailSearch,
            string? filteredInfor)
        {
            var result = _baseDL.GetRecordByFilterPaging(keyword, pageNumber, pageSize, sortOrderColumn, sortOrderDirection, kindOperator, textDetailSearch, filteredInfor);

            //Xử lý kết quả trả về
            return result;
        }

        /// <summary>
        /// Hàm lấy danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: huynq (9/2/2023)
        public IEnumerable<T> GetAllRecord()
        {
            var listRecords = _baseDL.GetAllRecord();
            return listRecords;
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
        public T GetRecordById(Guid recordId)
        {
            var recordById = _baseDL.GetRecordById(recordId);
            return recordById;
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
        public ServiceResult InsertRecord(T record, Guid? parentId)
        {
            //Validate
            var validateResults = ValidateRequest(record, parentId);

            //Thất bại -> Return lỗi
            if (validateResults.Count > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.InvalidData,
                    Message = ResourceVI.Invalid_InputData,
                    Data = validateResults
                };
            }

            //Thành công -> Gọi vào DL để chạy stored
            var numberOfAffectedRows = _baseDL.InsertRecord(record, parentId);

            //XỬ lý kết quả trả về
            if (numberOfAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.InsertFailed,
                    Message = ResourceVI.InsertFaild
                };
            }

            //Trả ngược lại cho Controller

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
        public ServiceResult UpdateRecord(Guid recordId, T? record)
        {
            //Validate
            var validateResults = ValidateRequest(recordId, record);

            //Thất bại -> Return lỗi
            if (validateResults.Count > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.InvalidData,
                    Message = ResourceVI.Invalid_InputData,
                    Data = validateResults
                };
            }

            //Thành công -> Gọi vào DL để chạy stored
            var numberOfAffectedRows = _baseDL.UpdateRecord(recordId, record);

            //XỬ lý kết quả trả về
            if (numberOfAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.InsertFailed,
                    Message = ResourceVI.UpdateFailed
                };
            }
        }

        /// <summary>
        /// Hàm xóa 1 bản ghi
        /// </summary>
        /// <param name="recordId">Khoá chính</param>
        /// <returns>
        /// 200 - Xóa thành công
        /// 500 - Có exception
        /// </returns>
        /// CreatedBy: huynq (9/2/2023)
        public ServiceResult DeleteRecord(Guid recordId)
        {
            //Validate
            var validateResults = ValidateRequest(recordId);

            //Thất bại -> Return lỗi
            if (validateResults.Count > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.InvalidData,
                    Message = ResourceVI.Invalid_InputData,
                    Data = validateResults
                };
            }

            //Thành công -> Gọi vào DL để chạy stored
            var numberOfAffectedRows = _baseDL.DeleteRecord(recordId);

            //XỬ lý kết quả trả về
            if (numberOfAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.DeleteFailed,
                    Message = ResourceVI.DeleteFailed
                };
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
        public int DeleteMultipleRecord(List<Guid> recordIds)
        {
            var numberOfAffectedRows = _baseDL.DeleteMultipleRecord(recordIds);

            //Xử lý kết quả trả về
            return numberOfAffectedRows;
        }

        /// <summary>
        /// Hàm xuất dữ liệu ra file excel
        /// </summary>
        /// <returns>
        /// 200 - Xuất thành công
        /// 500 - Có Exception
        /// </returns>
        /// CreatedBy: huynq (15/2/2023)
        public Byte[] ExportAllRecord()
        {
            var dataTable = _baseDL.ExportAllRecord();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var ws = WorksheetRecord(package, dataTable);

                int colIndex = 1;
                int rowIndex = 3;

                //Tạo các header từ column header của dataTable
                foreach (DataColumn col in dataTable.Columns)
                {
                    var cell = ws.Cells[rowIndex, colIndex];

                    //In đậm
                    cell.Style.Font.Bold = true;

                    //Set màu header
                    var fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                    //Căn chỉnh border
                    var border = cell.Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                    //Căn giữa
                    ws.Cells[rowIndex, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    cell.Value = col.ColumnName;

                    //Tự động căn chỉnh width header table
                    cell.AutoFitColumns();

                    colIndex++;
                }

                //Tạo danh sách các bản ghi từ row datatable
                foreach (DataRow row in dataTable.Rows)
                {
                    colIndex = 1;
                    rowIndex++;
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];

                        cell.Value = row[col.ColumnName].ToString();

                        var border = cell.Style.Border;

                        colIndex++;
                    }
                }

                //Tự động căn chỉnh width column của table
                for (int i = 1; i <= ws.Dimension.End.Column; i++)
                {
                    ws.Column(i).AutoFit();
                }

                return package.GetAsByteArray();
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
        public Byte[] ExportRecordById(List<Guid> recordIds)
        {
            var dataTable = _baseDL.ExportRecordById(recordIds);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var ws = WorksheetRecord(package, dataTable);

                int colIndex = 1;
                int rowIndex = 3;

                //Tạo các header từ column header của dataTable
                foreach (DataColumn col in dataTable.Columns)
                {
                    var cell = ws.Cells[rowIndex, colIndex];

                    //In đậm
                    cell.Style.Font.Bold = true;

                    //Set màu header
                    var fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                    //Căn chỉnh border
                    var border = cell.Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                    //Căn giữa
                    ws.Cells[rowIndex, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    cell.Value = col.ColumnName;

                    //Tự động căn chỉnh width header table
                    cell.AutoFitColumns();

                    colIndex++;
                }

                //Tạo danh sách các bản ghi từ row datatable
                foreach (DataRow row in dataTable.Rows)
                {
                    colIndex = 1;
                    rowIndex++;
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];

                        cell.Value = row[col.ColumnName].ToString();

                        var border = cell.Style.Border;

                        colIndex++;
                    }
                }

                //Tự động căn chỉnh width column của table
                for (int i = 1; i <= ws.Dimension.End.Column; i++)
                {
                    ws.Column(i).AutoFit();
                }

                return package.GetAsByteArray();
            }

        }

        private ExcelWorksheet WorksheetRecord(ExcelPackage package, DataTable dt)
        {
            ExcelWorksheet ws = null;
            foreach (var attribute in typeof(T).GetCustomAttributes(false))
            {
                //Đọc Attribute của đối tượng
                var nameOfObject = attribute as ObjectName;
                if (nameOfObject != null)
                {
                    ws = package.Workbook.Worksheets.Add(string.Format(ResourceVI.Title_Content_Excel, (nameOfObject.Name).ToUpper()));

                    // Lấy ra số lượng cột cần dùng dựa vào số lượng header
                    var countColHeader = dt.Columns.Count;

                    // Merge các column lại từ column 1 đến số column header
                    ws.Cells[1, 1].Value = string.Format(ResourceVI.Title_Content_Excel, (nameOfObject.Name).ToUpper());
                    ws.Cells[1, 1, 1, countColHeader].Merge = true;

                    // In đậm
                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;

                    //Cỡ chữ
                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Size = 16;

                    // Căn giữa
                    ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                }
            }
            return ws;
        }

        /// <summary>
        /// Kiểm tra mã nhân viên có bị trùng hay không
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>
        /// true - Đã bị trùng
        /// false - Không bị trùng
        /// </returns>
        /// CreatedBy: huynq (8/2/2023)
        public bool CheckRecordCode(string recordCode)
        {
            var res = _baseDL.CheckRecordCode(recordCode);
            return res;
        }

        /// <summary>
        /// Lấy mã nhân viên theo Id
        /// </summary>
        /// <param name="employeeId">Khóa chính</param>
        /// <returns>
        /// Trả về 1 mã nhân viên
        /// </returns>
        /// CreatedBy: huynq (8/2/2023)
        public string GetRecordCodeById(Guid recordId)
        {
            var res = _baseDL.GetRecordCodeById(recordId);
            return res;
        }

        /// <summary>
        /// Hàm validate chức năng Thêm cho Object
        /// </summary>
        /// <param name="record">Object muốn validate</param>
        /// <returns>danh sách thông báo lỗi</returns>
        /// CreatedBy: huynq (10/2/2023)
        protected virtual List<string> ValidateRequest(T record, Guid? parentId)
        {
            var errorData = new List<string>();

            var result = ValidateGeneral(record, parentId);
            if (result.Count > 0)
            {
                foreach (var element in result)
                {
                    errorData.Add(element);
                }
            }

            var primaryCode = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RecordCode)));
            foreach (var property in primaryCode)
            {
                var value = property.GetValue(record);
                //var name = property.Name;
                var ErrorpropertyName = string.Empty;
                var propertyName = property.GetCustomAttributes(typeof(PropertyName), false);

                if (propertyName.Length > 0)
                {
                    ErrorpropertyName = (propertyName[0] as PropertyName).Name;
                }

                var isDuplicate = CheckRecordCode((value.ToString()).Trim());
                if (isDuplicate == true)
                {
                    //errorData.Add(name, string.Format(ResourceVI.UserMsg_DuplicateCode, ErrorpropertyName));
                    errorData.Add(string.Format(ResourceVI.UserMsg_DuplicateCode, ErrorpropertyName, (value.ToString()).Trim()));
                }
            }

            return errorData;
        }

        /// <summary>
        /// Hàm validate chức năng sửa cho object
        /// </summary>
        /// <param name="recordId">Khóa chính</param>
        /// <param name="record">Object muốn validate</param>
        /// <returns>danh sách thông báo lỗi</returns>
        /// CreatedBy: huynq (10/2/2023)
        protected virtual List<string> ValidateRequest(Guid? recordId, T? record)
        {
            var errorData = new List<string>();

            //Tìm thuộc tính được đánh dấu là id gốc
            var propertiesAll = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(IsRootId)));

            foreach(var attr in propertiesAll)
            {
                var valueRootId = attr.GetValue(record);

                if(valueRootId != null)
                {
                    var result = ValidateGeneral(record, (Guid)valueRootId);
                    if (result.Count > 0)
                    {
                        foreach (var element in result)
                        {
                            errorData.Add(element);
                        }
                    }
                }
                
            }

            var primaryCode = record.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(RecordCode)));
            foreach (var property in primaryCode)
            {
                var value = property.GetValue(record);
                //var name = property.Name;
                var ErrorpropertyName = string.Empty;
                var propertyName = property.GetCustomAttributes(typeof(PropertyName), false);

                if (propertyName.Length > 0)
                {
                    ErrorpropertyName = (propertyName[0] as PropertyName).Name;
                }

                if (GetRecordCodeById((Guid)recordId) != value.ToString())
                {
                    var isDuplicate = CheckRecordCode((value.ToString()).Trim());
                    if (isDuplicate == true)
                    {
                        //errorData.Add(name, string.Format(ResourceVI.UserMsg_DuplicateCode, ErrorpropertyName));
                        errorData.Add(string.Format(ResourceVI.UserMsg_DuplicateCode, ErrorpropertyName, (value.ToString()).Trim()));
                    }
                }
            }

            return errorData;
        }

        /// <summary>
        /// Hàm validate chức năng xóa cho Object
        /// </summary>
        /// <param name="recordId">Id muốn xóa</param>
        /// <returns>danh sách thông báo lỗi</returns>
        /// CreatedBy: huynq (10/2/2023)
        protected virtual List<string> ValidateRequest(Guid recordId)
        {
            var errorData = new List<string>();

            return errorData;
        }

        /// <summary>
        /// Hàm xử lý validate chung cho các Object
        /// </summary>
        /// <param name="record">Object muốn validate</param>
        /// <returns>danh sách thông báo lỗi</returns>
        /// CreatedBy: huynq (10/2/2023)
        private List<string> ValidateGeneral(T record, Guid? parentId)
        {
            var errorData = new List<string>();

            //Lấy ra tên tất cả các thuộc tính có Attribute là CheckIsEmpty
            var properties = record.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(CheckIsEmpty)));

            foreach (var property in properties)
            {
                var value = property.GetValue(record);
                //var name = property.Name;
                var errorPropertyName = string.Empty;
                var propertyName = property.GetCustomAttributes(typeof(PropertyName), false);

                if (propertyName.Length > 0)
                {
                    errorPropertyName = (propertyName[0] as PropertyName).Name;
                }

                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    //errorData.Add(name, string.Format(ResourceVI.Validate_InputNull, ErrorpropertyName));
                    errorData.Add(string.Format(ResourceVI.Validate_InputNull, errorPropertyName));
                }
            }

            //Lấy ra tên tất cả các thuộc tính có Attribute là CheckIsEmpty
            var propertiesAll = record.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(MinLengthCustom)));

            foreach (var property in propertiesAll)
            {
                var value = property.GetValue(record) as string;
                //var name = property.Name;
                var errorPropertyName = string.Empty;
                var propertyName = property.GetCustomAttributes(typeof(PropertyName), false);
                var minLengthAttr = (MinLengthCustom)Attribute.GetCustomAttribute(property, typeof(MinLengthCustom));

                if (propertyName.Length > 0)
                {
                    errorPropertyName = (propertyName[0] as PropertyName).Name;
                }

                if (value.Length < minLengthAttr.ValueMin)
                {
                    //errorData.Add(name, string.Format(ResourceVI.Validate_InputNull, ErrorpropertyName));
                    errorData.Add(string.Format(ResourceVI.ValueMinlength_Input, errorPropertyName, minLengthAttr.ValueMin));
                }
            }

            //Validate riêng
            var result = ValidateCustom(record, parentId);
            if (result.Count > 0)
            {
                foreach (var element in result)
                {
                    errorData.Add(element);
                }
            }

            return errorData;
        }

        /// <summary>
        /// Hàm validate riêng cho từng object
        /// </summary>
        /// <param name="record">Object muốn validate</param>
        /// <returns>danh sách thông báo lỗi</returns>
        /// CreatedBy: huynq (10/2/2023)
        protected virtual List<string> ValidateCustom(T? record, Guid? parentId)
        {
            var errorData = new List<string>();

            return errorData;
        }

        #endregion
    }
}
