using Dapper;
using MySqlConnector;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebMovie.Backend.Common;
using WebMovie.Backend.Common.Constants;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL;
using WebMovie.Backend.DL.BaseDL;

namespace Misa.Amis.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
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
            //Chuẩn bị tên stored procedure
            string storedProceduredPagination = String.Format(ProcedureName.Pagination, typeof(T).Name);
            //string storedProceduredTotalRecords = "Proc_Employee_TotalRecords";

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();
            parameters.Add("@p_PageNumber", pageNumber);
            parameters.Add("@p_PageSize", pageSize);
            parameters.Add("@p_TextSearch", keyword);
            parameters.Add("@p_SortOrderColumn", sortOrderColumn);
            parameters.Add("@p_SortOrderDirection", sortOrderDirection);
            parameters.Add("@p_Operator", kindOperator);
            parameters.Add("@p_Detailsearch", textDetailSearch);
            parameters.Add("@p_FilteredInfor", filteredInfor);

            //Khởi tạo kế nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                var reader = mySqlConnection.QueryMultiple(storedProceduredPagination, parameters, commandType: CommandType.StoredProcedure);
                //var multi = mySqlConnection.QueryMultiple(storedProceduredTotalRecords, commandType: CommandType.StoredProcedure);
                int count = reader.Read<int>().FirstOrDefault();
                var listRecordByFilterPaging = reader.Read<T>().ToList();
                var result = new PagingResult<T>(count, pageNumber, pageSize, listRecordByFilterPaging);

                //Xử lý kết quả trả về
                return result;
            }
        }

        /// <summary>
        /// Hàm lấy danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: huynq (9/2/2023)
        public IEnumerable<T> GetAllRecord()
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(T).Name, "All");

            //Khởi tạo kế nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                var reader = mySqlConnection.QueryMultiple(storedProceduredName, commandType: CommandType.StoredProcedure);
                var listRecords = reader.Read<T>().ToList();

                return listRecords;
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
        public T GetRecordById(Guid recordId)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(T).Name, "ById");

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            //Tìm thuộc tính được đánh dấu là id
            var primaryCode = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(KeyAttribute)));
            foreach (var property in primaryCode)
            {
                var propName = property.Name;

                parameters.Add($"@p_{propName}", recordId);
            }
            
            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                var recordById = mySqlConnection.QueryFirstOrDefault<T>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);

                return recordById;
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
        public int InsertRecord(T record, Guid? parentId)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Insert, typeof(T).Name);

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            //Lấy tất cả các thuộc tính có trong object record (Employee, Department,...)
            var properties = typeof(T).GetProperties();

            //Duyệt qua tất cả các thuộc tính có trong object record (Employee, Department,...)
            foreach (var property in properties)
            {
                //Đọc tên thuộc tính có trong object record (Employee, Department,...)
                string name = property.Name;

                //Lấy giá trị của các thuộc tính
                var value = property.GetValue(record);

                //Kiểm tra thuộc tính có phải là khóa chính không
                var primaryKey = Attribute.IsDefined(property, typeof(KeyAttribute));

                //Sinh dữ liệu cho khóa chính
                if (primaryKey == true)
                {
                    if (property.PropertyType == typeof(Guid))
                    {
                        value = Guid.NewGuid();
                    }
                }

                //Kiểm tra thuộc tính có không có đối số
                var noParam = Attribute.IsDefined(property, typeof(NoParam));

                if (noParam == true)
                {
                    continue;
                }

                ////Tìm thuộc tính được đánh dấu là id gốc
                //var propertyRootId = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(IsRootId)));
                //foreach (var attr in propertyRootId)
                //{
                //    //Console.WriteLine("fsdfsdfsdf: " + property.Name);
                //    //parameters.Add($"{property.Name}", "056aa5ce-88b2-4f04-a283-4aed11578adc");
                //    //Kiểm tra thuọc tính có phải parentId không
                //    var primaryRootId = Attribute.IsDefined(typeof(T).GetProperty($"{attr.Name}"), typeof(IsRootId));
                //    if (primaryRootId == true)
                //    {
                //        value = parentId;
                //    }
                //        //if (attr.PropertyType == typeof(Guid))
                //        //{
                //        //    value = parentId;
                //        //}
                //        //}
                //}

                //Viết đối số trong DB
                string argument = "@p_" + name;

                parameters.Add($"{argument}", value);
            }

            //Tìm thuộc tính được đánh dấu là id gốc
            var propertiesAll = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(IsRootId)));
            //foreach (var property in propertiesAll)
            //{
            //    Console.WriteLine("fsdfsdfsdf: " + property.Name);
            //    parameters.Add($"{property.Name}", "056aa5ce-88b2-4f04-a283-4aed11578adc");
            //}
            if(propertiesAll.Count() > 0)
            {
                var primaryCode = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RecordCode)));
                foreach (var property in primaryCode)
                {
                    var value = property.GetValue(record) as string;

                    var minLengthAttr = (MinLengthCustom)Attribute.GetCustomAttribute(property, typeof(MinLengthCustom));

                    if (value.Length == minLengthAttr.ValueMin)
                    {
                        parameters.Add($"p_{typeof(T).Name}ParentId", null);
                    }
                    else if(value.Length > minLengthAttr.ValueMin)
                    {
                        parameters.Add($"p_{typeof(T).Name}ParentId", parentId);

                    }
                }
            }

            var primaryStatus = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(isStatus)));
            if(primaryStatus.Count() > 0)
            {
                foreach (var property in primaryStatus)
                {
                    parameters.Add($"p_{property.Name}", "Đang sử dụng");
                }
            }

            parameters.Add("p_CreatedDate", DateTime.Now);

            //Khởi tạo kết nối đến DB
            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                numberOfAffectedRows = mySqlConnection.Execute(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);
            }

            //Xử lý kết quả trả về
            return numberOfAffectedRows;
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
        public int UpdateRecord(Guid recordId, T? record)
        {
            string storedProceduredName = String.Format(ProcedureName.Update, typeof(T).Name);
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                //Đọc tên thuộc tính có trong class Employee
                string name = property.Name;

                //Lấy giá trị của các thuộc tính
                var value = property.GetValue(record);

                var primaryKey = Attribute.IsDefined(property, typeof(KeyAttribute));

                if (primaryKey == true)
                {
                    if (property.PropertyType == typeof(Guid))
                    {
                        value = recordId;
                    }
                }

                //Kiểm tra thuộc tính có không có đối số
                var noParam = Attribute.IsDefined(property, typeof(NoParam));

                if (noParam == true)
                {
                    continue;
                }

                //Kiểm tra thuộc tính là mật khẩu
                var isPass = Attribute.IsDefined(property, typeof(isPassword));

                if (isPass == true)
                {
                    continue;
                }

                //Viết đối số trong DB
                string argument = "@p_" + name;

                parameters.Add($"{argument}", value);
            }

            //Khởi tạo kết nối đến DB
            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                numberOfAffectedRows = mySqlConnection.Execute(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
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
        public int DeleteRecord(Guid recordId)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Delete, typeof(T).Name);

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            //Tìm thuộc tính được đánh dấu là id
            var primaryCode = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(KeyAttribute)));
            foreach (var property in primaryCode)
            {
                var propName = property.Name;

                parameters.Add($"@p_{propName}", recordId);
            }

            //parameters.Add($"p_{typeof(T).Name}Id", recordId);

            //Khởi tạo kết nối đến DB
            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {

                mySqlConnection.Open();

                //Gọi vào DB
                numberOfAffectedRows = mySqlConnection.Execute(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
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
            //Khởi tạo kết nối đến DB
            int numberOfAffectedRows = 0;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        var properties = typeof(T).GetProperties();
                        foreach (var property in properties)
                        {
                            var key = (KeyAttribute?)property.GetCustomAttribute(typeof(KeyAttribute), false);
                            if (key != null)
                            {
                                string query = string.Format("DELETE FROM {0} WHERE {1} IN @ids", typeof(T).Name, property.Name);

                                numberOfAffectedRows = mySqlConnection.Execute(query, param: new { ids = recordIds }, transaction: transaction);
                                transaction.Commit();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                }
            }

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
        public DataTable ExportAllRecord()
        {
            DataTable dt = new DataTable();

            //Lấy ra tên tất cả các thuộc tính có Attribute là ExportExcel
            var properties = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(ExportExcel))).ToList();

            CreateColumnDatatable(dt);

            string storedProceduredName = String.Format(ProcedureName.Get, typeof(T).Name, "All");

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                var reader = mySqlConnection.QueryMultiple(storedProceduredName, commandType: CommandType.StoredProcedure);
                var listRecords = reader.Read<T>().ToList();

                //Tạo 1 mảng object lưu thông tin các bản ghi có độ dài là số lượng các thuộc tính được xuất ra excel
                var values = new object[properties.Count()];

                foreach (var emp in listRecords)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        //Thêm giá trị từng thuộc tính được xuất ra excel vào mảng
                        values[i] = properties[i].GetValue(emp);
                    }

                    dt.Rows.Add(values);
                }
            }
            return dt;
        }

        /// <summary>
        /// Hàm xuất dữ liệu ra file excel theo Id
        /// </summary>
        /// <returns>
        /// 200 - Xuất thành công
        /// 500 - Có Exception
        /// </returns>
        /// CreatedBy: huynq (20/2/2023)
        public DataTable ExportRecordById(List<Guid> recordIds)
        {
            DataTable dt = new DataTable();

            var properties = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(ExportExcel))).ToList();

            CreateColumnDatatable(dt);

            string storedProceduredName = String.Format(ProcedureName.Get, typeof(T).Name, "All");

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();


                var keyProperty = typeof(T).GetProperties();


                string propertyNameForeignKey = string.Empty;
                string tableAccessoryName = string.Empty;
                //Lấy ra tên khóa ngoại và tên bảng phụ
                foreach (var property in keyProperty)
                {
                    var foreign = (ForeignKey?)property.GetCustomAttribute(typeof(ForeignKey), false);
                    if (foreign != null)
                    {
                        propertyNameForeignKey = property.Name;

                        //Tên bảng phụ (VD: thuộc tính được lấy ra có tên là DepartmentId -> tên bảng phụ là Department (Nếu theo đúng quy tắc đặt tên khóa chính))
                        tableAccessoryName = (property.Name).Substring(0, (property.Name).Length - 2);
                    }
                }
                foreach (var property in keyProperty)
                {
                    var key = (KeyAttribute?)property.GetCustomAttribute(typeof(KeyAttribute), false);
                    if (key != null)
                    {
                        string query = string.Format("SELECT *, ROW_NUMBER() OVER (ORDER BY e.CreatedDate DESC) AS STT FROM {0} e LEFT JOIN {1} d ON e.{2} = d.{2} WHERE {3} IN @ids", typeof(T).Name, tableAccessoryName, propertyNameForeignKey, property.Name);

                        var reader = mySqlConnection.QueryMultiple(query, param: new { ids = recordIds });
                        var listRecords = reader.Read<T>().ToList();


                        //Tạo 1 mảng object lưu thông tin các bản ghi có độ dài là số lượng các thuộc tính được xuất ra excel
                        var values = new object[properties.Count()];

                        foreach (var emp in listRecords)
                        {
                            for (int i = 0; i < values.Length; i++)
                            {
                                //Thêm giá trị từng thuộc tính được xuất ra excel vào mảng
                                values[i] = properties[i].GetValue(emp);
                            }

                            dt.Rows.Add(values);
                        }
                    }
                }



                //var reader = mySqlConnection.QueryMultiple(storedProceduredName, commandType: CommandType.StoredProcedure);
                //var listRecords = reader.Read<T>().ToList();

                ////Tạo 1 mảng object lưu thông tin các bản ghi có độ dài là số lượng các thuộc tính được xuất ra excel
                //var values = new object[properties.Count()];

                //foreach (var emp in listRecords)
                //{
                //    for (int i = 0; i < values.Length; i++)
                //    {
                //        //Thêm giá trị từng thuộc tính được xuất ra excel vào mảng
                //        values[i] = properties[i].GetValue(emp);
                //    }

                //    dt.Rows.Add(values);
                //}
            }

            return dt;
        }

        /// <summary>
        /// Tạo column datatable
        /// </summary>
        /// <param name="dt">datatable</param>
        /// <returns>
        /// Datatable có tiêu đề cột
        /// </returns>
        /// CreatedBy: huynq (20/2/2023)
        private DataTable CreateColumnDatatable(DataTable dt)
        {
            var properties = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(ExportExcel))).ToList();

            foreach (var property in properties)
            {
                //var name = property.Name;

                var propertyExportExcel = string.Empty;
                var propertyName = property.GetCustomAttributes(typeof(PropertyName), false);

                if (propertyName.Length > 0)
                {
                    //Lấy ra tên đầy đủ của các thuộc tính được xuất ra excel
                    propertyExportExcel = (propertyName[0] as PropertyName).Name;
                }

                //Header table
                dt.Columns.Add(propertyExportExcel);
            }
            return dt;
        }

        /// <summary>
        /// Kiểm tra mã bản ghi có bị trùng hay không
        /// </summary>
        /// <param name="recordCode">Mã bản ghi</param>
        /// <returns>
        /// true - Đã bị trùng
        /// false - Không bị trùng
        /// </returns>
        /// CreatedBy: huynq (8/2/2023)
        public bool CheckRecordCode(string recordCode)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(T).Name, $"{typeof(T).Name}Code");

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            var primaryCode = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(RecordCode)));
            foreach (var property in primaryCode)
            {
                //Đọc tên thuộc tính có trong object record (Employee, Department,...)
                string name = property.Name;

                //Viết đối số trong DB
                string argument = "@p_" + name;

                parameters.Add($"{argument}", recordCode);
            }

            //parameters.Add($"@p_{typeof(T).Name}Code", recordCode);

            //Khởi tạo kế nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                var res = mySqlConnection.QueryFirstOrDefault<string>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);

                //Xử lý kết quả trả về
                if (res != null)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Lấy mã bản ghi theo Id
        /// </summary>
        /// <param name="employeeId">Khóa chính</param>
        /// <returns>
        /// Trả về 1 mã bản ghi
        /// </returns>
        /// CreatedBy: huynq (8/2/2023)
        public string GetRecordCodeById(Guid recordId)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(T).Name, $"{typeof(T).Name}CodeById");

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            var primaryCode = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(KeyAttribute)));
            foreach (var property in primaryCode)
            {
                //Đọc tên thuộc tính có trong object record (Employee, Department,...)
                string name = property.Name;

                //Viết đối số trong DB
                string argument = "@p_" + name;

                parameters.Add($"{argument}", recordId);
            }
            //parameters.Add($"@p_{typeof(T).Name}Id", recordId);

            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                var recordCodeById = mySqlConnection.QueryFirstOrDefault<string>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);

                //Xử lý kết quả trả về
                return recordCodeById;
            }
        }

        #endregion
    }
}
