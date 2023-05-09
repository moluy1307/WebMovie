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
using WebMovie.Backend.Common;
using WebMovie.Backend.Common.Constants;
using WebMovie.Backend.Common.Entities;
using System.IO;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Search;

namespace WebMovie.Backend.DL.UserDL
{
    public class UserDL : BaseDL<User>, IUserDL
    {

        #region Method

        public User Login(string username, string password)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(User).Name, $"{typeof(User).Name}Login");

            var parameters = new DynamicParameters();
            parameters.Add($"@p_Username", username);
            parameters.Add($"@p_Password", password);

            //Khởi tạo kế nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                var res = mySqlConnection.QueryFirstOrDefault<User>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);

                return res;
            }

        }

        public int ChangePassword(Guid userId, string password)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = "Proc_User_ChangePassword";

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();
            parameters.Add("p_UserPassword", password);
            parameters.Add("p_UserId", userId);

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

        public User GetUserByEmail(string emailUser)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(User).Name, "ByEmail");

            var parameters = new DynamicParameters();
            parameters.Add($"@p_Email", emailUser);

            //Khởi tạo kế nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                var res = mySqlConnection.QueryFirstOrDefault<User>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);

                return res;
            }
        }

        public async Task SendConfirmCode(string emailMess)
        {
            var confirmCode = GenerateConfirmationCode();
            string subjectMess = "Mã xác thực";

            //Cập nhật mã xác thực
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        mySqlConnection.Execute("UPDATE User SET ConfirmCode = @p_ConfirmCode WHERE Email = @p_Email;",
                            new { p_ConfirmCode = confirmCode, p_Email = emailMess }, transaction: transaction);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            await SendEmail(emailMess, subjectMess, confirmCode);
        }

        public async Task SendNewPassword(string emailMess)
        {
            var newPassword = GenerateRandomPassword();
            string subjectMess = "Mật khẩu mới";

            //Cập nhật mật khẩu mới
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        mySqlConnection.Execute("UPDATE User SET Password = MD5(@p_Password) WHERE Email = @p_Email;",
                            new { p_Password = newPassword, p_Email = emailMess }, transaction: transaction);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            await SendEmail(emailMess, subjectMess, newPassword);
        }

        public int MinusAmountMoney(Guid userId, float amountMoneyMovie)
        {
            var userInfor = GetRecordById(userId);
            decimal totalAmount = 0;
            totalAmount = (decimal)userInfor.AmountMoney - (decimal)amountMoneyMovie;

            int numberOfAffectedRows = 0;
            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        numberOfAffectedRows = mySqlConnection.Execute("UPDATE User SET AmountMoney = @p_AmountMoney WHERE UserId = @p_UserId;",
                             new { p_AmountMoney = totalAmount, p_UserId = userId }, transaction: transaction);

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

        /// <summary>
        /// Hàm xử lý gửi email
        /// </summary>
        /// <param name="mailRequest"></param>
        /// <returns></returns>
        private async Task SendEmail(string emailMess, string subjectMess, string bodyMess)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("huynq130701@gmail.com"));
            email.To.Add(MailboxAddress.Parse(emailMess));
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
            tmp += "<p style=\"font-size: 18px;\"> " + subjectMess + " của bạn là:</p>";
            tmp += "<p style=\"font-size: 24px; font-weight: 700; \">" + bodyMess + "</p>";
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

        /// <summary>
        /// Hàm tạo mã xác nhận ngẫu nhiên
        /// </summary>
        /// <returns></returns>
        private string GenerateConfirmationCode()
        {
            var random = new Random();
            var confirmationCode = "";
            for (int i = 0; i < 6; i++)
            {
                confirmationCode += random.Next(0, 9).ToString();
            }
            return confirmationCode;
        }

        /// <summary>
        /// Hàm tạo mật khẩu mới ngẫu nhiên
        /// </summary>
        /// <returns></returns>
        private string GenerateRandomPassword()
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random();

            var password = "";
            for (int i = 0; i < 8; i++)
            {
                password += validChars[random.Next(validChars.Length)];
            }

            return password;
        }

        #endregion
    }
}
