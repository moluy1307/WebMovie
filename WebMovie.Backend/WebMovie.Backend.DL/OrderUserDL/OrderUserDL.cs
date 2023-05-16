using Dapper;
using MailKit.Security;
using MimeKit;
using Misa.Amis.DL.BaseDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.Common.Constants;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.DL.MovieDL;
using WebMovie.Backend.DL.UserDL;
using MailKit.Net.Smtp;

namespace WebMovie.Backend.DL.OrderUserDL
{
    public class OrderUserDL : BaseDL<OrderUser>, IOrderUserDL
    {
        public OrderUser GetOrderdById(Guid recordId)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(OrderUser).Name, "ById");

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();
            parameters.Add($"@p_OrderId", recordId);

            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                var recordById = mySqlConnection.QueryFirstOrDefault<OrderUser>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);

                return recordById;
            }
        }

        /// <summary>
        /// Cập nhật tồng tiền cho khách hàng
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int UpdateAmountMoney(Guid orderId)
        {
            var orderInfor = GetOrderdById(orderId);
            decimal totalAmount = 0;
            if (orderInfor.AmountMoney == null)
            {
                totalAmount = (decimal)orderInfor.Recharge;
            }
            else
            {
                totalAmount = (decimal)orderInfor.AmountMoney + (decimal)orderInfor.Recharge;
            }

            //Khởi tạo kết nối đến DB
            int numberOfAffectedRows = 0;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        numberOfAffectedRows = mySqlConnection.Execute("UPDATE User SET AmountMoney = @p_AmountMoney WHERE UserId = @p_UserId;",
                            new { p_AmountMoney = totalAmount, p_UserId = orderInfor.UserId }, transaction: transaction);

                        if(numberOfAffectedRows > 0)
                        {
                            //mySqlConnection.Execute("DELETE FROM OrderUser WHERE OrderId = p_OrderId;",
                            //new { p_AmountMoney = totalAmount, p_UserId = orderInfor.UserId }, transaction: transaction);
                            //DeleteOrder(orderId); 

                            mySqlConnection.Execute("UPDATE orderuser SET Enable = @p_Enable WHERE OrderId = @p_OrderId;",
                            new { p_Enable = 1, p_OrderId = orderInfor.OrderId }, transaction: transaction);
                        }
                        
                        transaction.Commit();
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

        public int DeleteOrder(Guid recordId)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Delete, typeof(OrderUser).Name);

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();

            parameters.Add($"p_OrderId", recordId);

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

        public IEnumerable<OrderUser> GetOrderByUserId(Guid userId)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(OrderUser).Name, "ByUserId");

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();
            parameters.Add($"@p_UserId", userId);

            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                var recordById = mySqlConnection.Query<OrderUser>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);

                return recordById;
            }
        }

        public IEnumerable<OrderUser> GetOrderRevenue(int? sortByYear)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(OrderUser).Name, "Revenue");

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();
            parameters.Add("@p_SortByYear", sortByYear);

            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                var recordById = mySqlConnection.Query<OrderUser>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);

                return recordById;
            }
        }

        public IEnumerable<OrderUser> GetUserIdBoughtMovie()
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(OrderUser).Name, "UserIdBoughtMovie");

            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                var recordById = mySqlConnection.Query<OrderUser>(storedProceduredName, commandType: CommandType.StoredProcedure);

                return recordById;
            }
        }

        /// <summary>
        /// Hàm xử lý gửi email
        /// </summary>
        /// <param name="mailRequest"></param>
        /// <returns></returns>
        public async Task SendEmail(string subjectMess, string accountName, string customerName, int moneyRecharge)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("huynq130701@gmail.com"));
            email.To.Add(MailboxAddress.Parse("huynq130701@gmail.com"));
            email.Subject = subjectMess;

            var builder = new BodyBuilder();

            //if (mailRequest.Attachments != null)
            //{
            //    byte[] fileBytes;
            //    foreach (var file in mailRequest.Attachments)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using (var ms = new MemoryStream())
            //            {
            //                file.CopyTo(ms);
            //                fileBytes = ms.ToArray();
            //            }
            //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            //        }
            //    }
            //}

            string tmp = "";
            tmp += "<div style=\" width: 100%; height: 100%; position: relative\">";
            tmp += "<div style=\"background-color: #EEEEEE; position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); text-align: center; height:100%; padding: 100px 0\">";
            tmp += "<p style=\"font-size: 18px;\">Thông tin yêu cầu</p>";
            tmp += "<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse;width: 100%;\">";
            tmp += "<tr>";
            tmp += "<th>Tài khoản</th>";
            tmp += "<th>Tên khách hàng</th>";
            tmp += "<th>Số xu yêu cầu nạp</th>";
            tmp += "</tr>";
            tmp += "<tr>";
            tmp += "<td>"+ accountName + "</td>";
            tmp += "<td>" + customerName + "</td>";
            tmp += "<td>" + moneyRecharge + "</td>";
            tmp += "</tr>";
            tmp += "</table>";
            tmp += "</div>";
            tmp += "</div>";

            builder.HtmlBody = tmp;
            email.Body = builder.ToMessageBody();

            using var client = new SmtpClient();

            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate("huynq130701@gmail.com", "rreacnwxcgokwxkt");
            await client.SendAsync(email);
            client.Disconnect(true);

        }
    }
}
