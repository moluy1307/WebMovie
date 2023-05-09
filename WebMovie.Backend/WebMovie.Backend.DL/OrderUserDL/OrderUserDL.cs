using Dapper;
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
                            DeleteOrder(orderId);
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

        public OrderUser GetOrderByUserId(Guid userId)
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
                var recordById = mySqlConnection.QueryFirstOrDefault<OrderUser>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);

                return recordById;
            }
        }
    }
}
